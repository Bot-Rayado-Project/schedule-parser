import asyncio
import traceback
from parser.utils.constants import SUPPLEMENTS, TIME
from parser.utils.logger import get_logger

logger = get_logger(__name__)


async def get_schedule_zrs(day_type: str, group_text: str, week_type: str, schedule) -> str | None:

    week_column = 'G' if week_type == 'нечетная' else 'H'
    const = 1 if week_type == 'четная' else -1

    days = {'понедельник': 14, 'вторник': 20, 'среда': 26, 'четверг': 32, 'пятница': 38, 'суббота': 44}

    schedule_output = ''
    try:
        for i in range(days[day_type], days[day_type] + 5):

            if schedule[week_column + str(i)].value is not None:
                schedule_output += str(TIME[i - days[day_type] + 1]) + '  ' \
                    + str(schedule[week_column + str(i)].value) + '\n\n' \
                    + 'Преподаватель: ' + str(schedule[chr(ord(week_column) + 1 * const) + str(i)].value) + '\n'\
                    + 'Вид занятия: ' + str(SUPPLEMENTS[str((schedule[chr(ord(week_column) + 2 * const) + str(i)].value).replace(' ', ''))]) + '\n' \
                    + 'Форма проведения: ' + str(SUPPLEMENTS[str((schedule[chr(ord(week_column) + 3 * const) + str(i)].value).replace(' ', ''))]) + '\n' \
                    + '⸻⸻⸻⸻⸻\n'
            else:
                schedule_output += 'Пары нет\n' + '⸻⸻⸻⸻⸻\n'
    except Exception as e:
        logger.error(f'Error in zrs {e}: {traceback.format_exc()}')
        await asyncio.sleep(0.33)
        return None
    return schedule_output
