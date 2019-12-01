#!/usr/bin/env python
from assimilator2 import *

if __name__ == '__main__':
    asm = PymwAssimilator("svt_av1_encoder", assimilator_config.svt_av1_output_path)
    asm.run()
