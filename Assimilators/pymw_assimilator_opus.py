#!/usr/bin/env python
from assimilator2 import *

if __name__ == '__main__':
    asm = PymwAssimilator("opus_encoder", assimilator_config.opus_output_path)
    asm.run()
