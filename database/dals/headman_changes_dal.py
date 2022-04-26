from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import HeadmanChange


class HeadmanChangesDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_headman_changes(self, stream_group: str, day: str, parity: str, pair_number: int, changes: str) -> None:
        new_change = HeadmanChange(stream_group=stream_group, day=day, parity=parity, pair_number=pair_number, changes=changes)
        self.db_session.add(new_change)
        await self.db_session.flush()

    async def get_headman_changes(self, stream_group: str, day: str, parity: str, pair_number: int) -> List[HeadmanChange]:
        q = await self.db_session.execute(select(HeadmanChange.changes).where(HeadmanChange.stream_group == stream_group,
                                                                              HeadmanChange.day == day,
                                                                              HeadmanChange.pair_number == pair_number,
                                                                              HeadmanChange.parity == parity))
        return q.scalars().all()

    async def delete_all_headman_changes(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(HeadmanChange).where(HeadmanChange.stream_group == stream_group,
                                                                  HeadmanChange.day == day,
                                                                  HeadmanChange.parity == parity))

    async def update_headman_changes(self, stream_group: str, day: str, parity: str, pair_number: int, changes: str) -> None:
        q = update(HeadmanChange).where(HeadmanChange.stream_group == stream_group,
                                        HeadmanChange.day == day,
                                        HeadmanChange.pair_number == pair_number,
                                        HeadmanChange.parity == parity)
        q = q.values(changes=changes)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
