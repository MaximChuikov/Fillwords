using System;
using System.IO;

namespace Меню
{
    class Field
    {
        protected const int XLENGHT = 10;
        protected const int YLENGHT = 8;
        private static char[,] field = new char[XLENGHT, YLENGHT];
        //string[] words = File.ReadAllLines("words.txt");
        private string[] words = { "кит", "лодка", "ноутбук", "конь", "знак", "телевизор", "мышь", "экран",
                                "клавиатура", "программа", "каркас", "окружность", "цвет", "куб", "нога","дуб","лень","сон","чай",
                                "кофе","ложь"};

        private bool CellIsFree(int x, int y) //свободна ли ячейка и не превышены ли края поля
        {
            return x <= XLENGHT && y <= YLENGHT && x >= 0 && y >= 0 && field[x, y] == null;
        }
        private int RandomDirection(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }

        public static char[,] FillField()
        {



            return Field.field;
        }

        private bool ThreePointsAroundIsFree(int x, int y)
        {
            if (CellIsFree(x + 1, y))
            {//иду вправо
                if (CellIsFree(x + 1, y + 1))
                {//иду вправо-вверх
                    if (CellIsFree(x, y + 1))
                    {//иду вправо-вверх-влево
                        return true;
                    }
                    else if (CellIsFree(x + 1, y + 2))
                    {//иду вправо-вверх-вверх
                        return true;
                    }
                    else if (CellIsFree(x + 2, y + 1))
                    {//иду вправо-вверх-вправо
                        return true;
                    }
                }
                if (CellIsFree(x + 2, y))
                {//иду вправо-вправо
                    if (CellIsFree(x + 2, y + 1))
                    {//иду вправо-вправо-вверх
                        return true;
                    }
                    else if (CellIsFree(x + 3, y))
                    {//иду вправо-вправо-вправо
                        return true;
                    }
                    else if (CellIsFree(x + 2, y - 1))
                    {//иду вправо-вправо-вниз
                        return true;
                    }
                }
                if (CellIsFree(x + 1, y - 1))
                {//иду вправо-вниз
                    if (CellIsFree(x + 2, y - 1))
                    {//иду вправо-вниз-вправо
                        return true;
                    }
                    else if (CellIsFree(x + 1, y - 2))
                    {//иду вправо-вниз-вниз
                        return true;
                    }
                    else if (CellIsFree(x, y - 1))
                    {//иду вправо-вниз-влево
                        return true;
                    }
                }
            }
            else if (CellIsFree(x, y - 1))
            {//иду вниз
                if (CellIsFree(x + 1, y - 1))
                {//иду вниз-вправо
                    if (CellIsFree(x + 1, y))
                    {//иду вниз-вправо-вверх
                        return true;
                    }
                    else if (CellIsFree(x + 2, y - 1))
                    {//иду вниз-вправо-вправо
                        return true;
                    }
                    else if (CellIsFree(x + 1, y - 2))
                    {//иду вниз-вправо-вниз
                        return true;
                    }
                }
                if (CellIsFree(x, y - 2))
                {//иду вниз-вниз
                    if (CellIsFree(x + 1, y - 2))
                    {//иду вниз-вниз-вправо
                        return true;
                    }
                    else if (CellIsFree(x, y - 3))
                    {//иду вниз-вниз-вниз
                        return true;
                    }
                    else if (CellIsFree(x - 1, y - 2))
                    {//иду вниз-вниз-влево
                        return true;
                    }
                }
                if (CellIsFree(x - 1, y - 1))
                {//иду вниз-влево
                    if (CellIsFree(x - 1, y - 2))
                    {//иду вниз-влево-вниз
                        return true;
                    }
                    else if (CellIsFree(x - 2, y - 1))
                    {//иду вниз-влево-влево
                        return true;
                    }
                    else if (CellIsFree(x - 1, y))
                    {//иду вниз-влево-вверх
                        return true;
                    }
                }
            }
            else if (CellIsFree(x - 1, y))
            {//иду влево
                if (CellIsFree(x - 1, y - 1))
                {//иду влево-вниз
                    if (CellIsFree(x, y - 1))
                    {//иду влево-вниз-вправо
                        return true;
                    }
                    else if (CellIsFree(x - 1, y - 2))
                    {//иду влево-вниз-вниз
                        return true;
                    }
                    else if (CellIsFree(x - 2, y - 1))
                    {//иду влево-вниз-влево
                        return true;
                    }
                }
                if (CellIsFree(x - 2, y))
                {//иду влево-влево
                    if (CellIsFree(x - 2, y - 1))
                    {//иду влево-влево-вниз
                        return true;
                    }
                    else if (CellIsFree(x - 3, y))
                    {//иду влево-влево-влево
                        return true;
                    }
                    else if (CellIsFree(x - 2, y + 1))
                    {//иду влево-влево-вверх
                        return true;
                    }
                }
                if (CellIsFree(x - 1, y + 1))
                {//иду влево-вверх
                    if (CellIsFree(x - 2, y + 1))
                    {//иду влево-вверх-влево
                        return true;
                    }
                    else if (CellIsFree(x - 1, y + 2))
                    {//иду влево-вверх-вверх
                        return true;
                    }
                    else if (CellIsFree(x, y + 1))
                    {//иду влево-вверх-вправо
                        return true;
                    }
                }
            }
            else if (CellIsFree(x, y + 1))
            {//иду вверх
                if (CellIsFree(x - 1, y + 1))
                {//иду вверх-влево
                    if (CellIsFree(x - 1, y))
                    {//иду вверх-влево-вниз
                        return true;
                    }
                    else if (CellIsFree(x - 2, y + 1))
                    {//иду вверх-влево-влево
                        return true;
                    }
                    else if (CellIsFree(x - 1, y + 2))
                    {//иду вверх-влево-вверх
                        return true;
                    }
                }
                if (CellIsFree(x, y + 2))
                {//иду вверх-вверх
                    if (CellIsFree(x - 1, y + 2))
                    {//иду вверх-вверх-влево
                        return true;
                    }
                    else if (CellIsFree(x, y + 3))
                    {//иду вверх-вверх-вверх
                        return true;
                    }
                    else if (CellIsFree(x + 1, y + 2))
                    {//иду вверх-вверх-вправо
                        return true;
                    }
                }
                if (CellIsFree(x + 1, y + 1))
                {//иду вверх-вправо
                    if (CellIsFree(x + 1, y + 2))
                    {//иду вверх-вправо-вверх
                        return true;
                    }
                    else if (CellIsFree(x + 2, y + 1))
                    {//иду вверх-вправо-вправо
                        return true;
                    }
                    else if (CellIsFree(x + 1, y))
                    {//иду вверх-вправо-вниз
                        return true;
                    }
                }
            }
            return false;
        }
    }
    class Game
    {   
        public void NewGame()
        {
            Console.WriteLine("Введите ваш никнейм");
            string playerName = Console.ReadLine();

            

            //начало алгоритма. начну с 0,0
            int x = 0;
            int y = 0;




            Environment.Exit(0);
        }
    }
    class Menu
    {
        static public void SelectingString(string oldStr, string newStr, int x1, int x2, int oldY, int newY)
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


        static public void SetCursorAndWrite(string str, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);

        }

        static public void DrawFrame(int rightIndent, int upIndent, int width, int lenght)
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
    }
    class Program
    {
        

        static void Main()
        {
            const int RIGHTINDENT = 1;   // отступ справа
            const int UPINDENT = 1;      // отступ сверху
            const int WIDTH = 10;         // ширина
            const int LENGHT = 4;         // длина

            Menu.DrawFrame(RIGHTINDENT,UPINDENT,WIDTH,LENGHT);

            string[] menustrings = { "Новая игра", "Продолжить", "Рейтинг", "Выход" };

            for (int i = 0; i < menustrings.Length; i++)
            {
                Menu.SetCursorAndWrite(menustrings[i], RIGHTINDENT, UPINDENT + i);
            }

            int selectedLine = 0;
            Menu.SelectingString(menustrings[selectedLine], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine, UPINDENT);
            ConsoleKeyInfo key;
            Game game = new Game();
            for ( ; ; )
            {
                Console.SetCursorPosition(RIGHTINDENT-1,UPINDENT+LENGHT+1);
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow && selectedLine != 0)
                {
                    selectedLine--;
                    Menu.SelectingString(menustrings[selectedLine + 1], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine + 1, UPINDENT + selectedLine);
                }

                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow && selectedLine != LENGHT - 1)
                {
                    selectedLine++;
                    Menu.SelectingString(menustrings[selectedLine-1],menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine-1, UPINDENT + selectedLine);
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (selectedLine)
                    {
                        case 0:
                            game.NewGame();
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
