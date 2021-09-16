using System;

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

            Console.WriteLine("Целочисленные типы: \n" + Byte+" "+Sbyte + " " + Short + " " + Ushort + " " + Int + " " + Uint + " " + Long + " " + Ulong + "\n");

            float Float = 1.2f+6;
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
            int j = (int)O; //значимый тип, распаковка

        /*d. Продемонстрируйте работу с неявно типизированной
             переменной*/

            var f = 5.34F; // тип float

        /*e. Продемонстрируйте пример работы с Nullable переменной*/

            byte? N = null; //целочисленный тип со значением null
            N = 15;
            Console.WriteLine(N + "\n"); // 15; может быть и null, и число

            //Строки

            /*a. Объявите строковые литералы. Сравните их*/

            string P = "Привет";
            string H = "Hello";
            string Q = "Hello";

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

           

        }
    }
}
