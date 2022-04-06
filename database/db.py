import asyncpg
from parser.utils.logger import get_logger

logger = get_logger(__name__)


async def db_connect(user: str = 'postgres', password: str = 'admin', name: str = 'schedules', host: str = 'localhost'):
    connection = await asyncpg.connect(user=user, password=password, database=name, host=host)
    return connection


async def db_close(connection):
    await connection.close()


async def db_execute(connection, group: str, dayofweek: str, weektype: str, schedule: str):
    even: bool = True if weektype == 'четная' else False
    a: dict = await connection.fetch(f"SELECT schedule FROM schedule_table WHERE streamgroup = '{group}' AND dayofweek = '{dayofweek}' AND even = '{even}';")
    if len(a) == 0:
        logger.info(f'Успешно записано {dayofweek}, {group}, {weektype}')
        await connection.fetchrow(f"INSERT INTO schedule_table VALUES('{group}','{dayofweek}','{schedule}',{even});")
    else:
        logger.info(f'Успешно перезаписано {dayofweek}, {group}, {weektype}')
        await connection.fetchrow(f"UPDATE schedule_table SET schedule = '{schedule}' WHERE streamgroup = '{group}' AND dayofweek = '{dayofweek}' AND even = '{even}';")
