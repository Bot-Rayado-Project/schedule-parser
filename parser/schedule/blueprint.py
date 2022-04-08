import traceback
import asyncio
from parser.utils.constants import TIME
from parser.utils.logger import get_logger

logger = get_logger(__name__)


async def get_schedule(start_cell: int, week_type: str, group: str, schedule, group_column: str, x: int, day_type: str) -> str | None:
    week_days = {'понедельник': 0, 'вторник': 1, 'среда': 2, 'четверг': 3, 'пятница': 4, 'суббота': 5, }
    day = week_days[day_type] * x + start_cell
    schedule_output = ''
    time_para = 1  # Номер пары для времени
    for para_cell in range(day, day + 10, 2):
        try:
            if schedule[group_column + str(para_cell)].value is not None:
                schedule_output += str(TIME[time_para]) + '  ' \
                    + str(schedule[group_column + str(para_cell)].value) + '\n\n' \
                    + '⸻⸻⸻⸻⸻\n'  # Добавляем пару, то есть ячейку если она не пустая
            else:
                # Если же ячейка пустая, значит пары нет
                schedule_output += 'Пары нет\n' + '⸻⸻⸻⸻⸻\n'
            time_para += 1  # Счётчик пары плюс один
        except Exception as e:
            logger.error(f'Error in blueprint {e}: {traceback.format_exc()}')
            await asyncio.sleep(0.33)
            return None

    return schedule_output
