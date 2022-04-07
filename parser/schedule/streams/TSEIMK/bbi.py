from parser.schedule.blueprint import get_schedule


async def get_schedule_bbi(day_type: str, group_text: str, group_column: str, week_type: str, schedule) -> str:

    start_cell = 3 if week_type == 'четная' else 2
    return await get_schedule(start_cell, week_type, group_text, schedule, group_column, 11, day_type)
