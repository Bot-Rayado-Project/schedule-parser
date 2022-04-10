import os
import asyncio
import traceback
import typing
import parser.schedule.recieve as r
from datetime import datetime, timedelta
from parser.utils.logger import get_logger
from parser.utils.constants import REPEAT_DELAY

logger = get_logger(__name__)


class Parser:
    def __init__(self) -> None:
        self.first_start = True
        self.run_once_task = None
        self.run_forever_task = None
        self.future_time = datetime.utcnow() + timedelta(days=30)

    def stop_forever(self):
        self._run_forver_task.cancel()
        self.run_forever_task = None

    def stop_once(self):
        self._run_once_task.cancel()
        self.run_once_task = None

    def run_once(self, ignore_errors: bool = True) -> None:
        self._run_once_task = asyncio.get_running_loop().create_task(self._start(10, True), name='run_once_task')
        self.run_once_task = True

    def run_forever(self) -> None:
        self.first_start = True
        self._run_forver_task = asyncio.get_running_loop().create_task(coro=self.start(), name='run_forever_task')
        self.run_forever_task = True

    async def run(self, ignore_errors: bool = True) -> None:
        logger.info("Starting parser...")
        if not os.path.isdir("tables"):
            os.mkdir("tables")
        asyncio.get_running_loop().create_task(self.start(ignore_errors))

    async def _start(self, delay: float, run_once: typing.Optional[bool] = False) -> None:
        if not run_once:
            if self.first_start:
                self.first_start = False
                logger.info("First start detected")
                res = await r.get_links()
                if res is None:
                    raise Exception
            else:
                logger.info(f"Another start detected. Sleeping for {delay} seconds...")
                self.future_time = datetime.utcnow() + timedelta(seconds=delay)
                await asyncio.sleep(delay)
                res = await r.get_links()
                if res is None:
                    raise Exception
        else:
            logger.info('Run once comand detected')
            try:
                res = await r.get_links()
                if res is None:
                    raise Exception
                self.run_once_task = None
            except Exception as e:
                logger.error(f"Error in run once ({e}): {traceback.format_exc()}")
                await asyncio.sleep(0.33)

    async def start(self, ignore_errors: bool = True) -> None:
        if not ignore_errors:
            while True:
                await self._start(int(REPEAT_DELAY))
        else:
            while True:
                try:
                    await self._start(int(REPEAT_DELAY))
                except Exception as e:
                    logger.error(f"Error in event loop ({e}): {traceback.format_exc()}")
                    await asyncio.sleep(0.33)
                    continue

    def run_forever_old(self, ignore_errors: bool = True, loop: typing.Optional[asyncio.AbstractEventLoop] = None) -> None:
        loop = loop or asyncio.get_event_loop()
        loop.create_task(self.run(ignore_errors))
        loop.run_forever()
