FROM python:3.10.2

WORKDIR /schedule-parser
COPY poetry.lock pyproject.toml /schedule-parser/

RUN pip install poetry
RUN poetry install

RUN poetry config virtualenvs.create false && poetry install

COPY . /schedule-parser

CMD ["python", "server.py"]