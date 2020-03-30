import boinc_path_config
from assimilator import *
import assimilator_functions
import glob, os, shutil, MySQLdb
import assimilator_config


class PymwAssimilator(Assimilator):
    """
    PyMW Assimilator. Copies workunit results to a predefined output directory.
    """

    def __init__(self, appname, pymwDir):
        Assimilator.__init__(self, appname)
        self.MySQLHost = assimilator_config.MySQLHost
        self.MySQLDB = assimilator_config.MySQLDB
        self.MySQLUsername = assimilator_config.MySQLUsername
        self.MySQLPassword = assimilator_config.MySQLPassword
        self.pymwDir = pymwDir

    def _copy_to_output(self, result, error_mask=0):
        # validate that the destination path still exists
        if not os.path.exists(self.pymwDir):
            self.logCritical("PyMW path does not exist or is inaccessible: %s\n", \
                             self.pymwDir)
            return

        resultFullPath = self.get_file_path(result)
        resultName = re.search('<open_name>(.*)</open_name>', result.xml_doc_in).group(1)

        # validate that the source path is accessible
        if not error_mask and not os.path.exists(resultFullPath):
            self.logCritical("Result path does not exist or is inaccessible: %s\n", \
                             resultFullPath)
            return

        # copy the file to the output directory where it
        # will be processed by PyMW
        try:
            dest = os.path.join(self.pymwDir, resultName)
            if error_mask:
                dest += ".error"
                f = open(dest, "w")
                try:
                    f.writelines("BOINC error: " + str(error_mask) + "\n")
                    if result.stderr_out:
                        f.writelines("STD ERR: " + result.stderr_out + "\n")
                    f.writelines("For additional information, check: " +
                                 "$project/log_$machine/pymw_assimilator.py.log\n")
                finally:
                    f.close()
                self.logNormal("Error flag created [%s]\n", resultName)
            else:
                shutil.copy2(resultFullPath, dest)
                wu_basename = os.path.basename(dest)
                template_base_name = assimilator_functions.get_workunit_basename(self.appname, wu_basename)
                templates = glob.glob(assimilator_config.templates_path + template_base_name + "*")
                for template in templates:
                    os.remove(template)
                os.chmod(dest, 0777)
                assimilator_functions.assimilate(self.appname, template_base_name)

                self.logNormal("Result copied [%s]\n", resultName)
        except Exception as msg:
            self.logCritical("Error copying output\n" + \
                             "  - Source: %s\n" + \
                             "  - Dest: %s\n" +
                             "  - Error: %s",
                             resultFullPath, dest, msg)

    def assimilate_handler(self, wu, results, canonical_result):
        """
        Assimilates a canonical result by copying the result file
        to the PyMW pickup directory, self.pymwDir
        """
        try:
            # check for valid wu.canonical_result
            if wu.canonical_result:
                self.logNormal("[%s] Found canonical result\n", wu.name)
                self._copy_to_output(canonical_result, wu.error_mask)
            elif wu.error_mask != 0:
                # this is an error
                self.logNormal("[%s] Workunit failed, sending arbitrary result\n", wu.name)
                self._copy_to_output(results[0], wu.error_mask)
                self.logNormal("[%s] No canonical result\n", wu.name)
            else:
                self.logNormal("[%s] No canonical result\n", wu.name)
        except Exception as ex:
            self.logCritical(str(ex))

        # report errors with the workunit
        if self.report_errors(wu):
            pass

    def parse_args(self, args):
        Assimilator.parse_args(self, args)

# allow the module to be executed as an application
