from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import HeadmanSchedule


class HeadmanScheduleDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_headman_schedule(self, stream_group: str, day: str, parity: str, schedule: str) -> None:
        new_schedule = HeadmanSchedule(id=id, stream_group=stream_group, day=day, parity=parity, schedule=schedule)
        self.db_session.add(new_schedule)
        await self.db_session.flush()

    async def get_headman_schedule(self, stream_group: str, day: str, parity: str) -> List[HeadmanSchedule]:
        q = await self.db_session.execute(select(HeadmanSchedule.schedule).where(HeadmanSchedule.stream_group == stream_group,
                                                                                 HeadmanSchedule.day == day,
                                                                                 HeadmanSchedule.parity == parity))
        return q.scalars().all()

    async def delete_headman_schedule(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(HeadmanSchedule).where(HeadmanSchedule.stream_group == stream_group,
                                                                    HeadmanSchedule.day == day,
                                                                    HeadmanSchedule.parity == parity))

    async def update_headman_schedule(self, stream_group: str, day: str, parity: str, schedule: str) -> None:
        q = update(HeadmanSchedule).where(HeadmanSchedule.stream_group == stream_group,
                                          HeadmanSchedule.day == day,
                                          HeadmanSchedule.parity == parity)
        q = q.values(schedule=schedule)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
