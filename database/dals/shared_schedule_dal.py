from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import SharedSchedule


class SharedScheduleDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_shared_schedule(self, stream_group: str, day: str, parity: str, schedule: str) -> None:
        new_schedule = SharedSchedule(stream_group=stream_group, day=day, parity=parity, schedule=schedule)
        self.db_session.add(new_schedule)
        await self.db_session.flush()

    async def get_shared_schedule(self, stream_group: str, day: str, parity: str) -> List[SharedSchedule]:
        q = await self.db_session.execute(select(SharedSchedule.schedule).where(SharedSchedule.stream_group == stream_group,
                                                                                SharedSchedule.day == day,
                                                                                SharedSchedule.parity == parity))
        return q.scalars().all()

    async def delete_shared_schedule(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(SharedSchedule).where(SharedSchedule.stream_group == stream_group,
                                                                   SharedSchedule.day == day,
                                                                   SharedSchedule.parity == parity))

    async def update_shared_schedule(self, stream_group: str, day: str, parity: str, schedule: str) -> None:
        q = update(SharedSchedule).where(SharedSchedule.stream_group == stream_group,
                                         SharedSchedule.day == day,
                                         SharedSchedule.parity == parity)
        q = q.values(schedule=schedule)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
