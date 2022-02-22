import unittest
import Main
import io
import logging
class TestSTT(unittest.TestCase):
    def test_stt(self):
        with open('AudioIta.wav', 'rb') as fh:
            buf = io.BytesIO(fh.read())
        x=Main.speech_to_text(buf)
        assert x=="Chat One come due gocce d'acqua Elena e Caterina hanno 37 anni e sono sorelle non sorelle qualsiasi sono Infatti gemelle"
    def test_stt_error(self):
        with open('error.m4a', 'rb') as fh:
            buf = io.BytesIO(fh.read())
        x=Main.speech_to_text(buf)
        assert x.startswith("Error:")
if __name__ == '__main__':
    unittest.main()

