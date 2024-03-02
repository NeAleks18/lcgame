import os

type = input(
"""
Выберите тип коммита:
1 - [WIP]
2 - [Fix]
3 - [Done]
""")

text = input("Описание коммита: ")

typeResult = "None"

if type == 1: typeResult = "WIP"
elif type == 2: typeResult = "Fix"
elif type == 3: typeResult = "Done"

os.system("git add .")
if typeResult != "None": os.system(f'git commit -m "[{typeResult}] {text}"')
else: os.system(f'git commit -m "{text}"')
os.system("git push")