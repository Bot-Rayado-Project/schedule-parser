import os


class NoneException(Exception):
    pass


DBUSER = os.environ.get('DBUSER')
DBNAME = os.environ.get('DBNAME')
DBHOST = os.environ.get('DBHOST')
DBPASSWORD = os.environ.get('DBPASSWORD')
EADRESS = os.environ.get('EADRESS')
EPASSWORD = os.environ.get('EPASSWORD')
REPEAT_DELAY = os.environ.get('REPEAT_DELAY') or 28800
DEBUG = os.environ.get('DEBUG') or False

if DBUSER is None or DBPASSWORD is None or DBNAME is None or DBHOST is None:
    raise NoneException

STREAMS_IDS: dict = {'бвт': 'it_09.03.01', 'бст': 'it_09.03.02', 'бфи': 'it_02.03.02', 'бэи': 'it_09.03.03',
                     'биб': 'kiib_10.03.01', 'бмп': 'kiib_01.03.04', 'зрс': 'kiib_10.05.02', 'бап': 'kiib_15.03.04', 'бут': 'kiib_27.03.04',
                     'брт': 'rit_11.03.01', 'бик': 'rit_11.03.02',
                     'бин': 'siss_11.03.02',
                     'бби': 'tseimk_38.03.05', 'бэр': 'tseimk_42.03.01', 'бээ': 'tseimk_38.03.01'}
STREAMS: list = ['бвт', 'бст', 'бфи', 'биб', 'бэи', 'бик', 'бмп', 'зрс', 'бап', 'бут', 'брт', 'бээ', 'бби', 'бэр', 'бин']

DAYS = ['понедельник', 'вторник', 'среда', 'четверг', 'пятница', 'суббота']
GROUPS = ['бвт2101', 'бвт2102', 'бвт2103', 'бвт2104', 'бвт2105', 'бвт2106',
          'бвт2107', 'бвт2108', 'бфи2101', 'бфи2102', 'бст2101', 'бст2102',
          'бст2103', 'бст2104', 'бст2105', 'бст2106', 'бэи2101', 'бэи2102',
          'бэи2103', 'биб2101', 'биб2102', 'биб2103', 'биб2104', 'бмп2101',
          'зрс2101', 'зрс2102', 'бап2101', 'бут2101', 'брт2101', 'брт2102',
          'бик2101', 'бик2102', 'бик2103', 'бик2104', 'бик2105', 'бик2106',
          'бик2107', 'бик2108', 'бик2109', 'бээ2101', 'бби2101', 'бэр2101',
          'бин2101', 'бин2102', 'бин2103', 'бин2104', 'бин2105', 'бин2106',
          'бин2107', 'бин2108', 'бин2109', 'бин2110']
WEEKTYPES = ['четная', 'нечетная']

WEEK_COLUMN_GROUPS = {
    'бвт2101': 'D',
    'бвт2102': 'E',
    'бвт2103': 'F',
    'бвт2104': 'G',
    'бвт2105': 'E',
    'бвт2106': 'F',
    'бвт2107': 'G',
    'бвт2108': 'H',
    'бфи2101': 'D',
    'бфи2102': 'E',
    'бст2101': 'D',
    'бст2102': 'E',
    'бст2103': 'F',
    'бст2104': 'D',
    'бст2105': 'E',
    'бст2106': 'F',
    'бэи2101': 'D',
    'бэи2102': 'E',
    'бэи2103': 'F',
    'биб2101': 'D',
    'биб2102': 'J',
    'биб2103': 'P',
    'биб2104': 'V',
    'бмп2101': 'D',
    'зрс2101': 'D',
    'зрс2102': 'J',
    'бап2101': 'D',
    'бут2101': 'D',
    'брт2101': 'D',
    'брт2102': 'E',
    'бик2101': 'D',
    'бик2102': 'E',
    'бик2103': 'F',
    'бик2104': 'D',
    'бик2105': 'E',
    'бик2106': 'F',
    'бик2107': 'D',
    'бик2108': 'E',
    'бик2109': 'F',
    'бби2101': 'D',
    'бээ2101': 'D',
    'бэр2101': 'D',
    'бин2101': 'D',
    'бин2102': 'E',
    'бин2103': 'F',
    'бин2104': 'G',
    'бин2105': 'D',
    'бин2106': 'E',
    'бин2107': 'F',
    'бин2108': 'D',
    'бин2109': 'E',
    'бин2110': 'F'}

TIME = {
    1: '9:30 - 11:05\n',
    2: '11:20 - 12:55\n',
    3: '13:10 - 14:45\n',
    4: '15:25 - 17:00\n',
    5: '17:15 - 18:50\n'}

SUPPLEMENTS = {
    'лек': 'лекция',
    'лаб': 'лабораторная',
    'пр': 'практика',
    'дист': 'дистанционно',
    'очно': 'очно'}
