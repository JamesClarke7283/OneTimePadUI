#!/bin/bash

# Program which creates a virtual environment with dependencies (if not exists). And runs the OneTimePadUI program 
if [ -d venv ]
then
    echo "Found Virtual Environment"
    source venv/bin/activate
else
    echo "No Virtual Environment found"
    python3.10 -m venv .venv
    source .venv/bin/activate
    echo "Installing dependencies..."
    pip3 install -r requirements.txt
fi
echo "Running OneTimePadUI..."
python3.10 main.py
