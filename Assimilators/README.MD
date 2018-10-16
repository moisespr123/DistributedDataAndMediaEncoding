# BOINC Assimilators

These are the scripts that will move the result files to their respective folders and update the MySQL database table accordingly. They are a slightly modified version of the `pymw_assimilator.py` script that's available in the `bin` folder of a project server.

You'll need to modify the `assimilator_config.py` file to point to the correct paths and enter your MySQL server information.

To add the assimilators, copy the `script_assimilator` program and rename them to their respective app assimilator name. Then, modify your `config.xml` file to add the following information:

## FLAC assimilator
```
<daemon>
    <cmd>flac_assimilator --app flac_encoder -d 3 --script "pymw_assimilator_flac.py"</cmd>
</daemon>
```

## Opus assimilator
```
<daemon>
    <cmd>opus_assimilator --app opus_encoder -d 3 --script "pymw_assimilator_opus.py"</cmd>
</daemon>
```

## rav1e assimilator
```
<daemon>
    <cmd>rav1e_assimilator --app rav1e_encoder -d 3 --script "pymw_assimilator_rav1e.py"</cmd>
</daemon>
```
