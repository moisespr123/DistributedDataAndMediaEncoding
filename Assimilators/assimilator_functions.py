import assimilator_config
import MySQLdb
import os
from assimilators import *
from av1_assimilator import av1_assimilator
from database_functions import *


def generic_wu_basename_select(wu_basename):
    return database_select(wu_basename, "SELECT random_token FROM user_media_files WHERE output_file='%s'")

def get_input_filename(random_token):
    return database_select_multi(random_token, "SELECT input_file FROM user_media_files WHERE random_token='%s'")


app_specific_dict = {'ffmpeg_encoder': {'wu_get': generic_wu_basename_select,
                                        'assimilator': generic_assimilator, },
                     'flac_encoder': {'wu_get': generic_wu_basename_select,
                                      'assimilator': generic_assimilator, },
                     'mp3packer': {'wu_get': generic_wu_basename_select,
                                      'assimilator': generic_assimilator, },
                     'opus_encoder': {'wu_get': generic_wu_basename_select,
                                      'assimilator': generic_assimilator, },
                     'rav1e_encoder': {'wu_get': generic_wu_basename_select,
                                       'assimilator': av1_assimilator, },
                     'svt_av1_encoder': {'wu_get': generic_wu_basename_select,
                                       'assimilator': av1_assimilator, }
                     }


def get_workunit_basename(appname, wu_basename):
    return app_specific_dict[appname]['wu_get'](wu_basename)


def assimilate(appname, wu_basename):
    app_specific_dict[appname]['assimilator'](wu_basename)


def delete_raw_files(random_token):
    files = get_input_filename(random_token)
    for file in files:
        os.remove(assimilator_config.raw_files_path + file[0])
