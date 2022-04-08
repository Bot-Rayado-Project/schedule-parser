from typing import Any, Tuple, Union
from parser.utils.logger import get_logger

import traceback
import asyncio
import asyncpg

logger = get_logger(__name__)


async def db_connect(user: str, password: str, name: str, host: str) -> asyncpg.Connection | None:
    '''Выполняет подключение к базе данных. В случае ошибки подключение выполняет еще одну попытку. Всего попыток 5.
    В случае последней неудачи возвращает None, иначе - asyncpg.Connection'''
    tries = 5
    while True:
        try:
            connection = await asyncpg.connect(user=user, password=password, database=name, host=host)
            logger.info(f'Successfully connected to database {name} to host {host} with user {user}')
            return connection
        except Exception as e:
            tries -= 1
            logger.error(f"Error connecting to database ({e}). Tries left: {tries}: {traceback.format_exc()}")
            await asyncio.sleep(0.33)
            if tries == 0:
                return None


async def db_close(connection: asyncpg.Connection) -> None:
    '''Закрывает подключение с БД'''
    try:
        await connection.close()
    except Exception as e:
        logger.error(f"Error closing database ({e}): {traceback.format_exc()}")
        await asyncio.sleep(0.33)


async def db_write_schedule(connection: asyncpg.Connection, group: str, dayofweek: str, weektype: str, schedule: Union[str, Tuple[Any, ...], bool]) -> None:
    '''Записывает расписание для нужной группы, дня недели, четной или нечетной недели'''
    even: bool = True if weektype == 'четная' else False
    database_responce: list = await connection.fetch(f"SELECT schedule FROM schedule_table WHERE streamgroup = '{group}' AND dayofweek = '{dayofweek}' AND even = '{even}';")
    try:
        _database_responce = dict(database_responce[0])
        if _database_responce['schedule'] == schedule:
            # logger.info(f'Изменений нет в {dayofweek}, {group}, {weektype}')
            pass
        else:
            await connection.fetchrow(f"UPDATE schedule_table SET schedule = '{schedule}' WHERE streamgroup = '{group}' AND dayofweek = '{dayofweek}' AND even = '{even}';")
            logger.info(f'Успешно перезаписано {dayofweek}, {group}, {weektype}')
    except Exception:
        logger.info(f'Успешно записано {dayofweek}, {group}, {weektype}')
        await connection.fetchrow(f"INSERT INTO schedule_table VALUES('{group}','{dayofweek}','{schedule}',{even});")
        pass
