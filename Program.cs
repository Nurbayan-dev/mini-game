using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        const int size = 14;
        char[,] map = new char[size, size];
        bool[,] obstacles = new bool[size, size]; // Добавляем массив для препятствий
        int treasureX, treasureY, playerX, playerY;

        // Заполняем карту и добавляем препятствия
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                map[i, j] = '⭒';
                obstacles[i, j] = false;
            }
        }

        // Размещаем сокровище и игрока
        do
        {
            treasureX = random.Next(0, size);
            treasureY = random.Next(0, size);
            playerX = random.Next(0, size);
            playerY = random.Next(0, size);
        } while (treasureX == playerX && treasureY == playerY);

        map[treasureX, treasureY] = '?';
        map[playerX, playerY] = '!';

        // Размещаем препятствия
        for (int i = 0; i < size * 2; i++)
        {
            int obstacleX = random.Next(0, size);
            int obstacleY = random.Next(0, size);
            if (obstacleX != treasureX || obstacleY != treasureY)
            {
                obstacles[obstacleX, obstacleY] = true;
            }
        }

        void PrintMap()
        {
            Console.Clear();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (map[i, j] == '!')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (map[i, j] == '?')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (obstacles[i, j]) // Проверяем, есть ли препятствие на данной клетке
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("☐ ");
                        Console.ResetColor();
                        continue;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        Console.WriteLine("Ahoy! Here be the map. Find the treasure '?'. Ye are '!'.");
        PrintMap();
        Console.WriteLine("Move with ◄ (left), ► (right), ▲ (up), ▼ (down).");

        while (true)
        {
            if (playerX == treasureX && playerY == treasureY)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You are winner! Ye found the treasure!");
                Console.ResetColor();
                break;
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                int newPlayerX = playerX;
                int newPlayerY = playerY;
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        newPlayerY = playerY - 1;
                        break;
                    case ConsoleKey.RightArrow:
                        newPlayerY = playerY + 1;
                        break;
                    case ConsoleKey.UpArrow:
                        newPlayerX = playerX - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        newPlayerX = playerX + 1;
                        break;
                    default:
                        continue;
                }
              
                if (newPlayerX >= 0 && newPlayerX < size && newPlayerY >= 0 && newPlayerY < size && !obstacles[newPlayerX, newPlayerY])
                {
                    map[playerX, playerY] = '⭒';
                    playerX = newPlayerX;
                    playerY = newPlayerY;
                    map[playerX, playerY] = '!'; 
                }
                PrintMap();
            }
        }
    }
}