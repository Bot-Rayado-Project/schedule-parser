import asyncio
import traceback
import typing
import parser.schedule.recieve as r
from datetime import datetime, timedelta
from parser.utils.logger import get_logger
from parser.utils.constants import REPEAT_DELAY

logger = get_logger(__name__)


class Parser:
    '''Class defining parser functions'''

    def __init__(self) -> None:
        self.first_start = True
        self.run_once_task = None
        self.run_forever_task = None
        self.future_time = datetime.utcnow() + timedelta(days=30)

    def stop_forever(self) -> None:
        '''Stops forever running parser'''
        self._run_forver_task.cancel()
        self.run_forever_task = None

    def stop_once(self) -> None:
        '''Stops one time parser'''
        self._run_once_task.cancel()
        self.run_once_task = None

    def run_once(self) -> None:
        '''Starts one time parcer'''
        self._run_once_task = asyncio.get_running_loop().create_task(self._start(run_once=True), name='run_once_task')
        self.run_once_task = True

    def run_forever(self, ignore_errors: typing.Optional[bool] = True) -> None:
        '''Starts forever running parser'''
        self.first_start = True
        self._run_forver_task = asyncio.get_running_loop().create_task(coro=self._start_forever(ignore_errors), name='run_forever_task')
        self.run_forever_task = True

    async def _start(self, delay: typing.Optional[float] = 28800, run_once: typing.Optional[bool] = False) -> None:
        '''Low level function of run_once'''
        if not run_once:
            if self.first_start:
                self.first_start = False
                logger.info("First start detected")
                res = await r.get_links()
                if res is None:
                    raise Exception
            else:
                logger.info(f"Another start detected. Sleeping for {delay} seconds...")
                self.future_time = datetime.utcnow() + timedelta(seconds=float(delay))
                await asyncio.sleep(float(delay))
                res = await r.get_links()
                if res is None:
                    raise Exception
        else:
            logger.info('Run once command detected')
            try:
                res = await r.get_links()
                if res is None:
                    raise Exception
                self.run_once_task = None
            except Exception as e:
                logger.error(f"Error in run once ({e}): {traceback.format_exc()}")
                await asyncio.sleep(0.33)

    async def _start_forever(self, ignore_errors: typing.Optional[bool] = True) -> None:
        '''Low level function of run_forever'''
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
