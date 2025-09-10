class Program
{
    static void Main()
    {
        while (true)
        {
            int R = ReadInt("R");
            if (R <= 0) throw new Exception("R cannot be 0 or less");

            int x = ReadInt("x"), y = ReadInt("y");

            int res = CheckIfPointIsInBounds(x, y, R);

            string resultMessage = res switch
            {
                1 => "Point is inside",
                -1 => "Point is on border",
                0 => "Point is outside",
                _ => "Unknown result"
            };

            Console.WriteLine(resultMessage); 
        }
    }

    static int ReadInt(string message)
    {
        Console.Write($"{message}: ");
        return Int32.Parse(Console.ReadLine() ?? "0");
    }

    static int CheckIfPointIsInBounds(int x, int y, int R)
    {
        const double eps = 1e-9;

        bool IsOnHalfCircleBorder()
        {
            long dist2 = (long)x * x + (long)y * y;
            long r2 = (long)R * R;
            bool onDiameter = Math.Abs(y) < eps && x >= -R && x <= R;
            bool onArc = Math.Abs(dist2 - r2) < eps && y >= 0;
            return onDiameter || onArc;
        }

        bool IsInsideHalfCircle()
        {
            long dist2 = (long)x * x + (long)y * y;
            long r2 = (long)R * R;
            return y > 0 && dist2 < r2; 
        }

        bool IsOnTriangleBorder()
        {
            bool onAB = Math.Abs(x) < eps && y <= 0 && y >= -R;
            bool onBC = Math.Abs(y + R) < eps && x <= 0 && x >= -R;
            bool onAC = Math.Abs(y - x) < eps && x <= 0 && x >= -R && y <= 0 && y >= -R; 
            return onAB || onBC || onAC;
        }

        bool IsInsideTriangle()
        {
            if (x < -R || x > 0) return false;
            if (y <= -R || y >= x) return false;
            return true;
        }

        if (IsOnHalfCircleBorder() || IsOnTriangleBorder())
            return -1;

        if (IsInsideHalfCircle() || IsInsideTriangle())
            return 1;

        return 0;
    }
}