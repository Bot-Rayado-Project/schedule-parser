import logging

log_format = '%(asctime)s.%(msecs)03d %(filename)s:%(lineno)d %(levelname)s %(message)s'


def get_file_handler() -> None:
    file_handler = logging.FileHandler("logs.log")
    file_handler.setLevel(logging.WARNING)
    file_handler.setFormatter(logging.Formatter(log_format))
    return file_handler


def get_stream_handler() -> None:
    stream_handler = logging.StreamHandler()
    stream_handler.setLevel(logging.INFO)
    stream_handler.setFormatter(logging.Formatter(log_format))
    return stream_handler


def get_logger(name: str) -> None:
    logger = logging.getLogger(name)
    logger.setLevel(logging.INFO)
    logger.addHandler(get_file_handler())
    logger.addHandler(get_stream_handler())
    return logger
