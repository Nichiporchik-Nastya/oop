using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Типы

            /*a. Определите переменные 
                 всех возможных примитивных типов С# и проинициализируйте их*/

            byte Byte = 0;
            sbyte Sbyte = 1;
            short Short = -1000;
            ushort Ushort = 1000;
            int Int = -70215458;
            uint Uint = 1;
            long Long = -154879655698;
            ulong Ulong = 154879655698;

            Console.WriteLine("Целочисленные типы: \n" + Byte + " " + Sbyte + " " + Short + " " + Ushort + " " + Int + " " + Uint + " " + Long + " " + Ulong + "\n");

            float Float = 1.2f + 6;
            double Double = -4242.6e-2;
            decimal Decimal = 156832225486532356;

            Console.WriteLine("Типы с плавающей точкой: \n" + Float + " " + Double + " " + Decimal + "\n");

            bool Bool = false;
            char Char = 'N';
            string String = "nastya";

            Console.WriteLine("Логический и символьные типы: \n" + Bool + " " + Char + " " + String + "\n");

            object Object = Bool; //может хранить любой другой тип
            dynamic Dinamic = String; //можно присваивать любой тип данных динамически, во время выполнения программы

            Console.WriteLine("Обджект и дайнемик: \n" + Object + " " + Dinamic + "\n");

            /*b. Выполните 5 операций явного
                 и 5 неявного приведения*/

            //явные
            int newDouble = (int)Double; //newDouble = -42, Double = -42.426
            char newUint = (char)Uint; // newUint = '☺', Uint = 1
            sbyte newUshort = (sbyte)Ushort; // newUshort = -24, Ushort = 1000
            long newDecimal = (long)Decimal;
            uint newFloat = (uint)Float; //newFloat = 7, Float = 7.2

            //неявные
            short newSByte = Sbyte;
            long newInt = Int;
            float newLong = Long;
            uint newByte = Byte;
            double newUlong = Ulong;

            /*c. Выполните упаковку
                 и распаковку значимых типов (влияет на производительность)*/

            int k = 500; //структура, значимый тип
            object O = k; //класс, ссылочный тип, упаковка
            int n = (int)O; //значимый тип, распаковка

            /*d. Продемонстрируйте работу с неявно типизированной
                 переменной*/

            var f = 5.34F; // тип float

            /*e. Продемонстрируйте пример работы с Nullable переменной*/

            byte? N = null; //целочисленный тип со значением null
            //N = 15;
            Console.WriteLine("Значение переменной NullAble"+N + "\n"); // 15; может быть и null, и число

            //Строки

            /*a. Объявите строковые литералы. Сравните их*/

            string P = "Привет";
            string H = "Hello";
            string Q = "Hello";

            Console.WriteLine("\nРезультаты сравнения строк: ");

            Console.WriteLine(String.Compare(P, H)); //1, первая строка следует за второй в порядке сортировки
            Console.WriteLine(String.Compare(H, P)); //-1, Первая строка предшествует второй в порядке сортировки или первая строка имеет значение null.
            Console.WriteLine(String.Compare(Q, H)); //0 строки равны
            Console.WriteLine(String.Compare(Q, "olleH")); //-1 

            Console.WriteLine(String.Equals(P, H)); //false
            Console.WriteLine(H.Equals("Hello")); //true
            Console.WriteLine(String.Equals(Q, H)); //true

            /*b. Создайте три строки на основе String. Выполните: сцепление,
                копирование, выделение подстроки, разделение строки на слова,
                вставки подстроки в заданную позицию, удаление заданной
                подстроки*/

            Console.WriteLine("\nРабота со строками: ");

            string s = "В траве сидел кузнечик";

            string p = String.Concat(P, "!!!"); //сцепление
            string p2 = String.Concat(P, H);

            string S = string.Copy(P); //копирование
            Console.WriteLine(S);

            string[] subs = s.Split(); //разделение строки на слова
            for (int i = 0; i < subs.Length; i++)
                Console.WriteLine(subs[i]);

            string first = "высокой ";
            string second = "зелёный ";
            s = s.Insert(14, second); //вставка подстроки в заданную позицию
            s = s.Insert(2, first);
            Console.WriteLine(s);

            int index = s.IndexOf(first); //получить номер символа, с которого начинается подстрока в строке
            s = s.Remove(index, first.Length); //удалить подстроку из строки
            Console.WriteLine(s);

            index = s.IndexOf(second); //получить номер символа, с которого начинается подстрока в строке
            string subSecond = s.Substring(index, second.Length); //извлечь подстроку из строки
            Console.WriteLine(subSecond); //зелёный

            /*c. Создайте пустую и null строку. Продемонстрируйте что можно
                выполнить с такими строками*/

            string empty = ""; //строка пустая
            string Null = null; //значение строки не указано
            if (String.Equals(empty, Null))
            {
                Console.WriteLine("значения путой строки и null-строки совпадают");
            }
            else 
            {
                Console.WriteLine("значения путой строки и null-строки не совпадают"); //в консоль
            }

            /*d. Создайте строку на основе StringBuilder. Удалите определенные
            позиции и добавьте новые символы в начало и конец строки*/

            StringBuilder str = new StringBuilder("Привет мир"); //создание строки

            str.Append("!!!"); //добавление символов в конец строки

            str.Insert(0,"---"); //добавление символов в начало строки

            str.Remove(5, 1); //добавление символов в определённых позициях
            str.Remove(6, 1);
            str.Remove(9, 1);

            Console.WriteLine("\nРезультат работы со StringBuilder: ");
            Console.WriteLine(str);

            //Массивы

            /*a. Создайте целый двумерный массив и выведите его на консоль в
            отформатированном виде (матрица). */

            Console.WriteLine("\nВывод матрицы: ");

            int[,] mas = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } }; //объявление с заданием значений

            int rows = mas.GetLength(0) ; //количество подмассивов в нулевом измерении (самом массиве)
            int columns = mas.Length / rows; //длину строки поделить на количество строк получить количество столбцов
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{mas[i, j]} \t"); //интерполяция
                }
                Console.WriteLine();
            }

            /*b. Создайте одномерный массив строк. Выведите на консоль его
            содержимое, длину массива. Поменяйте произвольный элемент
            (пользователь определяет позицию и значение)*/

            Console.WriteLine("\nЗамена строк: ");

            string[] strArr = {"Добрый день", "Как дела?", "Неплохо", "Cпасибо" };
            int x = 0;
            foreach (string strAr in strArr)
            {
                Console.Write($" {++x}) {strAr}");
            }
            Console.Write("\nДлина массива: "+ strArr.Length);

            Console.Write("\nВведите позицию и значение для замены...\n");
            int pos = Convert.ToInt32(Console.ReadLine());
            string answer = Console.ReadLine();
            strArr[--pos] = answer;
            foreach (string strAr in strArr)
            {
                Console.Write(strAr + " ");
            }
            Console.WriteLine();

            /*c. Создайте ступечатый (не выровненный) массив вещественных
            чисел с 3-мя строками, в каждой из которых 2, 3 и 4 столбцов
            соответственно. Значения массива введите с консоли*/

            Console.WriteLine("\nСтупенчатый массив ");

            // Объявление ступенчатого массива
            int[][] stepsArr = new int[3][];
            stepsArr[0] = new int[2];
            stepsArr[1] = new int[3];
            stepsArr[2] = new int[4];

            //Console.WriteLine("\nВведите значения зубчатого массива");
            Random random = new Random();

            for (int J = 0; J < stepsArr.Length; J++)
            {
                for (int I = 0; I < stepsArr[J].Length; I++)
                {
                    //stepsArr[J][I] = Convert.ToInt32(Console.Read());
                    stepsArr[J][I] = random.Next(100);
                }
            }
            for (int J = 0; J < stepsArr.Length; J++)
            {
                for (int I = 0; I < stepsArr[J].Length; I++)
                {
                    Console.Write(stepsArr[J][I]+"\t");
                }
                Console.WriteLine();
            }

            /*d. Создайте неявно типизированные переменные для хранения
            массива и строки*/

            var Nsting = "";
            var Narray = new object[0];

            //Кортежи

            /*a. Задайте кортеж из 5 элементов с типами int, string, char, string,
            ulong*/
            /*b. Сделайте именование его элементов*/
            /*c. Выведите кортеж на консоль целиком и выборочно (1, 3, 4
            элементы)*/
            /*d. Выполните распаковку кортежа в переменные.*/

            Console.WriteLine("\nРезультаты работы с кортежами: ");

            var Kort = (integer: 500, myString: "Okey", symbol: '♀', "Nope", longNumber: 36578921546); //задание кортежа и именование почти всех элементов
            
            Console.WriteLine(Kort.Item4, Kort.integer); //вывод четвёртого по ключевому слову и первого по названию
            Console.WriteLine(Kort.symbol); //вывод 3
            Console.WriteLine(Kort); //вывод целиком

            var (integer, mySting, symbol, myString2, longNumber) = Kort; //распаковка в переменные
            Console.WriteLine(myString2);

            /*e. Сравните два кортежа.*/

            var firstTuple = Tuple.Create(10, 5);
            var secondTuple = Tuple.Create(5, 15);
            var firstTuple2 = Tuple.Create(10, 5);

            bool res = firstTuple.Equals(secondTuple); //false
            res = firstTuple.Equals(firstTuple2); //true
            Console.WriteLine(res);

            //Локальная функция

            /*Создайте локальную функцию в main и вызовите ее. Формальные
            параметры функции – массив целых и строка. Функция должна вернуть
            кортеж, содержащий: максимальный и минимальный элементы массива,
            сумму элементов массива и первую букву строки*/

            int[] mas2 = { -1, 3, 25, 10, -8, 0};
            string myStr = "Сегодня пятница!";

            Console.WriteLine("\nРезультат выполнения локальной функции: ");
            Console.WriteLine(MinMaxSumFirst(mas2, myStr));

            static (int max, int min, int sum, char letter) MinMaxSumFirst (int[] mas2, string myStr)
            {
                int max = -999, min = 999, sum =0;
                foreach (int m2 in mas2)
                {
                    sum += m2;
                    if (m2 > max)
                    {
                        max = m2;
                    }
                    if (m2 < min)
                    {
                        min = m2;
                    }
                }
                char letter = Convert.ToChar(myStr.Substring(0, 1));
                var rez = (max, min, sum, letter);
                return rez;
            }
        }
    }
}
