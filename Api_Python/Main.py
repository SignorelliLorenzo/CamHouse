from logging import exception
from operator import imod
from flask import Flask, request, jsonify, make_response
from flask_restful import Resource, Api, reqparse
import pandas as pd
import speech_recognition as sr
import io
from functools import wraps
from werkzeug.security import generate_password_hash, check_password_hash
from flask_sqlalchemy import SQLAlchemy
import uuid 
import jwt
from datetime import datetime, timedelta

app = Flask(__name__)
app.config['SECRET_KEY']='Th1s1ss3cr3t'
app.config['SQLALCHEMY_DATABASE_URI']='sqlite:///Users.db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = True
db = SQLAlchemy(app)
class User(db.Model):
    Id = db.Column(db.Integer, primary_key = True)
    Public_id = db.Column(db.String(50), unique = True)
    Name = db.Column(db.String(100))
    Email = db.Column(db.String(70), unique = True)
    Password = db.Column(db.String(80))
    Admin=db.Column(db.Boolean,default = False, nullable=False)
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
    def decorated(*args, **kwargs):
        token = None
        # jwt is passed in the request header
        if 'X-Access-Tokens' in request.headers:
            token = request.headers['X-Access-Tokens']
        # return 401 if token is not passed
        if not token:
            return jsonify({'message' : 'Token is missing !!'}), 401
  
        try:
            # decoding the payload to fetch the stored details
            data = jwt.decode(token, app.config['SECRET_KEY'],algorithms=["HS256"])
            current_user = User.query\
                .filter_by(Public_id = data['public_id'])\
                .first()
        except jwt.ExpiredSignatureError:
            return jsonify({
                'message' : 'Token has expired !!'
            }), 401
        except:
            return jsonify({
                'message' : 'Token is invalid !!'
            }), 401
        # returns the current logged in users contex to the routes
        return  f(current_user, *args, **kwargs)
  
    return decorated
@app.route('/Login', methods =['POST'])
def login():
    # creates dictionary of form data
    auth = request.args
  
    if not auth or not auth.get('email') or not auth.get('password'):
        # returns 401 if any email or / and password is missing
        return make_response(
            'Could not verify',
            401,
            {'WWW-Authenticate' : 'Basic realm ="Login required !!"'}
        )
  
    user = User.query\
        .filter_by(Email = auth.get('email'))\
        .first()
  
    if not user:
        # returns 401 if user does not exist
        return make_response(
            'Could not verify',
            401,
            {'WWW-Authenticate' : 'Basic realm ="User does not exist !!"'}
        )
  
    if check_password_hash(user.Password, auth.get('password')):
        # generates the JWT Token
        token = jwt.encode({
            'public_id': user.Public_id,
            'exp' : datetime.utcnow() + timedelta(days = 365)
        }, app.config['SECRET_KEY'])
        return make_response(jsonify({'token' : token}), 201)
    # returns 403 if password is wrong
    return make_response(
        'Could not verify',
        403,
        {'WWW-Authenticate' : 'Basic realm ="Wrong Password !!"'}
    )
  
# REGISTER
@app.route('/Register', methods =['POST'])
@token_required
def register(AuthUser):
    # creates a dictionary of the form data
    if not AuthUser.Admin:
        return make_response('Unauthorized', 401)
    data = request.args
  
    # gets name, email and password
    
    name, email = data.get('name'), data.get('email')
    password = data.get('password')
    admin= (data.get('admin')=='yes')
    if not name or not email or not password or not data.get('admin'):
        return make_response('Missing values', 401)
    if  data.get('admin')!='yes' and  data.get('admin')!='no' :
        return make_response('Value admin can be yes or no', 401)
    
   
    # checking for existing user
    user = User.query\
        .filter_by(Email = email)\
        .first()
    if not user:
        # database ORM object
        user = User(
            Public_id = str(uuid.uuid4()),
            Name = name,
            Email = email,
            Password = generate_password_hash(password),
            Admin=admin
        )
        # insert user
        db.session.add(user)
        db.session.commit()
  
        return make_response('Successfully registered.', 201)
    else:
        # returns 202 if user already exists
        return make_response('User already exists. Please Log in.', 202)
# POST
@app.route('/', methods=['POST'])
@token_required
def Post(data):
    return speech_to_text(io.BytesIO(request.data))

     
if __name__ == '__main__':
    app.run(debug=True)