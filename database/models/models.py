from sqlalchemy import Column
from sqlalchemy import String
from sqlalchemy import Integer
from sqlalchemy.orm import declarative_base


Base = declarative_base()


class SharedSchedule(Base):
    __tablename__ = 'shared_schedules'

    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    schedule = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [stream_group, day, parity, schedule]
    }


class HeadmanSchedule(Base):
    __tablename__ = 'headman_schedules'

    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    schedule = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [stream_group, day, parity, schedule]
    }


class PersonalSchedule(Base):
    __tablename__ = 'personal_schedules'

    id = Column(Integer, nullable=False)
    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    schedule = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [id, stream_group, day, parity, schedule]
    }


class HeadmanAnnotation(Base):
    __tablename__ = 'headman_annotations'

    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    annotation = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [stream_group, day, parity, annotation]
    }


class PersonalAnnotation(Base):
    __tablename__ = 'personal_annotations'

    id = Column(Integer, nullable=False)
    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    annotation = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [id, stream_group, day, parity, annotation]
    }


class HeadmanChange(Base):
    __tablename__ = 'headman_changes'

    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    pair_number = Column(Integer, nullable=False)
    changes = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [day, parity, pair_number, changes]
    }


class PersonalChange(Base):
    __tablename__ = 'personal_changes'

    id = Column(Integer, nullable=False)
    stream_group = Column(String, nullable=False)
    day = Column(String, nullable=False)
    parity = Column(String, nullable=False)
    pair_number = Column(Integer, nullable=False)
    changes = Column(String, nullable=False)

    __mapper_args__ = {
        "primary_key": [id, day, parity, pair_number, changes]
    }
