using System;

namespace Меню
{
    class Program
    {
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
        }
    }
}
