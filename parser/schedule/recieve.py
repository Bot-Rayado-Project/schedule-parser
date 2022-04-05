import aiohttp

from parser.utils.constants import STREAMS, STREAMS_IDS
from parser.utils.logger import get_logger
from bs4 import BeautifulSoup

logger = get_logger(__name__)


async def aiohttp_fetch(url: str) -> str:
    async with aiohttp.ClientSession() as session:
        async with session.get(url) as response:
            return await response.text()


async def get_links() -> dict | None:
    '''Задает стартовые сигнатуры для каждого потока. Вызывается единожды - при запуске бота.'''
    raw_responce = await aiohttp_fetch('https://mtuci.ru/time-table/')
    soup = BeautifulSoup(raw_responce, 'html.parser')
    logger.info('Поиск ссылок')
    for link in soup.find_all('a'):
        _link = link.get('href')
        try:
            if _link.lower().startswith('/upload/') and "1-kurs" in _link.lower():
                for stream in STREAMS:
                    if STREAMS_IDS[stream] in _link.lower():
                        logger.info(_link)
        except AttributeError:
            pass
        except KeyError:
            return None
