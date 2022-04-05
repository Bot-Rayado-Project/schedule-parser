from parser.schedule.streams.IT.bvt import get_schedule_bvt
from parser.schedule.streams.IT.bst import get_schedule_bst
from parser.schedule.streams.IT.bei import get_schedule_bei
from parser.schedule.streams.IT.bfi import get_schedule_bfi
from parser.schedule.streams.KIIB.bap import get_schedule_bap
from parser.schedule.streams.KIIB.bib import get_schedule_bib
from parser.schedule.streams.KIIB.bmp import get_schedule_bmp
from parser.schedule.streams.KIIB.but import get_schedule_but
from parser.schedule.streams.RIT.bik import get_schedule_bik
from parser.schedule.streams.RIT.brt import get_schedule_brt
from parser.schedule.streams.SISS.bin import get_schedule_bin
from parser.schedule.streams.TCEIMK.bbi import get_schedule_bbi
from parser.schedule.streams.TCEIMK.bee import get_schedule_bee
from parser.schedule.streams.TCEIMK.ber import get_schedule_ber

GROUP_MATCHING_SCHEDULE = {
    'бвт': get_schedule_bvt,
    'бст': get_schedule_bst,
    'бэи': get_schedule_bei,
    'бфи': get_schedule_bfi,
    'бап': get_schedule_bap,
    'биб': get_schedule_bib,
    'бмп': get_schedule_bmp,
    'бут': get_schedule_but,
    'бик': get_schedule_bik,
    'брт': get_schedule_brt,
    'бби': get_schedule_bbi,
    'бээ': get_schedule_bee,
    'бэр': get_schedule_ber,
    'бин': get_schedule_bin
}
