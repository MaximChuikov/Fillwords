using System;

namespace Меню
{
    class Program
    {
        static void SelectingString(string oldStr, string newStr, int x1, int x2, int oldY, int newY)
        {
            Console.SetCursorPosition(x2, oldY);
            for (int i = x2; i > x1; i--)
            {
                Console.Write("\b \b");
            }
            Console.Write(oldStr);

            Console.SetCursorPosition(x1, newY);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(newStr);
            Console.BackgroundColor = ConsoleColor.Black;

        }
        static void SetCursorAndWrite(string str, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);

        }

        static void DrawFrame(int rightIndent, int upIndent, int width, int lenght)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;


            SetCursorAndWrite(new string(' ', width + 2), rightIndent - 1, upIndent - 1);

            SetCursorAndWrite(new string(' ', width + 2), rightIndent - 1, lenght + upIndent);

            for (int i = 1; i <= lenght; i++)
            {
                SetCursorAndWrite(" ", rightIndent - 1, i + upIndent - 1);
                SetCursorAndWrite(" ", rightIndent + width, i + upIndent - 1);
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        static void Main()
        {
            const int RIGHTINDENT = 2;   // отступ справа, можно менять
            const int UPINDENT = 100;      // отступ сверху
            const int WIDTH = 10;         // ширина
            const int LENGHT = 4;         // длина

            DrawFrame(RIGHTINDENT, UPINDENT, WIDTH, LENGHT);

            string[] menustrings = { "Новая игра", "Продолжить", "Рейтинг", "Выход" };

            for (int i = 0; i < menustrings.Length; i++)
            {
                SetCursorAndWrite(menustrings[i], RIGHTINDENT, UPINDENT + i);
            }
            int selectedLine = 0;
            SelectingString(menustrings[selectedLine], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine, UPINDENT);
            ConsoleKeyInfo key;
            for (; ; )
            {
                Console.SetCursorPosition(RIGHTINDENT - 1, UPINDENT + LENGHT + 1);
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow && selectedLine != 0)
                {
                    selectedLine--;
                    SelectingString(menustrings[selectedLine + 1], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine + 1, UPINDENT + selectedLine);
                }

                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow && selectedLine != LENGHT - 1)
                {
                    selectedLine++;
                    SelectingString(menustrings[selectedLine - 1], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine - 1, UPINDENT + selectedLine);
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (selectedLine)
                    {
                        case 0:
                            Console.WriteLine($"Тут оджнажды будет {menustrings[selectedLine]}");
                            break;
                        case 1:
                            Console.WriteLine($"Тут оджнажды будет {menustrings[selectedLine]}");
                            break;
                        case 2:
                            Console.WriteLine($"Тут оджнажды будет {menustrings[selectedLine]}");
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;


                    }

                }
            }
        }
    }
}
