
from flask import Flask
from flask_restful import Resource, Api, reqparse
import pandas as pd
from flask import request
import ast
import speech_recognition as sr
import sys
import io


app = Flask(__name__)
api = Api(app)

def STT(audio):
    
    # inizializzi il recognizer
    r = sr.Recognizer()

    # apri il file
    with sr.AudioFile(audio) as source:
        # ascolta i dati (carica l'audio in memoria)
        audio_data = r.record(source)
        # riconosce (converte l'audio in testo)
        text = r.recognize_google(audio_data, language="it-IT")
        return text
    #per avviare: python ./Speech_Recognition_ByAudioFile.py AudioIta.wav

@app.route('/', methods=['POST'])
def Post():
    return STT(io.BytesIO(request.data))

     
if __name__ == '__main__':
    app.run(debug=True)