using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace Меню
{
    class Field
    {
        public const int XLENGHT = 6;
        public const int YLENGHT = 6;
        protected const int EDGENUM =(int) (XLENGHT * YLENGHT / 2.5);

        private static char[,] field = new char[XLENGHT, YLENGHT];


        //string[] words = File.ReadAllLines("words.txt");
        private static List<string> words2 = new List<string> {"як", "фа"/* нота*/,"ёж", "ил", "ум", "щи", "юг", "яд" };
        private static List<string> words3 = new List<string> { "кит", "жук", "кот", "суп", "дом", "бор", "рог", "лом", "шум", "том", "бит", "год", "газ", "зуб", "лук", "фен", "эхо", "яма" };
        private static List<string> words4 = new List<string> { "хром", "кофе", "мышь", "круг", "лапа", "плуг", "вата", "борт", "гипс", "дань", "желе", "идол", "опыт", "пена", "урок", "фтор", "ярус" };
        private static List<string> words5 = new List<string> { "скейт", "порог", "груда", "тыква", "крупа", "факел", "багет", "веник", "волос", "гайка", "дубль", "желоб", "лакей", "совок", "центр" };
        private static List<string> words6 = new List<string> { "капкан", "лопата", "пророк", "кимоно", "бросок", "акация", "затвор", "колпак", "нейлон", "физика", "хозяин", "хлопец", "шерсть", "ястреб" };

        

        private static bool CellIsFree(int x, int y) //свободна ли ячейка и не превышены ли края поля
        {

            if (x <= XLENGHT-1 && y <= YLENGHT-1 && x >= 0 && y >= 0)
                if (field[x, y] == ' ')
                    return true;
                else
                    return false;
            else
                return false;

        }
        private static int Random(int min, int max)
        {
            Random rand = new Random();
            try
            {
                return rand.Next(min, max);
            }
            catch
            {
                return min;
            }
        }

        public static char[,] FillField()
        {
            Console.Clear();
            List<string> fieldWords = new List<string>();
            ClearArray(field);
            int x = 0;
            int y = 0;
            string currentWord = "";
            bool stop = false;
            int cell = XLENGHT * YLENGHT;
            bool nextFor;

            for (; ; )
            {
                stop = false;
                nextFor = false;

                if (cell > EDGENUM)
                {
                    currentWord = GetWord(Random(3, 7), fieldWords);
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
                        x = Random(0, XLENGHT);
                        y = Random(0, YLENGHT);
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
                            switch (Random(1, 5))
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

        private static bool TwoPointsAroundIsFree (int x, int y)
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

        private static char[,] ClearArray (char[,] field)
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
        private static string GetWord(int lenght, List<string>fieldWords)
        {
            string currentWord = null;
            int num;
            switch (lenght)
            {
                case 2:
                    do
                    {
                        num = Random(0, words2.Count);
                        currentWord = words2[num];
                    }
                    while (fieldWords.Contains(currentWord));
                    break;
                case 3:
                    do
                    {
                        num = Random(0, words3.Count);
                        currentWord = words3[num];
                    }
                    while (fieldWords.Contains(currentWord));
                    break;
                case 4:
                    do
                    {
                        num = Random(0, words4.Count);
                        currentWord = words4[num];
                    }
                    while (fieldWords.Contains(currentWord));
                    break;
                case 5:
                    do
                    {
                        num = Random(0, words5.Count);
                        currentWord = words5[num];
                    }
                    while (fieldWords.Contains(currentWord));
                    break;
                case 6:
                    do
                    {
                        num = Random(0, words6.Count);
                        currentWord = words6[num];
                    }
                    while (fieldWords.Contains(currentWord));
                    break;
            }
            return currentWord;
        }
    }
    class Game
    {   
        public static void NewGame()
        {
            Console.WriteLine("Введите ваш никнейм");
            string playerName = Console.ReadLine();
            Console.Clear();
            Console.Write("Идёт создание карты филвордов...");
            char[,] field = Field.FillField();
            Console.Clear();

            for (int i = 0; i < Field.XLENGHT; i++)
            {
                for (int j = 0; j < Field.YLENGHT; j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
            }

            Console.ReadKey();


            



            Environment.Exit(0);
        }

        private bool KeyReader(char[,] field)
        {
            ConsoleKeyInfo key;

            for (; ; )
            {
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:

                        break;
                        break;
                    case ConsoleKey.DownArrow:

                        break;
                    case ConsoleKey.LeftArrow:

                        break;
                    case ConsoleKey.RightArrow:
                        break;
                }
            }
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
