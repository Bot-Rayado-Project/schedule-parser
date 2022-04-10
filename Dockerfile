FROM python:3.10.2

ARG ENVIRON

ENV ENVIRON=${ENVIRON} \
  PYTHONFAULTHANDLER=1 \
  PYTHONUNBUFFERED=1 \
  PYTHONHASHSEED=random \
  PIP_NO_CACHE_DIR=off \
  PIP_DISABLE_PIP_VERSION_CHECK=on \
  PIP_DEFAULT_TIMEOUT=100 \
  POETRY_VERSION=1.0.0

# System deps:
RUN pip install "poetry==$POETRY_VERSION"

# Copy only requirements to cache them in docker layer:
WORKDIR /schedule-parser
COPY poetry.lock pyproject.toml /schedule-parser/

# Project initialization:
RUN poetry config virtualenvs.create false \
  && poetry install --no-dev --no-interaction --no-ansi

# Creating folders, and files for a project:
COPY . /schedule-parser

# Setting start comand to python server.py:
CMD ["uvicorn", "server:app", "--host", "0.0.0.0", "--port", "5000"]