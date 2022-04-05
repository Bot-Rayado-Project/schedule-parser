from parser.utils.constants import TIME


async def get_schedule(start_cell: int, week_type: str, group: str, schedule, group_column: str, x: int, day_type: str) -> str:
    week_days = {'понедельник': 0, 'вторник': 1, 'среда': 2, 'четверг': 3, 'пятница': 4, 'суббота': 5, }
    day = week_days[day_type] * x + start_cell
    schedule_output = '⸻⸻⸻⸻⸻\n' + 'Группа: ' + group.upper() + '\n' \
        + 'День недели: ' + day_type.capitalize() + '\n' + 'Неделя: ' + week_type.capitalize() + '\n' \
        + '⸻⸻⸻⸻⸻\n'  # Добавляем заголовок вывода, группа и тд.
    time_para = 1  # Номер пары для времени
    for para_cell in range(day, day + 10, 2):
        if schedule[group_column + str(para_cell)].value != None:
            schedule_output += str(TIME[time_para]) + '  ' \
                + str(schedule[group_column + str(para_cell)].value) + '\n\n' \
                + '⸻⸻⸻⸻⸻\n'  # Добавляем пару, то есть ячейку если она не пустая
        else:
            # Если же ячейка пустая, значит пары нет
            schedule_output += 'Пары нет\n' + '⸻⸻⸻⸻⸻\n'
        time_para += 1  # Счётчик пары плюс один

    return schedule_output
