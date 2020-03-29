#!/usr/bin/env python
from assimilator2 import *

if __name__ == '__main__':
    asm = PymwAssimilator("paq_compressor", assimilator_config.paq_output_path)
    asm.run()
