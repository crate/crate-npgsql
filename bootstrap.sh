#!/bin/bash

if hash python3.7 2> /dev/null; then
    python3.7 -m venv .venv --without-pip
    curl -s https://bootstrap.pypa.io/get-pip.py -o get-pip.py
    .venv/bin/python get-pip.py
elif hash python3 2> /dev/null; then
    # fallback to python3 in case there is no python3.7
    python3 -m venv .venv
else
    echo 'python3 required'
    exit 1
fi

.venv/bin/python -m pip install -U pip setuptools
.venv/bin/python -m pip install -r requirements.txt
