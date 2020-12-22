using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace Меню
{
    class Field
    {
        public static int lvl;
        public static int xLenght;
        public static int yLenght;

        private static Random rand = new Random();

        protected static int EDGENUM;

        private static char[,] field;
        private static int[,] numField;

        private static string[] words = File.ReadAllLines("dict.txt");

        private static bool CellIsFree(int x, int y) //свободна ли ячейка и не превышены ли края поля
        {

            if (x <= xLenght - 1 && y <= yLenght - 1 && x >= 0 && y >= 0)
                if (field[x, y] == ' ')
                    return true;
                else
                    return false;
            else
                return false;

        }

        public static char[,] FillField()
        {
            xLenght = lvl / 2 + lvl % 2;
            yLenght = lvl / 2;
            EDGENUM = (int)(xLenght * yLenght / 2.5);
            field = new char[xLenght, yLenght];

            numField = new int[xLenght, yLenght];

            List<string> fieldWords = new List<string>();
            ClearFieldArray(field);
            int x = 0;
            int y = 0;
            string currentWord = "";
            bool stop = false;
            int cell = xLenght * yLenght;
            bool nextFor;

            int step = 0;


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
                    for (int i = 0; i < xLenght; i++)
                    {
                        if (stop)
                            break;

                        for (int j = 0; j < yLenght; j++)
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
                                    ClearFieldArray(field);
                                    ClearNumArray();
                                    step = 0;
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
                        x = rand.Next(0, xLenght);
                        y = rand.Next(0, yLenght);
                    } while (!CellIsFree(x, y));
                }
                fieldWords.Add(currentWord);
                field[x, y] = currentWord[0];

                step++;
                numField[x, y] = step;
                

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
                        numField[x, y] = step;
                    }
                    else
                    {
                        ClearFieldArray(field);
                        ClearNumArray();
                        step = 0;
                        fieldWords.Clear();
                        break;
                    }

                }

                cell = 0;
                for (int i = 0; i < xLenght; i++)
                {
                    for (int j = 0; j < yLenght; j++)
                    {
                        if (field[i, j] == ' ')
                            cell++;
                    }
                }

                if (cell == 0)
                    break;
            }
            fieldWords1 = fieldWords;
            numField1 = numField;
            return Field.field;
        }
        public static List<string> fieldWords1 = new List<string>();
        public static int[,] numField1; 
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

        private static char[,] ClearFieldArray(char[,] field)
        {
            for (int i = 0; i < xLenght; i++)
            {
                for (int j = 0; j < yLenght; j++)
                {
                    field[i, j] = ' ';
                }
            }

            return field;
        }

        private static char[,] ClearNumArray()
        {
            for (int i = 0; i < xLenght; i++)
            {
                for (int j = 0; j < yLenght; j++)
                {
                    numField[i, j] = 0;
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
        public static char[,] field;
        public static string playerName;
        public static bool gameContinue = false;
        public static char[,] quessedWords;
        public static int numOfQuessedWords = 0;

        private static int ReadLvl()
        {
            int lvl;
            string str;
            bool complete = false;
            do
            {
                str = Console.ReadLine();
                if(Int32.TryParse(str, out lvl))
                {
                    complete = lvl >= 5 && lvl <= 28;
                }
                
            } while (!complete);
            Console.Clear();
            return lvl;
        }
        public static void NewGame()
        {
            Console.Clear();

            if(!gameContinue)
            {
                Console.WriteLine("Введите ваш никнейм");
                playerName = Console.ReadLine().Trim(' ');
                Console.Clear();

                Console.WriteLine("Введите уровень от 5 до 28");

                Field.lvl = ReadLvl();
                Console.Clear();

                Console.Write("Идёт создание карты филвордов...");

                field = Field.FillField();
                Console.Clear();

                quessedWords = new char[Field.xLenght, Field.yLenght];
                numOfQuessedWords = 0;

                for (int i = 0; i < Field.xLenght; i++)
                {
                    for (int j = 0; j < Field.yLenght; j++)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write(field[i, j]);
                    }
                }

            }
            else
            {
                for (int i = 0; i < Field.xLenght; i++)
                {
                    for (int j = 0; j < Field.yLenght; j++)
                    {
                        if (quessedWords[i, j] == '*')
                        {
                            PrintingColoredChar(i, j, "green");
                        }
                        else
                            PrintingColoredChar(i, j, "black");
                    }
                }
                gameContinue = false;
            }
            
            

            for (int i = 0; i < 1; i++)
            {
                Console.SetCursorPosition(0, i + Field.yLenght + 1);
                Console.Write(Field.fieldWords1[i]);
            }


            Console.SetCursorPosition(0, 0);

            ConsoleKeyInfo key;
            int x = 0;
            int y = 0;
            string currentWord = null;

            
            bool stopFor = false;

            bool wordIsQuessed = true;
            int firstLetter = 0;


            char[,] thisTry = new char[Field.xLenght, Field.yLenght];
            bool enter = false;


            for (; ; )
            {
                if (stopFor)
                {
                    Console.SetCursorPosition(Field.xLenght + 1, 0);
                    Console.Write("Правильно!");
                    Continue.DeleteFile();
                    Records.Read();
                    if (Records.names.Contains(playerName))
                    {
                        for (int i = 0; i < Records.names.Count; i++)
                        {
                            if (Records.names[i] == playerName)
                            {
                                Records.points[i] += Field.xLenght * Field.yLenght;
                                break;
                            }
                                

                        } 
                    }
                    else
                    {
                        Records.names.Add(playerName);
                        Records.points.Add(Field.xLenght * Field.yLenght);
                    }
                    Records.WriteInFile();
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
                                PrintingColoredChar(x, y - 1, "blue");
                                y--;
                            }

                            if(enter && quessedWords[x, y - 1] != '*')
                            {
                                y -= 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                                if (firstLetter != Field.numField1[x, y])
                                    wordIsQuessed = false;
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
                                PrintingColoredChar(x, y + 1, "blue");
                                y++;
                            }

                            if (enter && quessedWords[x, y + 1] != '*')
                            {
                                y += 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                                if (firstLetter != Field.numField1[x, y])
                                    wordIsQuessed = false;
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
                                PrintingColoredChar(x - 1, y, "blue");
                                x--;
                            }

                            if (enter && quessedWords[x - 1, y] != '*')
                            {
                                x -= 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");
                                if (firstLetter != Field.numField1[x, y])
                                    wordIsQuessed = false;
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
                                PrintingColoredChar(x + 1, y, "blue");
                                x++;
                            }

                            if (enter && quessedWords[x + 1, y] != '*')
                            {
                                x += 1;
                                currentWord += field[x, y];
                                thisTry[x, y] = '*';
                                PrintingColoredChar(x, y, "red");

                                if (firstLetter != Field.numField1[x, y])
                                    wordIsQuessed = false;
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
                            wordIsQuessed = true;
                            firstLetter = Field.numField1[x, y];
                        }
                        else
                        {
                            if (Field.fieldWords1.Contains(currentWord) && wordIsQuessed)
                            {
                                numOfQuessedWords++;
                                if (numOfQuessedWords >= Field.fieldWords1.Count)
                                    stopFor = true;

                                for (int i = 0; i < Field.xLenght; i++)
                                {
                                    for (int j = 0; j < Field.yLenght; j++)
                                    {
                                        if (thisTry[i, j] == '*')
                                            quessedWords[i, j] = '*';
                                    }
                                }
                            }


                            for (int i = 0; i < Field.xLenght; i++)
                            {
                                for (int j = 0; j < Field.yLenght; j++)
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

                    case ConsoleKey.S:
                        Continue.SaveGame();
                        Console.Clear();
                        Program.Main();
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
            char[,] array = new char[Field.xLenght, Field.yLenght];

            for (int i = 0; i < Field.xLenght; i++)
            {
                for (int j = 0; j < Field.yLenght; j++)
                {
                    array[i, j] = '_';
                }
            }

            return array;
        }

        private static bool IsEnterInFieldArea(int x1, int y1)
        {
            return (x1 >= 0 && y1 >= 0 && x1 < Field.xLenght && y1 < Field.yLenght);
        }
    }
    class Continue
    {
        private static string path = Environment.CurrentDirectory + "/continue.txt";
        public static void DeleteFile()
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        public static void ContinueGame()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Вы не сохраняли игру");
                Console.ReadKey();
                Console.Clear();
                Program.Main();
            }
            else
            {
                Game.gameContinue = true;

                List<string> file = new List<string>();
                string line;
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var textReader = new StreamReader(fileStream, Encoding.GetEncoding(1251), false))
                {
                    do
                    {
                        line = textReader.ReadLine();
                        if (line != null)
                        {
                            file.Add(line);
                        }
                    } while (line != null);
                }
                //заполнение необходимых данных для работы
                Field.xLenght = Int32.Parse(file[0]);
                Field.yLenght = Int32.Parse(file[1]);

                string[] words = file[2].Trim(' ').Split(' ');

                string charField = file[3];
                string[] numField = file[4].Trim(' ').Split(' ');
                string charQussedWords = file[5];
                Game.playerName = file[6];
                Game.numOfQuessedWords = Int32.Parse(file[7]);
                //
                
                
                char[,] copyChar = new char[Field.xLenght, Field.yLenght];
                for (int i = 0; i < charQussedWords.Length; i++)
                {
                    copyChar[i % Field.xLenght, i / Field.xLenght] = charField[i];
                }
                Game.field = copyChar;

                char[,] copyQuessed = new char[Field.xLenght, Field.yLenght];
                for (int i = 0; i < charQussedWords.Length; i++)
                {
                    copyQuessed[i % Field.xLenght, i / Field.xLenght] = charQussedWords[i];
                }
                Game.quessedWords = copyQuessed;

                Field.fieldWords1.Clear();
                for (int i = 0; i < words.Length; i++)
                {
                    Field.fieldWords1.Add(words[i]);
                }

                int[,] numArray = new int[Field.xLenght, Field.yLenght];
                for (int i = 0; i < charQussedWords.Length; i++)
                {
                    numArray[i % Field.xLenght, i / Field.xLenght] = Int32.Parse(numField[i]);
                }
                Field.numField1 = numArray;

                Game.NewGame();
            }

        }

        

        public static void SaveGame()
        {
            using (var fileStream1 = new FileStream(path, FileMode.Create))
            {

            };

            string[] toWrite = new string[8];

            toWrite[0] = Field.xLenght.ToString();
            toWrite[1] = Field.yLenght.ToString();

            string line = "";

            for (int i = 0; i < Field.fieldWords1.Count; i++)
            {
                line += Field.fieldWords1[i] + " ";
            }
            toWrite[2] = line;

            line = "";

            for (int i = 0; i < Field.yLenght; i++)
            {
                for (int j = 0; j < Field.xLenght; j++)
                {
                    line += Game.field[j, i];
                }
            }
            toWrite[3] = line;

            line = "";

            for (int i = 0; i < Field.yLenght; i++)
            {
                for (int j = 0; j < Field.xLenght; j++)
                {
                    line += Field.numField1[j, i].ToString() + " ";
                }
            }
            toWrite[4] = line;

            line = "";

            for (int i = 0; i < Field.yLenght; i++)
            {
                for (int j = 0; j < Field.xLenght; j++)
                {
                    line += Game.quessedWords[j, i];
                }
            }
            toWrite[5] = line;

            toWrite[6] = Game.playerName;
            toWrite[7] = Game.numOfQuessedWords.ToString();

            File.WriteAllLines(path, toWrite, Encoding.GetEncoding(1251));
        }




    }
    class Records
    {
        private static string path = Environment.CurrentDirectory + "/records.txt";

        public static List<string> names = new List<string>();
        public static List<int> points = new List<int>();

        public static void Read()
        {
            if (!File.Exists(path))
            {
                using (var fileStream1 = new FileStream(path, FileMode.Create));
            }

                names.Clear();
                points.Clear();

            List<string> file = new List<string>();
            string str;
            string line;
            using(var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream,Encoding.GetEncoding(1251),false))
            {
                do
                {
                    line = textReader.ReadLine();
                    if (line != null)
                    {
                        file.Add(line);
                    }
                } while (line != null);
            }

            

            int i = 0;
            if (file.Count != 0)
            {
                do
                {
                    str = file[i];
                    if (str != "")
                        names.Add(str);
                    i++;
                } while (str != "");

                int point;
                do
                {
                    try
                    {
                        point = Int32.Parse(file[i]);
                    }
                    catch
                    {
                        point = 0;
                    }
                    points.Add(point);
                    i++;
                } while (i < file.Count);
            } 
        }

        public static void ReadAndPrint()
        {
            Records.Read();
            for (int i = 0; i < names.Count; i++)
            {
                Console.Write(names[i] + " ");
                Console.WriteLine(points[i]);
            }
        }

        public static void WriteInFile()
        {
            string[] toWrite = new string [Records.names.Count + 1 + Records.points.Count];
            for (int i = 0; i < Records.names.Count; i++)
            {
                toWrite[i] = Records.names[i];
            }

            toWrite[Records.names.Count] = "";

            for (int i = 0; i < Records.points.Count; i++)
            {
                toWrite[Records.names.Count + 1 + i] = Records.points[i].ToString();
            }
            File.WriteAllLines(path, toWrite, Encoding.GetEncoding(1251));
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
                            Continue.DeleteFile();
                            Game.NewGame();
                            
                            break;
                        case 1:
                            Continue.ContinueGame();
                            break;
                        case 2:
                            Records.ReadAndPrint();
                            Console.ReadKey();
                            Console.Clear();
                            Program.Main();
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

