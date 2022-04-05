from parser.schedule.blueprint import get_schedule


async def get_schedule_bin(day_type: str, group_text: str, group_column: str, week_type: str, schedule) -> str:

    start_cell = 4 if week_type == 'четная' else 3
    return await get_schedule(start_cell, week_type, group_text, schedule, group_column, 11, day_type)
