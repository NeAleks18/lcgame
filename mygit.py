import os

type = input("Напиши тип коммита: ")

text = input("Описание коммита: ")

os.system("git add .")
os.system(f'git commit -m "[{type}] {text}"')
os.system("git push")