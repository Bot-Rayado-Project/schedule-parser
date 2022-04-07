import os
import asyncio
import traceback
import typing
import parser.schedule.recieve as r
from parser.utils.logger import get_logger

logger = get_logger(__name__)


class Parser:
    async def run(self, ignore_errors: bool = True):
        logger.info("Starting bot...")
        if not os.path.isdir("tables"):
            os.mkdir("tables")
        asyncio.get_running_loop().create_task(self.start(ignore_errors))

    async def _start(self, delay: float):
        await r.get_links()
        await asyncio.sleep(delay)

    async def start(self, ignore_errors: bool = True):
        if not ignore_errors:
            while True:
                await self._start(3600)
        else:
            while True:
                try:
                    await self._start(3600)
                except Exception as e:
                    logger.error(f"Error in event loop ({e}): {traceback.format_exc()}")
                    await asyncio.sleep(0.33)
                    continue

    def run_forever(self, ignore_errors: bool = True, loop: typing.Optional[asyncio.AbstractEventLoop] = None):
        loop = loop or asyncio.get_event_loop()
        loop.create_task(self.run(ignore_errors))
        loop.run_forever()
