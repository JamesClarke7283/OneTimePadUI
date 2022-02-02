from json_encrypt import json_decrypt, json_encrypt
import json


def save_keys(password, keys, path):
    ciphertext = json_encrypt(password, json.dumps({"keys": keys}))
    with open(path, 'w') as f:
        f.write(ciphertext)


def load_keys(password, path):
    with open(path, 'r') as f:
        json_ciphertext = f.read()
    plaintext = json_decrypt(password, json_ciphertext)
    return plaintext