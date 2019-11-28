from database_functions import *


def generic_assimilator(wu_basename):
    update_database(wu_basename, "UPDATE user_media_files SET processed=1 WHERE random_token='%s'")
