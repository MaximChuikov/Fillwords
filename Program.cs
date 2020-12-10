using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Меню
{
    class Field
    {
        public const int XLENGHT = 5;
        public const int YLENGHT = 5;

        private static Random rand = new Random();

        protected const int EDGENUM = (int)(XLENGHT * YLENGHT / 2.5);

        private static char[,] field = new char[XLENGHT, YLENGHT];


        private static string[] words = File.ReadAllLines("dict.txt");

        private static bool CellIsFree(int x, int y) //свободна ли ячейка и не превышены ли края поля
        {

            if (x <= XLENGHT - 1 && y <= YLENGHT - 1 && x >= 0 && y >= 0)
                if (field[x, y] == ' ')
                    return true;
                else
                    return false;
            else
                return false;

        }

        public static char[,] FillField()
        {
            List<string> fieldWords = new List<string>();
            ClearArray(field);
            int x = 0;
            int y = 0;
            string currentWord = "";
            bool stop = false;
            int cell = XLENGHT * YLENGHT;
            bool nextFor;
            Console.WriteLine("Идёт создание филвордов...");
            for (; ; )
            {
                stop = false;
                nextFor = false;

                if (cell > EDGENUM)
                {
                    currentWord = GetWord(rand.Next(3, 11), fieldWords);
                }
                else
                {
                    for (int i = 0; i < XLENGHT; i++)
                    {
                        if (stop)
                            break;

                        for (int j = 0; j < YLENGHT; j++)
                        {
                            if (CellIsFree(i, j))
                            {
                                x = i;
                                y = j;
                                if (ThreePointsAroundIsFree(i, j))
                                {
                                    currentWord = GetWord(4, fieldWords);
                                    stop = true;
                                    break;
                                }
                                else if (TwoPointsAroundIsFree(i, j) && !stop)
                                {
                                    currentWord = GetWord(3, fieldWords);
                                    stop = true;
                                    break;
                                }
                                else if (CellIsFree(i + 1, j) || CellIsFree(i - 1, j) || CellIsFree(i, j + 1) || CellIsFree(i, j - 1) && !stop)
                                {
                                    currentWord = GetWord(2, fieldWords);
                                    stop = true;
                                    break;
                                }
                                else
                                {
                                    ClearArray(field);
                                    //Console.Clear();
                                    fieldWords.Clear();
                                    nextFor = true;
                                    stop = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (nextFor)
                    continue;


                if (!stop)
                {
                    do
                    {
                        x = rand.Next(0, XLENGHT);
                        y = rand.Next(0, YLENGHT);
                    } while (!CellIsFree(x, y));
                }
                fieldWords.Add(currentWord);
                field[x, y] = currentWord[0];
                //Console.SetCursorPosition(x, y);
                //Console.Write(field[x, y]);

                for (int i = 1; i < currentWord.Length; i++)
                {
                    if (CellIsFree(x + 1, y) || CellIsFree(x - 1, y) || CellIsFree(x, y + 1) || CellIsFree(x, y - 1))
                    {
                        stop = false;
                        while (!stop)
                        {
                            switch (rand.Next(1, 5))
                            {
                                case 1:
                                    stop = CellIsFree(x + 1, y);
                                    if (stop)
                                        x += 1;
                                    break;
                                case 2:
                                    stop = CellIsFree(x - 1, y);
                                    if (stop)
                                        x -= 1;
                                    break;
                                case 3:
                                    stop = CellIsFree(x, y + 1);
                                    if (stop)
                                        y += 1;
                                    break;
                                case 4:
                                    stop = CellIsFree(x, y - 1);
                                    if (stop)
                                        y -= 1;
                                    break;

                            }
                        }
                        stop = false;
                        field[x, y] = currentWord[i];
                        //Console.SetCursorPosition(x, y);
                        //Console.Write(field[x, y]);
                    }
                    else
                    {
                        ClearArray(field);
                        //Console.Clear();
                        fieldWords.Clear();
                        break;
                    }

                }
                cell = 0;
                for (int i = 0; i < XLENGHT; i++)
                {
                    for (int j = 0; j < YLENGHT; j++)
                    {
                        if (field[i, j] == ' ')
                            cell++;
                    }
                }

                if (cell == 0)
                    break;
            }
            fieldWords1 = fieldWords;
            return Field.field;
        }
        public static List<string> fieldWords1 = new List<string>();

        private static bool TwoPointsAroundIsFree(int x, int y)
        {
            if (CellIsFree(x + 1, y))
            {//иду вправо
                if (CellIsFree(x + 1, y + 1))
                    return true;
                else if (CellIsFree(x + 2, y))
                {//иду вправо-вправо
                    return true;
                }
                if (CellIsFree(x + 1, y - 1))
                {//иду вправо-вниз
                    return true;
                }
            }
            else if (CellIsFree(x, y - 1))
            {//иду вниз
                if (CellIsFree(x + 1, y - 1))
                {//иду вниз-вправо
                    return true;
                }
                if (CellIsFree(x, y - 2))
                {//иду вниз-вниз
                    return true;
                }
                if (CellIsFree(x - 1, y - 1))
                {//иду вниз-влево
                    return true;
                }
            }
            else if (CellIsFree(x - 1, y))
            {//иду влево
                if (CellIsFree(x - 1, y - 1))
                {//иду влево-вниз
                    return true;
                }
                if (CellIsFree(x - 2, y))
                {//иду влево-влево
                    return true;
                }
                if (CellIsFree(x - 1, y + 1))
                {//иду влево-вверх
                    return true;
                }
            }
            else if (CellIsFree(x, y + 1))
            {//иду вверх
                if (CellIsFree(x - 1, y + 1))
                {//иду вверх-влево
                    return true;
                }
                if (CellIsFree(x, y + 2))
                {//иду вверх-вверх
                    return true;
                }
                if (CellIsFree(x + 1, y + 1))
                {//иду вверх-вправо
                    return true;
                }
            }
            return false;
        }

        private static bool ThreePointsAroundIsFree(int x, int y)
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

        private static char[,] ClearArray(char[,] field)
        {
            for (int i = 0; i < XLENGHT; i++)
            {
                for (int j = 0; j < YLENGHT; j++)
                {
                    field[i, j] = ' ';
                }
            }

            return field;
        }
        private static string GetWord(int lenght, List<string> fieldWords)
        {
            string currentWord = null;
            do
            {
                currentWord = words[rand.Next(0, words.Length)];
            } while (fieldWords.Contains(currentWord) || currentWord.Length != lenght);
            return currentWord;
        }
    }
    class Game
    {
        static char[,] field = Field.FillField();
        public static void NewGame()
        {
            Console.Clear();
            Console.WriteLine("Введите ваш никнейм");
            string playerName = Console.ReadLine();
            Console.Clear();
            Console.Write("Идёт создание карты филвордов...");
            Console.Clear();

            for (int i = 0; i < Field.XLENGHT; i++)
            {
                for (int j = 0; j < Field.YLENGHT; j++)
                {
                    Console.SetCursorPosition(i,j);
                    Console.Write(field[i,j]);
                }
            }

            for (int i = 0; i < Field.fieldWords1.Count; i++)
            {
                Console.SetCursorPosition(0, i + Field.YLENGHT + 1);
                Console.Write(Field.fieldWords1[i]);
            }


            Console.SetCursorPosition(0, 0);

            ConsoleKeyInfo key;
            int x = 0;
            int y = 0;
            string currentWord = null;
            char[,] quessedWords = new char[Field.XLENGHT, Field.YLENGHT];
            int numOfQuessedWords = 0;
            bool stopFor = false;




            char[,] thisTry = new char[Field.XLENGHT, Field.YLENGHT];
            bool enter = false;


            for (; ; )
            {
                if (stopFor)
                {
                    Console.SetCursorPosition(Field.XLENGHT + 1, 0);
                    Console.Write("Правильно!");
                    break;
                }

                Console.SetCursorPosition(x, y);
                key = Console.ReadKey();

                if(quessedWords[x,y] != '*' && !enter)
                    PrintingColoredChar(x, y, "black");
                else if (enter)
                    PrintingColoredChar(x, y, "red");
                else if (quessedWords[x,y] == '*')
                    PrintingColoredChar(x, y, "green");


                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (IsEnterInFieldArea(x, y - 1) && thisTry[x, y - 1] != '*')
                        {
                            if (!enter)
                            {
                                //PrintingColoredChar(x, y, "black");
                                PrintingColoredChar(x, y - 1, "blue");
                                y--;
                            }

                            if(enter && quessedWords[x, y - 1] != '*')
                            {
                                y -= 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                            }

                        }
                        else
                        {
                            PrintingColoredChar(x, y, "blue");
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (IsEnterInFieldArea(x, y + 1) && thisTry[x, y + 1] != '*')
                        {
                            if (!enter)
                            {
                                //PrintingColoredChar(x, y, "black");
                                PrintingColoredChar(x, y + 1, "blue");
                                y++;
                            }

                            if (enter && quessedWords[x, y + 1] != '*')
                            {
                                y += 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                            }

                        }
                        else
                        {
                            PrintingColoredChar(x, y, "blue");
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (IsEnterInFieldArea(x - 1, y) && thisTry[x - 1, y] != '*')
                        {
                            if (!enter)
                            {
                                //PrintingColoredChar(x, y, "black");
                                PrintingColoredChar(x - 1, y, "blue");
                                x--;
                            }

                            if (enter && quessedWords[x - 1, y] != '*')
                            {
                                x -= 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                            }

                        }
                        else
                        {
                            PrintingColoredChar(x, y, "blue");
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (IsEnterInFieldArea(x + 1, y) && thisTry[x + 1, y] != '*')
                        {
                            if (!enter)
                            {
                                //PrintingColoredChar(x, y, "black");
                                PrintingColoredChar(x + 1, y, "blue");
                                x++;
                            }

                            if (enter && quessedWords[x + 1, y] != '*')
                            {
                                x += 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                            }

                        }
                        else
                        {
                            PrintingColoredChar(x, y, "blue");
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (!enter)
                        {
                            thisTry = ClearArray();
                            currentWord = null;
                            PrintingColoredChar(x, y, "red");
                            currentWord += field[x, y];
                            thisTry[x, y] = '*';
                        }
                        else
                        {
                            if (Field.fieldWords1.Contains(currentWord))
                            {
                                numOfQuessedWords++;
                                if (numOfQuessedWords == Field.fieldWords1.Count)
                                    stopFor = true;

                                for (int i = 0; i < Field.XLENGHT; i++)
                                {
                                    for (int j = 0; j < Field.YLENGHT; j++)
                                    {
                                        if (thisTry[i, j] == '*')
                                            quessedWords[i, j] = '*';
                                    }
                                }
                            }


                            for (int i = 0; i < Field.XLENGHT; i++)
                            {
                                for (int j = 0; j < Field.YLENGHT; j++)
                                {
                                    if (quessedWords[i, j] == '*')
                                    {
                                        PrintingColoredChar(i, j, "green");
                                    }
                                    else
                                        PrintingColoredChar(i, j, "black");
                                }
                            }

                            currentWord = "";
                            thisTry = ClearArray();
                        }
                        enter = !enter;
                        break;

                    default:
                        PrintingColoredChar(x, y, "black");
                        break;
                }
            }



            Console.ReadKey();
            Console.Clear();
            Program.Main();
        }

        private static void PrintingColoredChar(int x, int y, string color)
        {
            switch(color)
            {
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "green":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case "blue":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
            Console.SetCursorPosition(x, y);
            Console.Write(field[x, y]);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        static char [,] ClearArray ()
        {
            char[,] array = new char[Field.XLENGHT, Field.YLENGHT];

            for (int i = 0; i < Field.XLENGHT; i++)
            {
                for (int j = 0; j < Field.YLENGHT; j++)
                {
                    array[i, j] = '_';
                }
            }

            return array;
        }

        private static bool IsEnterInFieldArea(int x1, int y1)
        {
            return (x1 >= 0 && y1 >= 0 && x1 < Field.XLENGHT && y1 < Field.YLENGHT);
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


        public static void Main()
        {
            const int RIGHTINDENT = 5;   // отступ справа
            const int UPINDENT = 5;      // отступ сверху
            const int WIDTH = 10;         // ширина
            const int LENGHT = 4;         // длина

            Menu.DrawFrame(RIGHTINDENT, UPINDENT, WIDTH, LENGHT);

            string[] menustrings = { "Новая игра", "Продолжить", "Рейтинг", "Выход" };

            for (int i = 0; i < menustrings.Length; i++)
            {
                Menu.SetCursorAndWrite(menustrings[i], RIGHTINDENT, UPINDENT + i);
            }

            int selectedLine = 0;
            Menu.SelectingString(menustrings[selectedLine], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine, UPINDENT);
            ConsoleKeyInfo key;
            for (; ; )
            {
                Console.SetCursorPosition(RIGHTINDENT - 1, UPINDENT + LENGHT + 1);
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow && selectedLine != 0)
                {
                    selectedLine--;
                    Menu.SelectingString(menustrings[selectedLine + 1], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine + 1, UPINDENT + selectedLine);
                }

                if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow && selectedLine != LENGHT - 1)
                {
                    selectedLine++;
                    Menu.SelectingString(menustrings[selectedLine - 1], menustrings[selectedLine], RIGHTINDENT, RIGHTINDENT + WIDTH, UPINDENT + selectedLine - 1, UPINDENT + selectedLine);
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (selectedLine)
                    {
                        case 0:
                            Game.NewGame();
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

