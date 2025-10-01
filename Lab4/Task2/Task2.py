import re

with open("TF_1.txt", "w", encoding="utf-8") as f:
    f.write("Hello, bookkeeper is here. This balloon is big. Cat, dog, apple!")

with open("TF_1.txt", "r", encoding="utf-8") as f:
    text = f.read()

words = re.findall(r"[A-Za-z]+", text)
doubled = [w for w in words if re.search(r"(.)\1", w, re.IGNORECASE)]

with open("TF_2.txt", "w", encoding="utf-8") as f:
    if doubled:
        for w in doubled:
            f.write(w + "\n")
    else:
        f.write("No words with doubled letters\n")

with open("TF_2.txt", "r", encoding="utf-8") as f:
    for line in f:
        print(line.strip())

