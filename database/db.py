from typing import Any, Tuple, Union
from parser.utils.logger import get_logger
from parser.utils.constants import DBUSER, DBPASSWORD, DBHOST, DBPORT, DBNAME
from database.dals.shared_schedule_dal import SharedScheduleDAL
from database.dals.headman_schedule_dal import HeadmanScheduleDAL
from database.dals.personal_schedule_dal import PersonalScheduleDAL
from database.dals.headman_annotations_dal import HeadmanAnnotationsDAL
from database.dals.personal_annotations_dal import PersonalAnnotationsDAL
from database.dals.headman_changes_dal import HeadmanChangesDAL
from database.dals.personal_changes_dal import PersonalChangesDAL

import jellyfish

from sqlalchemy.ext.asyncio import create_async_engine, AsyncSession
from sqlalchemy.orm import sessionmaker

logger = get_logger(__name__)


url = f"postgresql+asyncpg://{DBUSER}:{DBPASSWORD}@{DBHOST}:{DBPORT}/{DBNAME}"

engine = create_async_engine(url, future=True, echo=False)
async_session = sessionmaker(engine, expire_on_commit=False, class_=AsyncSession)


async def db_write_schedule(stream_group: str, day: str, parity: str, schedule: Union[str, Tuple[Any, ...], bool]) -> None:
    '''Записывает расписание для нужной группы, дня недели, четной или нечетной недели'''
    async with async_session() as session:
        async with session.begin():
            shared_schedule_dal = SharedScheduleDAL(session)
            database_responce: list = await shared_schedule_dal.get_shared_schedule(stream_group, day, parity)

            # Удаление изменений старосты
            headman_schedule_dal = HeadmanScheduleDAL(session)
            await headman_schedule_dal.delete_headman_schedule(stream_group, day, parity)

            # Удаление изменений любого человека
            personal_schedule_dal = PersonalScheduleDAL(session)
            await personal_schedule_dal.delete_all_personal_schedule(stream_group, day, parity)

            # Удаление аннотаций старосты
            headman_annotations_dal = HeadmanAnnotationsDAL(session)
            await headman_annotations_dal.delete_headman_annotation(stream_group, day, parity)

            # Удаление аннотаций любого человека
            personal_annotations_dal = PersonalAnnotationsDAL(session)
            await personal_annotations_dal.delete_all_personal_annotation(stream_group, day, parity)

            # Удаление изменений пар старосты
            headman_changes_dal = HeadmanChangesDAL(session)
            await headman_changes_dal.delete_all_headman_changes(stream_group, day, parity)

            # Удаление изменений пар любого человека
            personal_changes_dal = PersonalChangesDAL(session)
            await personal_changes_dal.delete_all_personal_changes(stream_group, day, parity)

            if len(database_responce) == 0:
                await shared_schedule_dal.create_shared_schedule(stream_group, day, parity, schedule)
                logger.info(f'Успешно записано {day}, {stream_group}, {parity}')

            else:
                if database_responce[0] != schedule:
                    logger.info(f'Сходство строк: {jellyfish.jaro_similarity(schedule, str(database_responce[0]))}')
                    await shared_schedule_dal.update_shared_schedule(stream_group, day, parity, schedule)
                    logger.info(f'Успешно перезаписано {day}, {stream_group}, {parity}')
