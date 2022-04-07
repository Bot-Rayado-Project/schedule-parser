import traceback
import asyncpg
import asyncio
import openpyxl

from parser.utils.constants_blueprint import GROUP_MATCHING_SCHEDULE
from parser.utils.constants import WEEK_COLUMN_GROUPS, DAYS, WEEKTYPES, GROUPS
from parser.utils.logger import get_logger
from parser.schedule.streams.KIIB.zrs import get_schedule_zrs
from database.db import db_write_schedule
from transliterate import translit


logger = get_logger(__name__)


async def get_sheet(temp_number: str, path: str) -> openpyxl.Workbook | None:
    try:
        wb_obj = openpyxl.load_workbook(path)
    except Exception as e:
        logger.error(f'Ошибка при открытии таблицы {path} ({e}): {traceback.format_exc()}')
        return None
    wb_obj.active = temp_number  # Задача листа таблицы
    sheet = wb_obj.active  # Выборка правильной таблицы

    return sheet


async def write_schedule(day_input: str, group_input: str, week_type: str) -> str | tuple | bool:
    if (('бвт' in group_input and int(group_input[-1]) < 5) or ('бфи' in group_input) or ('бст' in group_input and int(group_input[-1]) < 4)
        or ('бэи' in group_input) or ('биб' in group_input) or ('бмп' in group_input)
        or ('бап' in group_input) or ('бут' in group_input) or ('зрс' in group_input and int(group_input[-1]) == 1)
        or ('брт' in group_input) or ('бик' in group_input and int(group_input[-1]) < 4) or ('бээ' in group_input) or ('бби' in group_input)
            or ('бэр' in group_input) or ('бин' in group_input and int(group_input[-1]) < 5)):
        group_list = 0
    elif (('бвт' in group_input and int(group_input[-1]) > 4) or ('бст' in group_input and int(group_input[-1]) > 3)
          or ('зрс' in group_input and int(group_input[-1]) == 2)
          or ('бик' in group_input and int(group_input[-1]) > 3 and int(group_input[-1]) < 7)
          or ('бин' in group_input and int(group_input[-1]) > 4 and int(group_input[-1]) < 8)):
        group_list = 1
    else:
        group_list = 2  # Выборка номера листа для каждой группы разный от условия

    schedule = await get_sheet(group_list, f'tables/table_{group_input[:3]}.xlsx')

    if schedule != None:
        if group_input[0:3] == 'зрс':
            return await get_schedule_zrs(day_input, group_input, week_type, schedule)
        else:
            return await GROUP_MATCHING_SCHEDULE[group_input[0:3]](day_input, group_input, WEEK_COLUMN_GROUPS[group_input], week_type, schedule)
    else:
        return None


async def get_schedules(connection: asyncpg.Connection) -> None:
    counter = 0
    errors = {}
    for day in DAYS:
        for group in GROUPS:
            for weektype in WEEKTYPES:
                try:
                    schedule = await write_schedule(day, group, weektype)
                    if schedule != None:
                        await db_write_schedule(connection,
                                                translit(group, "ru", reversed=True),
                                                str(translit(day, "ru", reversed=True)).replace("'", ""), weektype, schedule)
                        counter += 1
                    else:
                        logger.error(f'Ошибка в {day}, {group}, {weektype} во время парсинга таблицы. Обновление отменено.')
                        await asyncio.sleep(0.33)
                except Exception as e:
                    errors[f"{day}, {group}, {weektype}"] = traceback.format_exc()
                    await asyncio.sleep(0.33)
                    pass
    if counter == 624:
        logger.info(f'Все таблицы загружены ({counter}/624)')
    else:
        logger.error(f'({counter}/624) таблиц загружено. Ошибка в {errors}')
