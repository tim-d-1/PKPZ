class Program
{
    /*
    2.1. Використовуючи генератор псевдовипадкових послідовностей чисел задати елементи матриці (матриць)
    в діапазоні -50…+49. Задано дві матриці A(n,n) і B(n,n) n=4. 
    Розробити програму, яка будує матрицю X(n,n) множенням елементів кожного рядка першої матриці 
    на найменше із значень елементів відповідного рядка другої матриці.
    */
    static void Task1()
    {
        Random rnd = new Random();
        int n = 4;
        int[,] A = new int[n, n];
        int[,] B = new int[n, n];
        int[,] X = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                A[i, j] = rnd.Next(-50, 50);
                B[i, j] = rnd.Next(-50, 50);
            }
        }
        for (int i = 0; i < n; i++)
        {
            int minB = B[i, 0];
            for (int j = 1; j < n; j++)
                if (B[i, j] < minB) minB = B[i, j];

            for (int j = 0; j < n; j++)
                X[i, j] = A[i, j] * minB;
        }
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write(X[i, j] + " ");
            Console.WriteLine();
        }
    }

/*
2.2. Дано зубчастий масив з n рядків, у рядках по m[j (j=1..n) елементів. 
Для кожного стовпця підрахувати суму парних додатних елементів і записати дані в новий масив.
*/

    static void Task2()
    {
        Random rnd = new Random();
        int n = rnd.Next(3, 6);
        int[][] arr = new int[n][];
        for (int i = 0; i < n; i++)
        {
            int m = rnd.Next(3, 7);
            arr[i] = new int[m];
            for (int j = 0; j < m; j++) arr[i][j] = rnd.Next(-10, 11);
        }

        int maxCols = 0;
        for (int i = 0; i < arr.Length; i++)
            if (arr[i].Length > maxCols) maxCols = arr[i].Length;

        int[] result = new int[maxCols];
        for (int col = 0; col < maxCols; col++)
        {
            int sum = 0;
            for (int row = 0; row < arr.Length; row++)
                if (col < arr[row].Length && arr[row][col] > 0 && arr[row][col] % 2 == 0)
                    sum += arr[row][col];
            result[col] = sum;
        }

        Console.WriteLine(string.Join(" ", result));
    }

    static void Main()
    {
        Task1();
        Console.ReadLine();
        Task2();
    }
}
