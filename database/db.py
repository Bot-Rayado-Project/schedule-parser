import asyncpg


async def db_execute(string: str):
    conn = await asyncpg.connect(user='postgres', password='admin', database='schedules', host='localhost')
    await conn.fetch(f"INSERT INTO schedule_table VALUES('{string}', 'понедельник', 'Увы', True);")

    await conn.close()
