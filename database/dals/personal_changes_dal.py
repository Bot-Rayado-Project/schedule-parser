from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import PersonalChange


class PersonalChangesDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_personal_changes(self, id: int, stream_group: str, day: str, parity: str, pair_number: int, changes: str) -> None:
        new_change = PersonalChange(id=id, stream_group=stream_group, day=day, parity=parity, pair_number=pair_number, changes=changes)
        self.db_session.add(new_change)
        await self.db_session.flush()

    async def get_personal_changes(self, id: int, stream_group: str, day: str, parity: str, pair_number: int) -> List[PersonalChange]:
        q = await self.db_session.execute(select(PersonalChange.changes).where(PersonalChange.id == id,
                                                                               PersonalChange.stream_group == stream_group,
                                                                               PersonalChange.day == day,
                                                                               PersonalChange.pair_number == pair_number,
                                                                               PersonalChange.parity == parity))
        return q.scalars().all()

    async def delete_all_personal_changes(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(PersonalChange).where(PersonalChange.stream_group == stream_group,
                                                                   PersonalChange.day == day,
                                                                   PersonalChange.parity == parity))

    async def update_personal_changes(self, id: int, stream_group: str, day: str, parity: str, pair_number: int, changes: str) -> None:
        q = update(PersonalChange).where(PersonalChange.id == id,
                                         PersonalChange.stream_group == stream_group,
                                         PersonalChange.day == day,
                                         PersonalChange.pair_number == pair_number,
                                         PersonalChange.parity == parity)
        q = q.values(changes=changes)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
