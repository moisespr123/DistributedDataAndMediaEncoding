#!/usr/bin/env python
from assimilator2 import *

if __name__ == '__main__':
    asm = PymwAssimilator("rav1e_encoder", assimilator_config.rav1e_output_path)
    asm.run()
