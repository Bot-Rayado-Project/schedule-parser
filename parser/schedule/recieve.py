import traceback
import asyncio
import aiohttp
import aiofile
import typing
import parser.utils.constants as c

from parser.schedule.sheethandler import get_schedules
from parser.utils.constants import STREAMS, STREAMS_IDS, DBUSER, DBPASSWORD, DBHOST, DBNAME, DEBUG
from parser.utils.logger import get_logger
from bs4 import BeautifulSoup

logger = get_logger(__name__)


async def aiohttp_fetch(url: str, content: typing.Optional[bool] = False) -> bytes | str:
    '''Отправляет GET запрос по URL'''
    async with aiohttp.ClientSession() as session:
        async with session.get(url) as response:
            if content:
                return await response.content.read()
            else:
                return await response.text()


async def get_links() -> None | int:
    '''Парсит сайт на наличие ссылок, записывает файлы таблиц для каждого потока и вызывает.
     В случае ошибки делает еще попытку (из 5) получить информацию'''
    tries = 5
    while tries != 0:
        raw_responce = await aiohttp_fetch('https://mtuci.ru/time-table/')
        soup = BeautifulSoup(raw_responce, 'html.parser')
        counter = 0
        errors = []
        logger.info('Looking for links...')
        for link in soup.find_all('a'):
            _link = link.get('href')
            try:
                _link.lower()
            except AttributeError:
                continue
            if _link.lower().startswith('/upload/') and "1-kurs" in _link.lower():
                for stream in STREAMS:
                    if STREAMS_IDS[stream] in _link.lower():
                        try:
                            async with aiofile.async_open(f'tables/table_{stream}.xlsx', 'wb') as table:
                                await table.write(await aiohttp_fetch('https://mtuci.ru' + _link, True))
                                counter += 1
                        except Exception as e:
                            logger.error(f"Error in downloading tables ({e}): {traceback.format_exc()}")
                            errors += [stream]
                            await asyncio.sleep(0.33)
                            continue
        if counter == len(STREAMS_IDS):
            logger.info(f'Все таблицы загружены ({counter}/{len(STREAMS_IDS)})')
            c.something_is_running = True
            await get_schedules()
            c.something_is_running = False
            return 0
        else:
            logger.error(f'({counter}/{len(STREAMS_IDS)}) таблиц загружено. Ошибка в {errors}. Выполняется попытка {tries-1}: {traceback.format_exc()}')
            tries -= 1
            await asyncio.sleep(0.33)
            continue
    c.something_is_running = False
    return None
