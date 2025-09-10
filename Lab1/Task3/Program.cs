using System;

class ChessAttackDefense
{
    struct Pos { public int x, y; public Pos(int x, int y) { this.x = x; this.y = y; } }

    static bool InBounds(Pos p) => p.x >= 1 && p.x <= 8 && p.y >= 1 && p.y <= 8;

    static bool KingAttacks(Pos king, Pos target) =>
        Math.Max(Math.Abs(king.x - target.x), Math.Abs(king.y - target.y)) == 1;

    static bool BishopAttacks(Pos bishop, Pos target, params Pos[] blockers)
    {
        int dx = target.x - bishop.x;
        int dy = target.y - bishop.y;
        if (Math.Abs(dx) != Math.Abs(dy) || dx == 0) return false;

        int stepX = dx > 0 ? 1 : -1;
        int stepY = dy > 0 ? 1 : -1;
        int steps = Math.Abs(dx);
        for (int s = 1; s < steps; s++)
        {
            Pos intermediate = new Pos(bishop.x + s * stepX, bishop.y + s * stepY);
            foreach (var b in blockers)
                if (b.x == intermediate.x && b.y == intermediate.y) return false; // blocked
        }
        return true;
    }

    static bool QueenAttacks(Pos queen, Pos target, params Pos[] blockers)
    {
        int dx = target.x - queen.x;
        int dy = target.y - queen.y;

        if (dx == 0 && dy == 0) return false;
        if (dx == 0)
        {
            int stepY = dy > 0 ? 1 : -1;
            for (int s = 1; s < Math.Abs(dy); s++)
            {
                Pos intermediate = new Pos(queen.x, queen.y + s * stepY);
                foreach (var b in blockers) if (b.x == intermediate.x && b.y == intermediate.y) return false;
            }
            return true;
        }
        else if (dy == 0)
        {
            int stepX = dx > 0 ? 1 : -1;
            for (int s = 1; s < Math.Abs(dx); s++)
            {
                Pos intermediate = new Pos(queen.x + s * stepX, queen.y);
                foreach (var b in blockers) if (b.x == intermediate.x && b.y == intermediate.y) return false;
            }
            return true;
        }
        else if (Math.Abs(dx) == Math.Abs(dy))
        {
            int stepX = dx > 0 ? 1 : -1;
            int stepY = dy > 0 ? 1 : -1;
            int steps = Math.Abs(dx);
            for (int s = 1; s < steps; s++)
            {
                Pos intermediate = new Pos(queen.x + s * stepX, queen.y + s * stepY);
                foreach (var b in blockers) if (b.x == intermediate.x && b.y == intermediate.y) return false;
            }
            return true;
        }
        return false;
    }

    static Pos ReadPos(string pieceName)
    {
        while (true)
        {
            Console.Write($"Enter {pieceName} coordinates as two integers X Y (1..8), e.g. \"4 2\": ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) { Console.WriteLine("Empty input. Try again."); continue; }
            var parts = line.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2 || !int.TryParse(parts[0], out int x) || !int.TryParse(parts[1], out int y))
            {
                Console.WriteLine("Invalid format. Provide two integers.");
                continue;
            }
            return new Pos(x, y);
        }
    }

    static void Main()
    {
        Console.WriteLine("Program: Determine attack/defense/simple move for three pieces:");
        Console.WriteLine("White King, White Bishop, Black Queen.");
        Console.WriteLine("Coordinate system: (1,1) bottom-left. Files and ranks 1..8.");
        Console.WriteLine();

        Pos king = ReadPos("White King");
        Pos bishop = ReadPos("White Bishop");
        Pos blackQueen = ReadPos("Black Queen");

        if (!InBounds(king) || !InBounds(bishop) || !InBounds(blackQueen))
        {
            Console.WriteLine("Error: One or more coordinates are outside the 1..8 board range.");
            return;
        }

        if ((king.x == bishop.x && king.y == bishop.y) ||
            (king.x == blackQueen.x && king.y == blackQueen.y) ||
            (bishop.x == blackQueen.x && bishop.y == blackQueen.y))
        {
            Console.WriteLine("Error: Two or more pieces occupy the same square.");
            return;
        }

        Pos[] allBlockers = { king, bishop, blackQueen };

        Console.WriteLine("\n--- If White King moves first ---");
        {
            if (KingAttacks(king, blackQueen))
            {
                Console.WriteLine("Result: attacks (White King can capture Black Queen).");
            }
            else
            {
                // "defends" if Black Queen attacks White Bishop AND King attacks the Bishop square (i.e., King defends Bishop)
                bool blackQueenAttacksBishop = QueenAttacks(blackQueen, bishop, king); // king may block queen -> include king in blockers
                bool kingDefendsBishop = KingAttacks(king, bishop);
                if (blackQueenAttacksBishop && kingDefendsBishop)
                    Console.WriteLine("Result: defends (Black Queen attacks White Bishop and White King defends the Bishop).");
                else
                    Console.WriteLine("Result: simple move (no attack or defense condition met for King's first move).");
            }
        }

        Console.WriteLine("\n--- If Black Queen moves first ---");
        {
            bool attacksAny = false;
            // queen attacks bishop?
            if (QueenAttacks(blackQueen, bishop, king, bishop)) // include king & bishop as blockers
            {
                Console.WriteLine("Result: attacks White Bishop (Black Queen threatens/captures the White Bishop).");
                attacksAny = true;
            }
            // queen attacks king?
            if (QueenAttacks(blackQueen, king, king, bishop))
            {
                Console.WriteLine("Result: attacks White King (Black Queen threatens/captures the White King).");
                attacksAny = true;
            }
            if (!attacksAny) Console.WriteLine("Result: simple move (Black Queen does not attack White King or White Bishop).");
        }

        Console.WriteLine("\n--- If White Bishop moves first ---");
        {
            // bishop attacks black queen?
            if (BishopAttacks(bishop, blackQueen, king, blackQueen))
            {
                Console.WriteLine("Result: attacks (White Bishop can capture Black Queen).");
            }
            else
            {
                // "defends" if Black Queen attacks White King AND Bishop attacks King square (bishop defends king)
                bool blackQueenAttacksKing = QueenAttacks(blackQueen, king, king, bishop);
                bool bishopDefendsKing = BishopAttacks(bishop, king, king, blackQueen);
                if (blackQueenAttacksKing && bishopDefendsKing)
                    Console.WriteLine("Result: defends (Black Queen attacks White King and White Bishop defends the King).");
                else
                    Console.WriteLine("Result: simple move (no attack or defense condition met for Bishop's first move).");
            }
        }
    }
}
