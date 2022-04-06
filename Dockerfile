FROM python:3.10.2

WORKDIR /schedule-parser

COPY . .

RUN pip install poetry
RUN poetry install

CMD ["python", "server.py"]