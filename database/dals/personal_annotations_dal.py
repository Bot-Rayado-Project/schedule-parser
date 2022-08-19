from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import PersonalAnnotation


class PersonalAnnotationsDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_personal_annotation(self, id: int, stream_group: str, day: str, parity: str, annotation: str) -> None:
        new_annotation = PersonalAnnotation(id=id, stream_group=stream_group, day=day, parity=parity, annotation=annotation)
        self.db_session.add(new_annotation)
        await self.db_session.flush()

    async def get_personal_annotation(self, id: int, stream_group: str, day: str, parity: str) -> List[PersonalAnnotation]:
        q = await self.db_session.execute(select(PersonalAnnotation.annotation).where(PersonalAnnotation.id == id,
                                                                                      PersonalAnnotation.stream_group == stream_group,
                                                                                      PersonalAnnotation.day == day,
                                                                                      PersonalAnnotation.parity == parity))
        return q.scalars().all()

    async def delete_all_personal_annotation(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(PersonalAnnotation).where(PersonalAnnotation.stream_group == stream_group,
                                                                       PersonalAnnotation.day == day,
                                                                       PersonalAnnotation.parity == parity))

    async def update_personal_annotation(self, id: int, stream_group: str, day: str, parity: str, annotation: str) -> None:
        q = update(PersonalAnnotation).where(PersonalAnnotation.id == id,
                                             PersonalAnnotation.stream_group == stream_group,
                                             PersonalAnnotation.day == day,
                                             PersonalAnnotation.parity == parity)
        q = q.values(annotation=annotation)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
