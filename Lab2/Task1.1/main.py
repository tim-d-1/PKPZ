
'''
Задано масив. Визначити кількість елементів масиву, що перевищують 
середньоарифметичне всіх елементів масиву та вивести їх на екран. 
'''
def task1():
    n = int(input())
    arr = list(map(int, input().split()))
    avg = sum(arr) / n
    res = [x for x in arr if x > avg]
    print("Amount of elements: {}".format(len(res)))
    print(*res)


'''
Глядацька зала має 10 рядів по 15 місць. 
У двовимірному масиві зберігається інформація про продані місця: 
якщо елемент масиву дорівнює 1, то місце вважається зайнятим, якщо 0 – вільним.
Написати програму, що визначає, чи є вільні місця в обраному користувачем ряді.
'''
def task2():
    import random
    rows, cols = 10, 15
    hall = [[random.randint(0, 1) for _ in range(cols)] for _ in range(rows)]
    r = int(input()) - 1
    if 0 in hall[r]:
        print("Vacant spots are present")
    else:
        print("There're no vacant spots left :(")
    for i in range(rows):
        print(*(hall[i]))

if __name__ == '__main__':
    task1()
    print("= Task2: ")
    task2()