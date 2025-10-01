from asyncio.base_tasks import _task_get_stack
import re

def task1():
    text = "I met Dr. Brown in N.Y. He likes colors: red, blue, green. Also, Mr. Black is here."
    matches = re.findall(r"\b\w+\.", text)
    print("Found:", matches)
    print("Count:", len(matches))

    subtext = input("Enter subtext to remove: ")
    text = re.sub(rf"\b{subtext}\b", "", text)
    /
    old = input("Enter subtext to replace: ")
    new = input("Enter new value: ")
    text = re.sub(rf"\b{old}\b", new, text)

    print("Updated text:", text)

def task2():
    text1 = "cat dog fish"
    text2 = "The cat chased the dog, but not the fish or bird."

    words1 = set(re.findall(r"\b\w+\b", text1.lower()))
    words2 = re.findall(r"\b\w+\b|\W+", text2)

    result = "".join([w for w in words2 if w.lower() not in words1])
    print(result.strip())

task1()
task2()