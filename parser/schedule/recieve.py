import aiohttp
import aiofile

from parser.schedule.sheethandler import get_schedules
from parser.utils.constants import STREAMS, STREAMS_IDS
from parser.utils.logger import get_logger
from database.db import db_execute
from bs4 import BeautifulSoup

logger = get_logger(__name__)


async def aiohttp_fetch(url: str, content: bool = False) -> str:
    async with aiohttp.ClientSession() as session:
        async with session.get(url) as response:
            if content:
                return await response.content.read()
            else:
                return await response.text()


async def get_links() -> dict | None:
    '''Задает стартовые сигнатуры для каждого потока. Вызывается единожды - при запуске бота.'''
    raw_responce = await aiohttp_fetch('https://mtuci.ru/time-table/')
    soup = BeautifulSoup(raw_responce, 'html.parser')
    counter = 0
    errors = []
    logger.info('Поиск ссылок')
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
                            logger.info(f'Файл tables/table_{stream}.xlsx успешно записан')
                            counter += 1
                    except Exception:
                        errors += [stream]
                        continue
    if counter == len(STREAMS_IDS):
        logger.info(f'Все таблицы загружены ({counter}/{len(STREAMS_IDS)})')
    else:
        logger.error(f'({counter}/{len(STREAMS_IDS)}) таблиц загружено. Ошибка в {errors}')
    await get_schedules()
