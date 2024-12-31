from fastapi import FastAPI, Depends, HTTPException, status, Body
from fastapi.security import OAuth2PasswordBearer
from jose import JWTError, jwt
from passlib.context import CryptContext
from typing import List
import json
import os

app = FastAPI()

SECRET_KEY = "Você é gay"
ALGORITHM = "HS256"
ACCESS_TOKEN_EXPIRE_MINUTES = 30

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="token")

fake_users_db = {}

def verify_password(plain_password, hashed_password):
    try:
        return pwd_context.verify(plain_password, hashed_password)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Error verifying password") from e

def get_password_hash(password):
    try:
        return pwd_context.hash(password)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Error hashing password") from e

def create_access_token(data: dict):
    try:
        to_encode = data.copy()
        encoded_jwt = jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)
        return encoded_jwt
    except Exception as e:
        raise HTTPException(status_code=500, detail="Error creating access token") from e

def decode_access_token(token: str):
    try:
        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])
        return payload
    except JWTError:
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Invalid token",
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail="Error decoding access token") from e

@app.post("/signup")
def signup(data: dict = Body(...)):
    try:
        if not isinstance(data, dict):
            raise HTTPException(status_code=400, detail="Invalid request format")

        username = data.get("username")
        password = data.get("password")

        if not username or not password:
            raise HTTPException(status_code=400, detail="Username and password are required")

        if username in fake_users_db:
            raise HTTPException(status_code=400, detail="User already exists")

        hashed_password = get_password_hash(password)
        fake_users_db[username] = {"username": username, "password": hashed_password}
        return {"msg": "User created successfully"}
    except KeyError:
        raise HTTPException(status_code=400, detail="Invalid signup data")
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal server error") from e

@app.post("/login")
def login(login_data: dict = Body(...)):
    try:
        username = login_data.get("username")
        password = login_data.get("password")

        if not username or not password:
            raise HTTPException(status_code=400, detail="Username and password are required")

        user = fake_users_db.get(username)
        if not user or not verify_password(password, user["password"]):
            raise HTTPException(status_code=401, detail="Invalid credentials")

        token = create_access_token(data={"sub": username})
        return {"access_token": token, "token_type": "bearer"}
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal server error") from e

@app.get("/protected")
def protected_endpoint(token: str = Depends(oauth2_scheme)):
    try:
        payload = decode_access_token(token)
        username = payload.get("sub")
        if not username:
            raise HTTPException(status_code=401, detail="Invalid token")

        if not os.path.exists("data.json"):
            raise HTTPException(status_code=404, detail="Data file not found")

        with open("data.json", "r") as fp:
            try:
                data = json.load(fp)
            except json.JSONDecodeError:
                raise HTTPException(status_code=500, detail="Error decoding JSON data")

        return {"data": data}
    except FileNotFoundError:
        raise HTTPException(status_code=404, detail="Data file not found")
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal server error") from e
