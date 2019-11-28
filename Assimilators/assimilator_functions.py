import assimilator_config
import MySQLdb
from assimilators import *
from database_functions import *


def generic_wu_basename_select(wu_basename):
    return database_select(wu_basename, "SELECT random_token FROM user_media_files WHERE output_file='%s'")


app_specific_dict = {'ffmpeg_encoder': {'wu_get': generic_wu_basename_select,
                                        'assimilator': generic_assimilator, },
                     'flac_encoder': {'wu_get': generic_wu_basename_select,
                                      'assimilator': generic_assimilator, },
                     'opus_encoder': {'wu_get': generic_wu_basename_select,
                                      'assimilator': generic_assimilator, },
                     'rav1e_encoder': {'wu_get': generic_wu_basename_select,
                                       'assimilator': generic_assimilator, }
                     }


def get_workunit_basename(appname, wu_basename):
    return app_specific_dict[appname]['wu_get'](wu_basename)


def assimilate(appname, wu_basename):
    app_specific_dict[appname]['assimilator'](wu_basename)
