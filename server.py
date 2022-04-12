from parser.parser import Parser
from fastapi import FastAPI
from datetime import datetime
import parser.utils.constants as c
import typing
import asyncio


app = FastAPI()

parser = Parser()

parser.run_forever()


@app.get('/')
async def root() -> dict:
    return {'result': '200'}


@app.get('/run/')
async def run(once: typing.Optional[bool] = False) -> dict:
    if (parser.future_time - datetime.utcnow()).seconds > 600:
        if not c.something_is_running:
            if once:
                if parser.run_once_task is None:
                    parser.run_once()
                    return {'result': 'One time parser started'}
                else:
                    return {'error': 'One time parser is already running'}
            else:
                if parser.run_forever_task is None:
                    parser.run_forever()
                    return {'result': 'Forever working parser started'}
                else:
                    return {'error': 'Forever working parser is already running'}
        else:
            return {'error': 'Cannot start another parser during parsing tables'}
    else:
        return {'error': f'Cannot start another parser as less then 10 minutes left for another launch \
({(parser.future_time - datetime.utcnow()).seconds} seconds remaining)'}


@app.get('/stop/')
async def stop(once: typing.Optional[bool] = False) -> dict:
    if once:
        if parser.run_once_task is not None:
            parser.stop_once()
            c.something_is_running = False
            return {'result': 'One time parser stopped'}
        else:
            return {'result': 'No one time parser running'}
    else:
        if parser.run_forever_task is not None:
            parser.stop_forever()
            c.something_is_running = False
            return {'result': 'Forever working parser stopped'}
        else:
            return {'error': 'No forever working parser running'}


@app.get('/status/')
async def status() -> dict:
    tasks = asyncio.all_tasks()
    _tasks = []
    for task in tasks:
        if task.get_name() == 'run_forever_task' or task.get_name() == 'run_once_task':
            _tasks.append(task.get_name())
    return {'result': _tasks} if len(_tasks) != 0 else {'result': 'No parser running'}


@app.get('/delay/')
async def delay(set: typing.Optional[bool] = False, value: typing.Optional[int] = 86400) -> dict:
    if set and value is not None:
        try:
            int(value)
        except Exception:
            return {'error': 'Error converting given value to int'}
        c.REPEAT_DELAY = int(value)
        return {'result': f'Repeat delay changed to {c.REPEAT_DELAY}'}
    return {'result': c.REPEAT_DELAY}
