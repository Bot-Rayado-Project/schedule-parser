import os
import asyncio
import traceback
import typing
import parser.schedule.recieve as r
from parser.utils.logger import get_logger
from parser.utils.constants import REPEAT_DELAY

logger = get_logger(__name__)


class Parser:
    def __init__(self):
        self.first_start = True

    async def run(self, ignore_errors: bool = True):
        logger.info("Starting bot...")
        if not os.path.isdir("tables"):
            os.mkdir("tables")
        asyncio.get_running_loop().create_task(self.start(ignore_errors))

    async def _start(self, delay: float):
        if self.first_start:
            logger.info("First start detected")
            await r.get_links()
            self.first_start = False
        else:
            logger.info(f"Not first start. Sleeping for {delay}...")
            await asyncio.sleep(delay)
            await r.get_links()

    async def start(self, ignore_errors: bool = True):
        if not ignore_errors:
            while True:
                await self._start(REPEAT_DELAY)
        else:
            while True:
                try:
                    await self._start(REPEAT_DELAY)
                except Exception as e:
                    logger.error(f"Error in event loop ({e}): {traceback.format_exc()}")
                    await asyncio.sleep(0.33)
                    continue

    def run_forever(self, ignore_errors: bool = True, loop: typing.Optional[asyncio.AbstractEventLoop] = None):
        loop = loop or asyncio.get_event_loop()
        loop.create_task(self.run(ignore_errors))
        loop.run_forever()
