from typing import List

from sqlalchemy import update
from sqlalchemy import select, delete
from sqlalchemy.orm import Session

from database.models.models import PersonalSchedule


class PersonalScheduleDAL:
    def __init__(self, db_session: Session):
        self.db_session = db_session

    async def create_personal_schedule(self, id: int, stream_group: str, day: str, parity: str, schedule: str) -> None:
        new_schedule = PersonalSchedule(id=id, stream_group=stream_group, day=day, parity=parity, schedule=schedule)
        self.db_session.add(new_schedule)
        await self.db_session.flush()

    async def get_personal_schedule(self, id: int, stream_group: str, day: str, parity: str) -> List[PersonalSchedule]:
        q = await self.db_session.execute(select(PersonalSchedule.schedule).where(PersonalSchedule.id == id,
                                                                                  PersonalSchedule.stream_group == stream_group,
                                                                                  PersonalSchedule.day == day,
                                                                                  PersonalSchedule.parity == parity))
        return q.scalars().all()

    async def delete_all_personal_schedule(self, stream_group: str, day: str, parity: str):
        await self.db_session.execute(delete(PersonalSchedule).where(PersonalSchedule.stream_group == stream_group,
                                                                     PersonalSchedule.day == day,
                                                                     PersonalSchedule.parity == parity))

    async def update_personal_schedule(self, id: int, stream_group: str, day: str, parity: str, schedule: str) -> None:
        q = update(PersonalSchedule).where(PersonalSchedule.id == id,
                                           PersonalSchedule.stream_group == stream_group,
                                           PersonalSchedule.day == day,
                                           PersonalSchedule.parity == parity)
        q = q.values(schedule=schedule)
        q.execution_options(synchronize_session="fetch")
        await self.db_session.execute(q)
