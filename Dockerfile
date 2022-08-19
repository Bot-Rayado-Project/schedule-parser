<<<<<<< HEAD
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
=======
FROM python:3.10-slim

ARG ENVIRON

ENV ENVIRON=${ENVIRON} \
  PYTHONFAULTHANDLER=1 \
  PYTHONUNBUFFERED=1 \
  PYTHONHASHSEED=random \
  PIP_NO_CACHE_DIR=off \
  PIP_DISABLE_PIP_VERSION_CHECK=on \
  PIP_DEFAULT_TIMEOUT=100 \
  POETRY_VERSION=1.0.0 \
  WORKER_CLASS=uvicorn.workers.UvicornH11Worker

# System deps:
RUN pip install "poetry==$POETRY_VERSION"

# Copy only requirements to cache them in docker layer:
>>>>>>> main
WORKDIR /schedule-parser

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /schedule-parser
COPY --from=build-env /schedule-parser/out .
ENTRYPOINT ["dotnet", "ParserAPI.dll"]