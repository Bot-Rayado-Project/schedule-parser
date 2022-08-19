from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import HeadmanAnnotation


class HeadmanAnnotationsDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_headman_annotation(self, stream_group: str, day: str, parity: str, annotation: str) -> None:
        new_annotation = HeadmanAnnotation(stream_group=stream_group, day=day, parity=parity, annotation=annotation)
        self.db_session.add(new_annotation)
        await self.db_session.flush()

    async def get_headman_annotation(self, stream_group: str, day: str, parity: str) -> List[HeadmanAnnotation]:
        q = await self.db_session.execute(select(HeadmanAnnotation.annotation).where(HeadmanAnnotation.stream_group == stream_group,
                                                                                     HeadmanAnnotation.day == day,
                                                                                     HeadmanAnnotation.parity == parity))
        return q.scalars().all()

    async def delete_headman_annotation(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(HeadmanAnnotation).where(HeadmanAnnotation.stream_group == stream_group,
                                                                      HeadmanAnnotation.day == day,
                                                                      HeadmanAnnotation.parity == parity))

    async def update_headman_annotation(self, stream_group: str, day: str, parity: str, annotation: str) -> None:
        q = update(HeadmanAnnotation).where(HeadmanAnnotation.stream_group == stream_group,
                                            HeadmanAnnotation.day == day,
                                            HeadmanAnnotation.parity == parity)
        q = q.values(annotation=annotation)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
