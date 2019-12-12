#!/usr/bin/env python
from assimilator2 import *

if __name__ == '__main__':
    asm = PymwAssimilator("mp3packer", assimilator_config.mp3packer_output_path)
    asm.run()
