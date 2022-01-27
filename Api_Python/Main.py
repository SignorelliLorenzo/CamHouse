
from operator import imod
from flask import Flask, request, jsonify, make_response
from flask_restful import Resource, Api, reqparse
import pandas as pd
from flask import request
import ast
import speech_recognition as sr
import sys
import io
from flask_sqlalchemy import SQLAlchemy
from functools import wraps
import jwt
from werkzeug.security import generate_password_hash, check_password_hash

app = Flask(__name__)
api = Api(app)
app.config['SECRET_KEY']='Th1s1ss3cr3t'
app.config['SQLALCHEMY_DATABASE_URI']='sqlite://///home/michael/geekdemos/geekapp/library.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = True
db = SQLAlchemy(app)
class Users(db.Model):
     id = db.Column(db.Integer, primary_key=True)
     public_id = db.Column(db.Integer)
     name = db.Column(db.String(50))
     password = db.Column(db.String(50))
     admin = db.Column(db.Boolean)
def speech_to_text(audio):
    
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
def token_required(f):
   @wraps(f)
   def decorator(*args, **kwargs):

      token = None

      if 'x-access-tokens' in request.headers:
         token = request.headers['x-access-tokens']

      if not token:
         return jsonify("ERRORE:TOKEN NON VALIDO")

      try:
         data = jwt.decode(token, app.config[SECRET_KEY])
         current_user = Users.query.filter_by(public_id=data['public_id']).first()
      except:
         return jsonify({'message': 'token is invalid'})

      return f(current_user, *args, **kwargs)
   return decorator
@app.route('/', methods=['POST'])
@token_required
def Post():
    return speech_to_text(io.BytesIO(request.data))

     
if __name__ == '__main__':
    app.run(debug=True)