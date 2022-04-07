import logging
import logging.handlers
from parser.utils.constants import USER, NAME, HOST, PASSWORD, REPEAT_DELAY, DEBUG

log_format = '%(asctime)s.%(msecs)03d %(filename)s:%(lineno)d %(levelname)s %(message)s'


def get_file_handler() -> None:
    file_handler = logging.FileHandler("logs.log", encoding='UTF-8')
    file_handler.setLevel(logging.INFO)
    file_handler.setFormatter(logging.Formatter(log_format))
    return file_handler


def get_smtp_handler() -> None:
    smtp_handler = logging.handlers.SMTPHandler(mailhost=("smtp.gmail.com", 587),
                                                fromaddr="rayadodevelopment@gmail.com",
                                                toaddrs="rayadodevelopment@gmail.com",
                                                subject=u"Schedule-parser error!",
                                                credentials=("rayadodevelopment@gmail.com", "txywpptzayonxlba"),
                                                secure=())
    smtp_handler.setLevel(logging.ERROR)
    smtp_handler.setFormatter(logging.Formatter(log_format))
    return smtp_handler


def get_stream_handler() -> None:
    stream_handler = logging.StreamHandler()
    stream_handler.setLevel(logging.INFO)
    stream_handler.setFormatter(logging.Formatter(log_format))
    return stream_handler


def get_logger(name: str) -> None:
    logger = logging.getLogger(name)
    logger.setLevel(logging.INFO)
    if not DEBUG:
        logger.addHandler(get_smtp_handler())
    logger.addHandler(get_file_handler())
    logger.addHandler(get_stream_handler())
    return logger


logger = get_logger(__name__)
logger.info(f'{USER}')
logger.info(f'{NAME}')
logger.info(f'{HOST}')
logger.info(f'{PASSWORD}')
logger.info(f'{REPEAT_DELAY}')
logger.info(f'{DEBUG}')
