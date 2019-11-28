import assimilator_config
import MySQLdb


def get_sql_connection():
    return MySQLdb.connect(host=assimilator_config.MySQLHost, db=assimilator_config.MySQLDB,
                           user=assimilator_config.MySQLUsername, passwd=assimilator_config.MySQLPassword)


def update_database(wu_basename, query):
    conn = get_sql_connection()
    cursor = conn.cursor()
    cursor.execute(query % (wu_basename))
    conn.commit()
    conn.close()


def database_select(wu_basename, query):
    conn = get_sql_connection()
    cursor = conn.cursor()
    cursor.execute(query % (wu_basename))
    wu = cursor.fetchall()[0][0]
    conn.close()
    cursor.close()
    return wu
