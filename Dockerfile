FROM python:3.10.2

WORKDIR /schedule-parser
COPY poetry.lock pyproject.toml /schedule-parser/

RUN pip install poetry
RUN pip install aiohttp
RUN pip install aiofile
RUN pip install asyncio
RUN pip install bs4
RUN pip install asyncpg

COPY . /schedule-parser

CMD ["python", "server.py"]