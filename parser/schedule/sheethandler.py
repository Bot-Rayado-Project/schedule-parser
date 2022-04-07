import traceback
import openpyxl

from parser.utils.constants_blueprint import GROUP_MATCHING_SCHEDULE
from parser.utils.constants import WEEK_COLUMN_GROUPS
from parser.utils.logger import get_logger
from parser.schedule.streams.KIIB.zrc import get_schedule_zrc
from database.db import db_execute
from transliterate import translit


logger = get_logger(__name__)


async def get_sheet(temp_number: str, path: str) -> openpyxl.Workbook:
    wb_obj = openpyxl.load_workbook(path)
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

    if group_input[0:3] == 'зрс':
        return await get_schedule_zrc(day_input, group_input, week_type, schedule)
    else:
        return await GROUP_MATCHING_SCHEDULE[group_input[0:3]](day_input, group_input, WEEK_COLUMN_GROUPS[group_input], week_type, schedule)


async def get_schedules(connection) -> None:
    days = ['понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота']
    groups = ['бвт2101', 'бвт2102', 'бвт2103', 'бвт2104', 'бвт2105', 'бвт2106',
              'бвт2107', 'бвт2108', 'бфи2101', 'бфи2102', 'бст2101', 'бст2102',
              'бст2103', 'бст2104', 'бст2105', 'бст2106', 'бэи2101', 'бэи2102',
              'бэи2103', 'биб2101', 'биб2102', 'биб2103', 'биб2104', 'бмп2101',
              'зрс2101', 'зрс2102', 'бап2101', 'бут2101', 'брт2101', 'брт2102',
              'бик2101', 'бик2102', 'бик2103', 'бик2104', 'бик2105', 'бик2106',
              'бик2107', 'бик2108', 'бик2109', 'бээ2101', 'бби2101', 'бэр2101',
              'бин2101', 'бин2102', 'бин2103', 'бин2104', 'бин2105', 'бин2106',
              'бин2107', 'бин2108', 'бин2109', 'бин2110']
    weektypes = ['четная', 'нечетная']
    counter = 0
    errors = []
    for day in days:
        for group in groups:
            for weektype in weektypes:
                try:
                    schedule = await write_schedule(day, group, weektype)
                    if schedule != None:
                        logger.info(translit(connection, "ru", reversed=True))
                        logger.info(translit(group, "ru", reversed=True))
                        logger.info(translit(day, "ru", reversed=True))
                        await db_execute(translit(connection, "ru", reversed=True),
                                         translit(group, "ru", reversed=True),
                                         translit(day, "ru", reversed=True), weektype, schedule)
                        counter += 1
                    else:
                        logger.error(f'Ошибка в {day}, {group}, {weektype} во время парсинга таблицы. Обновление отменено.')
                except Exception as e:
                    logger.error(f'Ошибка в {day}, {group}, {weektype}')
                    errors += [f"{day}, {group}, {weektype}"]
                    logger.error(f'{e}: {traceback.format_exc()}')
                    pass
    if counter == 624:
        logger.info(f'Все таблицы загружены ({counter}/624)')
    else:
        logger.error(f'({counter}/624) таблиц загружено. Ошибка в {errors}')
