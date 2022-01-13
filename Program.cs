using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        ///////////////////////////////////////////////////////   Lab1   /////////////////////////////////////////////////////////////

        // Функция для нахождения суммы двух неотрицательных чисел, заданных в шестнадцатиричной системе исчисления
        public static string S(string num1, string num2)
        {
            // Переменная для бита переноса
            bool c = false;
            // Переменная, которую в последствии вернёт функция 
            string a = "";

            // Выравниваем числе по длине, если одно длинее другого
            if (num1.Length > num2.Length) A(num1, ref num2);
            if (num1.Length < num2.Length) A(num2, ref num1);

            // Проходим в цикле по каждому символу с конца каждого слова и складываем их синхронно. Используем бит переноса по необходимости.
            for (int q = 0; q < num1.Length; q++)
            {
                if (c)
                {
                    if ((ToI(num1[num1.Length - q - 1]) + 1 + ToI(num2[num1.Length - q - 1])) > 15)
                    {
                        a = ToC(((ToI(num1[num1.Length - q - 1]) + ToI(num2[num1.Length - q - 1])) % 16) + 1) + a;
                        c = true;
                    }
                    else
                    {
                        a = ToC((ToI(num1[num1.Length - q - 1]) + 1 + ToI(num2[num1.Length - q - 1]))) + a;
                        c = false;
                    }
                }
                else
                {
                    if ((ToI(num1[num1.Length - q - 1]) + ToI(num2[num1.Length - q - 1])) > 15)
                    {
                        a = ToC(((ToI(num1[num1.Length - q - 1]) + ToI(num2[num1.Length - q - 1])) % 16)) + a;
                        c = true;
                    }
                    else
                    {
                        a = ToC((ToI(num1[num1.Length - q - 1]) + ToI(num2[num1.Length - q - 1]))) + a;
                        c = false;
                    }
                }

                if ((q == num1.Length - 1) && (c == true))
                {
                    a = '1' + a;
                }
            }

            // Возвращаем строку с искомой суммой двух чисел
            return a;
        }

        // Функция для нахождения расности двух неотрицательных чисел, заданных в шестнадцатиричной системе исчисления
        public static string D(string num1, string num2)
        {
            // Переменная для бита переноса
            bool c = false;
            // Переменная, которую в последствии вернёт функция 
            string a = "";

            // Приводим числа к формату, удобному для вычетания, на строках с 73 по 113 
            if (num1.Length > num2.Length)
            {
                A(num1, ref num2);
            }

            if ((num1.Length < num2.Length) && (num2[0] != '0'))
            {
                A(num2, ref num1);
                string num3 = num1;
                num1 = num2;
                num2 = num3;
            }

            if ((num1.Length < num2.Length) && (num2[0] == '0'))
            {
                while ((num2[0] == '0'))
                {
                    if (num1.Length == num2.Length) break;
                    if (num2.Remove(0, 1) != "")
                    {
                        num2 = num2.Remove(0, 1);
                    }
                    else { num2 = "0"; break; }

                }
            }

            if ((num1.Length == 1) && (num2.Length == 1))
            {
                if (((ToI(num1[0]) - ToI(num2[0])) == 0) || ((ToI(num1[0]) - ToI(num2[0])) < 0)) a = "0";
                if ((ToI(num1[0]) - ToI(num2[0])) > 0)
                {
                    a = "";
                    a = ToI(num1[0]) - ToI(num2[0]) + a;
                }
            }

            if ((num1.Length < num2.Length) && (num2 == "0"))
            {
                A(ref num2, num1.Length);
            }


            // Вычетаем посимвольно с конца строки в цикле. По необходимости используем бит переноса
            if (!((num1.Length == 1) && (num2.Length == 1)))
            {
                for (int q = 0; q < num1.Length; q++)
                {
                    if (!c)
                    {
                        if ((ToI(num1[num1.Length - q - 1]) - ToI(num2[num1.Length - q - 1])) < 0)
                        {
                            a = ToC(16 + (ToI(num1[num1.Length - q - 1]) - ToI(num2[num1.Length - q - 1]))) + a;
                            c = true;
                        }
                        else
                        {
                            a = ToC((ToI(num1[num1.Length - q - 1]) - ToI(num2[num2.Length - q - 1]))) + a;
                        }
                    }
                    else
                    {
                        if ((ToI(num1[num1.Length - q - 1]) - 1 - ToI(num2[num1.Length - q - 1])) < 0)
                        {
                            a = ToC(16 + (ToI(num1[num1.Length - q - 1]) - 1 - ToI(num2[num1.Length - q - 1]))) + a;
                            c = true;
                        }
                        else
                        {
                            a = ToC((ToI(num1[num1.Length - q - 1]) - 1 - ToI(num2[num1.Length - q - 1]))) + a;
                            c = false;
                        }
                    }
                }
            }

            // Убираем лишние нули начале строки, если таки имеются
            if (a != "")
            {
                while ((a[0] == '0'))
                {
                    if (a.Remove(0, 1) != "")
                    {
                        a = a.Remove(0, 1);
                    }
                    else { a = "0"; break; }
                }
            }

            // Возвращаем строку с искомой разностью двух чисел
            return a;
        }

        // Функция для нахождения произведения двух неотрицательных чисел, заданных в шестнадцатиричной системе исчисления
        public static string M(string num1, string num2)
        {
            // Создаём список, в котором в последствии будут храниться результаты умножения каждой цифры второго числа на первое число для дальнейшего их сложения 
            List<string> cc = new List<string>();
            // Переменная, которую в последствии вернёт функция
            string a = "";

            // Умножаем каждую цифру второго числа на первое число и добавляем результаты в список
            for (int q = 0; q < num2.Length; q++)
            {
                cc.Add(M1(num1, num2[num2.Length - q - 1]) + String.Concat(Enumerable.Repeat("0", q)));
            }

            // Складываем все элементы списка
            foreach (string num in cc)
            {
                a = S(a, num);
            }

            // Возвращаем строку с искомым произведением двух чисел
            return a;
        }

        // Функция для нахождения произведения неотрицательного числа на цифру, заданных в шестнадцатиричной системе исчисления
        public static string M1(string num1, char num2)
        {
            // Строка, которую в последствии вернёт функция
            string a = "";
            // Переменная для бита переноса
            int c = 0;

            // В цикле умножаем каждую цифру числа на цифру, переданную функцие. По необходимости используем ит переноса
            for (int q = 0; q < num1.Length; q++)
            {
                a = ToC(((ToI(num2) * ToI(num1[num1.Length - q - 1])) + c) % 16) + a;
                if ((((ToI(num2) * ToI(num1[num1.Length - q - 1])) + c) / 16) != 0)
                {
                    c = ((ToI(num2) * ToI(num1[num1.Length - q - 1])) + c) / 16;
                }
                else
                {
                    c = 0;
                }
                if ((num1.Length - q - 2) < 0)
                {
                    if (c != 0)
                    {
                        a = c + a;
                    }
                }
            }

            // Возвращаем искомое произведение числа на цифру 
            return a;
        }

        // Функция для нахождения частного двух неотрицательных чисел, заданных в шестнадцатиричной системе исчисления
        public static string F(string _num1, string num2)
        {
            // Переменная, которую в последствии вернёт функция
            string a = "";
            // Переменная, служащая копией делимого
            string num1 = _num1;

            // Выравниваем длины слов
            if (num1.Length > num2.Length)
            {
                A(num1, ref num2);
            }

            // Поскольку частное не может быть дробным
            if (num1.Length < num2.Length)
            {
                return "0";
            }

            // Переменная для имитации деления в столбик, которая будет принимать подстроки исходного числа 
            string numT = "";
            // Переменная для бита переноса
            int c = 0;

            // Находит первую подстроку заданного числа, которая может выступить делимым для заданного делителя, а затемс охранят её и её длину  
            for (int q = 1; q < num1.Length + 1; q++)
            {
                if ((N(num1.Substring(0, q)) / N(num2)) > 0)
                {
                    c = q;
                    numT = num1.Substring(0, q);
                    num1.Remove(0, q);
                    break;
                }
            }

            // До тех пор, пока длина изменяемого делителя не меньше длины заданного делимого выполняет деления и записывает результаты в строку 
            while (c < num1.Length + 1)
            {
                a = a + ToC((N(numT) / N(num2)));
                var m = M(num2, (ToC(N(numT) / N(num2))).ToString());
                numT = D(numT, m);

                if (c < num1.Length)
                {
                    numT = numT + num1[c];
                }

                if (c >= num1.Length)
                {
                    break;
                }

                c++;
            }

            // Если возвращаемая строка так и осталась пустой, частное будет равно нулю
            if (a == "") a = "0";

            // Возвращаем искомое частное двух чисел
            return a;
        }

        // Функция преобразования цифры шестнадцатиричной системы исчисления в число десятиричной
        public static int ToI(char num)
        {
            if ((num == 'A') || (num == 'B') || (num == 'C') || (num == 'D') || (num == 'E') || (num == 'F'))
            {
                return (int)num - 55;
            }
            else if (((num - '0') > 0) && ((num - '0') < 10))
            {
                return num - '0';
            }
            else return 0;
        }

        // Функция преобразования числа десятиричной системы исчисления в цифру шестнадцатиричной
        public static char ToC(BigInteger num)
        {
            if ((num == 10) || (num == 11) || (num == 12) || (num == 13) || (num == 14) || (num == 15))
            {
                return (char)(num + 55);
            }
            else if ((num > 0) && (num < 10))
            {
                return (char)(num + 48);
            }
            else return '0';
        }

        // Функция приведения длины одного неотрицательного числа, заданного в шестнадцатиричной системе исчисления, к длине другого, заданного в той же системе исчисления 
        public static void A(string num1, ref string num2)
        {
            // Конкатенируем нули со второй переменной в таком количестве, чтобы длины двух переданных чисел выравнялись 
            num2 = String.Concat(Enumerable.Repeat("0", num1.Length - num2.Length)) + num2;
        }

        // Функция приведения неотрицательного числа, заданного в шестнадцатиричной системе исчисления, к виду числа заданной длины
        public static void A(ref string num, int c)
        {
            if (c != 0)
            {
                // Конкатенируем нули с заданным числом в таком количестве, чтобы длина числа выравнялась с числом "с"
                num = String.Concat(Enumerable.Repeat("0", c - num.Length)) + num;
            }
        }

        // Функция приведения неотрицательного числа, заданного в шестнадцатиричной системе исчисления, к виду большого числа десятиричной системы исчисления
        public static BigInteger N(string num)
        {
            BigInteger sum = 0;

            // Если число состоит из одной цифры, то просто вернуть значение функции ToI
            if (num.Length == 1)
            {
                return ToI(num[0]);
            }

            // Умножаем каждую цифру числа на соответствующую степень шестнадцати
            for (int q = 0; q < num.Length; q++)
            {
                var n = BigInteger.Pow(16, num.Length - q - 1);
                sum = sum + ToI(num[q]) * n;
            }

            // Возвращаем искомое число
            return sum;
        }

        // Функция нахождения отстатка от деления одного неотрицательного числа, заданного в шестнадцатиричной системе исчисления, на другое, заданное в той же системе исчисления
        public static string R(string num1, string num2)
        {
            // Переменная, которая в последствии будет возвращена
            string a = "";

            if ((num1.Length > num2.Length) || (num1.Length == num2.Length))
            {
                a = D(num1, M(F(num1, num2), num2));
            }
            if (num1.Length < num2.Length)
            {
                a = "0";
            }

            // Возвращаем искомое число
            return a;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////   Lab2   /////////////////////////////////////////////////////////////

        // Функция для вызова функции нахождения наибольшего общего делителя двух неотрицательных чисел, заданных в шестнадцатиричной системе исчисления, с необходимыми параметрами
        public static string G(string num1, string num2)
        {
            // Убираем незначащие нули первого и второго слова 
            while ((num1[0] == '0'))
            {
                if (num1.Remove(0, 1) != "")
                {
                    num1 = num1.Remove(0, 1);
                }
                else
                {
                    num1 = "0";
                    break;
                }
            }
            while ((num2[0] == '0'))
            {
                if (num2.Remove(0, 1) != "")
                {
                    num2 = num2.Remove(0, 1);
                }
                else
                {
                    num2 = "0";
                    break;
                }
            }

            // Используя математические хитрости производим операции над числами так, что алгоритм поиска НСД может быть вызван с меньшими числами без потери истинности ответа
            if (num1.Length == num2.Length)
            {
                if (ToI(num1[0]) > ToI(num2[0]))
                {
                    return G1(num1, num2);
                }
                else
                {
                    return G1(num2, num1);
                }
            }
            if ((num1.Length > num2.Length))
            {
                if ((R(Char.ToString(num1[num1.Length - 1]), "2") == "0"))
                {
                    if ((R(Char.ToString(num2[num2.Length - 1]), "2") == "0"))
                    {
                        string d = "1";
                        while ((R(F(num1, "2"), "2") == "0") && (R(F(num2, "2"), "2") == "0"))
                        {
                            num1 = F(num1, "2");
                            num2 = F(num2, "2");
                            d = M(d, "2");
                        }
                        return M(d, G1(num1, num2));
                    }
                    else
                    {
                        return G1(F(num1, "2"), num2);
                    }
                }
                else
                {
                    if ((R(Char.ToString(num2[num2.Length - 1]), "2") == "0"))
                    {
                        return G1(num1, F(num2, "2"));
                    }
                    else
                    {
                        return G1(num2, D(num1, num2));
                    }
                }
            }
            else
            {
                if ((R(Char.ToString(num1[num1.Length - 1]), "2") == "0"))
                {
                    if ((R(Char.ToString(num2[num2.Length - 1]), "2") == "0"))
                    {
                        string d = "1";
                        while ((R(F(num1, "2"), "2") == "0") && (R(F(num2, "2"), "2") == "0"))
                        {
                            num1 = F(num1, "2");
                            num2 = F(num2, "2");
                            d = M(d, "2");
                        }
                        return M(d, G1(num1, num2));
                    }
                    else
                    {
                        return G1(F(num1, "2"), num2);
                    }
                }
                else
                {
                    if ((R(Char.ToString(num2[num2.Length - 1]), "2") == "0"))
                    {
                        return G1(num1, F(num2, "2"));
                    }
                    else
                    {
                        return G1(num1, D(num2, num1));
                    }
                }
            }
        }

        // Функция для нахождения наибольшего общего делителя двух неотрицательных чисел, заданных в шестнадцатиричной системе исчисления
        public static string G1(string num1, string num2)
        {
            if (N(num1) < N(num2))
            {
                string n;
                n = num1;
                num1 = num2;
                num2 = n;
            }
            if ((N(num1) % N(num2)) == N(num1)) return "1";
            if ((N(num1) % N(num2)) == N("0")) return num2;
            if (num1 == num2) return num1;

            return G1(num2, R(num1, num2));
        }
        static string Min(string x, string y)
        {
            return N(x) < N(y) ? x : y;
        }
        static string Max(string x, string y)
        {
            return N(x) > N(y) ? x : y;
        }
        public static string GCD(string a, string b)
        {
            if (a == "0")
            {
                return b;
            }
            else
            {
                var min = Min(a, b);
                var max = Max(a, b);
                //вызываем метод с новыми аргументами
                //Console.WriteLine("a = {0}, b = {1}", a, b);
                return GCD(D(max, min), min);
            }
        }


        // Функция для возведения неотрицательного числа, заданного в шестнадцатиричной системе исчисления, в заданную неотрицательную степень той же системы исчисления по модулю той же системы исчисления
        private static string ModPow(string baseNum, string exponent, string modulus)
        {
            BigInteger baseNumber = N(baseNum);
            BigInteger Exponent = N(exponent);
            BigInteger Modulus = N(modulus);

            return ModPowB(baseNumber, Exponent, Modulus).ToString("X");
        }
        private static BigInteger ModPowB(BigInteger baseNum, BigInteger exponent, BigInteger modulus)
        {
            BigInteger B, D;
            B = baseNum;

            B %= modulus;
            D = 1;
            if ((exponent & 1) == 1)
            {
                D = B;
            }

            while (exponent > 1)
            {
                exponent >>= 1;
                B = (B * B) % modulus;
                if ((exponent & 1) == 1)
                {
                    D = (D * B) % modulus;
                }
            }
            return (BigInteger)D;
        }

        //Баррет
        public static string B(string num1, string d)
        {
            var k = d.Length;
            string m = F((BigInteger.Pow(256, k)).ToString("X"), d);
            string r = "";

            string q = F(num1, (BigInteger.Pow(16, k - 1)).ToString("X"));
            q = M(q, m);
            q = F(q, (BigInteger.Pow(16, k + 1)).ToString("X"));
            r = D(num1, M(q, d));

            while (r.Length > d.Length)
            {
                r = D(r, d);
            }

            return r;
        }

        // Функция для приведения неотрицательного числа, заданного в шестнадцатиричной системе исчисления, к виду числа, заданного в двоичной системе исчисления
        public static string T(string num)
        {
            // Переменная, которая в последствии будет возвращена
            string a = "";
            // Переменная для хранения десятичной записи заданного числа
            BigInteger d = N(num);
            // Вспомогательная переменная
            BigInteger d1 = 0;

            // Делим до тех пор, пока число не исчерпается
            while (d > 0)
            {
                d1 = d % 2;
                d = d / 2;
                a = d1 + a;
            }

            // Возвращаем искомую двоичную запись заданного числа
            return a;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        static void Main(string[] args)
        {
            // Устанавливаем поддержку русского языка в консоли
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Перменная для общения с пользователем
            string input = "";

            // Основной цикл программы
            while (input != "esc")
            {
                Console.WriteLine("\nВыберите действие: ");
                Console.WriteLine("1. Нахождение суммы двух неотрицательных чисел.");
                Console.WriteLine("2. Нахождение разности двух неотрицательных чисел.");
                Console.WriteLine("3. Нахождение произведения двух неотрицательных чисел.");
                Console.WriteLine("4. Нахождение частного двух неотрицательных чисел.");
                Console.WriteLine("5. Приведение к виду большого числа заданной длины.");
                Console.WriteLine("6. Возведение в заданную степень неотрицательного числа");
                Console.WriteLine("7. Нахождение отстатка от деления двух неотрицательных чисел.");
                Console.WriteLine("\n8. Нахождение наибольшего общего делителя двух неотрицательных чисел.");
                Console.WriteLine("9. Нахождение наименьшего общего кратного двух неотрицательных чисел.");
                Console.WriteLine("10. Нахождение суммы двух неотрицательных чисел по заданному модулю.");
                Console.WriteLine("11. Нахождение разности двух неотрицательных чисел по заданному модулю.");
                Console.WriteLine("12. Возведение неотрицательного числа в неотрицательную степень по заданному модулю.");
                Console.WriteLine("13. Нахождение произведения двух неотрицательных чисел по заданному модулю.");
                Console.WriteLine("14. Нахождение числа по заданному модулю.");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Введите первое число");
                        string num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        string num2 = Console.ReadLine();
                        Console.WriteLine("Сумма чисел {0} и {1} равна {2}", num1, num2, S(num1, num2));
                        break;
                    case "2":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Разность чисел {0} и {1} равна {2}", num1, num2, D(num1, num2));
                        break;
                    case "3":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Произведение чисел {0} и {1} равно {2}", num1, num2, M(num1, num2));
                        break;
                    case "4":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Частное чисел {0} и {1} равно {2}", num1, num2, F(num1, num2));
                        break;
                    case "5":
                        Console.WriteLine("Введите число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите необходимую длину выходного числа");
                        string e = Console.ReadLine();
                        Console.WriteLine("Число {0} приведено к виду числа с {1} знаками: ", num1, e);
                        A(ref num1, Int32.Parse(e));
                        Console.Write(num1);
                        break;
                    case "6":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите степень, в которую необходимо возвести данное число");
                        string d = Console.ReadLine();
                        Console.WriteLine("Число {0} в {1} степени равно {2}", num1, d, ModPow(num1, d, "1"));
                        break;
                    case "7":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Остаток от деления числа {0} на {1} равен {2}", num1, num2, B(num1, num2));
                        break;
                    case "8":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Наибольший общий делитель чисел {0} и {1} равен {2}", num1, num2, GCD(num1, num2));
                        break;
                    case "9":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Наименьшее общее кратное чисел {0} и {1} равен {2}", num1, num2, F(M(num1, num2), GCD(num1, num2)));
                        break;
                    case "10":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Введите модуль");
                        d = Console.ReadLine();
                        Console.WriteLine("Сумма чисел {0} и {1} по модулю {2} равна {3}", num1, num2, d, R(S(num1, num2), d));
                        break;
                    case "11":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Введите модуль");
                        d = Console.ReadLine();
                        Console.WriteLine("Разность чисел {0} и {1} по модулю {2} равна {3}", num1, num2, d, R(D(num1, num2), d));
                        break;
                    case "12":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите степень");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Введите модуль");
                        d = Console.ReadLine();
                        Console.WriteLine("Число {0} в степени {1} по модулю {2} равно {3}", num1, num2, d, ModPow(num1, num2, d));
                        break;
                    case "13":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите второе число");
                        num2 = Console.ReadLine();
                        Console.WriteLine("Введите модуль");
                        d = Console.ReadLine();
                        Console.WriteLine("Произведение чисел {0} и {1} по модулю {2} равно {3}.", num1, num2, d, R(M(num1, num2), d));
                        break;
                    case "14":
                        Console.WriteLine("Введите первое число");
                        num1 = Console.ReadLine();
                        Console.WriteLine("Введите модуль");
                        d = Console.ReadLine();
                        Console.WriteLine("Число {0} по модулю {1} равно {2}.", num1, d, B(num1, d));
                        break;
                    case "15":
                        string num_1 = "87D6D58D3991D536544389CEFA72FD0EBED75B2EBDC2C79BC3717793108F0952011E7E2D7040FFFB32F10BEB8ED0A485026B6860020B230128A8222B0525A6888942FB01C537800BF25D6F021D4B99D3CBD6DF9055FA22F91A6CFC4FDFC408AEF78F6418D3CE4E20EC7888B61BAE3D73C27C257CCA905DE0353C3A7CFFD9FE15";
                        string num_2 = "791EDB102DA183759979CEF70E1405AF14B98CD44357EADF6A8E35E49F99BB56CBD3F68897D6E05502ED1DE14EC46D04F96992C2D129737987E84E62371648B37633794016852A8CBFFCFDE06B17EC216AE8914D59E677A15A90361A594F0D1524A41AE63C59D343D4E522646722B0292DD7C85571AC9A84FDA6CD2D8DE307F6";
                        string num_3 = "2AB3786D3A85E62EC763A05A73A7F08D21EEE3CBCAE207E40854121BFF8258F7B2B293B0D30277CDB987A6FCB5BF28B68D8E68ABA88DED37BD80A879A1BB53E3";


                        Console.WriteLine("\nСумма элементов: a + b = c");
                        Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2}", num_1, num_2, S(num_1, num_2));
                        Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2}", num_2, num_3, S(num_2, num_3));
                        Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2}", num_3, num_1, S(num_3, num_1));

                        Console.WriteLine("\nПроизведение элементов: a * b = c");
                        Console.WriteLine("\n\n{0} \n* \n{1} \n= \n{2}", num_1, num_2, M(num_1, num_2));
                        Console.WriteLine("\n\n{0} \n* \n{1} \n= \n{2}", num_2, num_3, M(num_2, num_3));
                        Console.WriteLine("\n\n{0} \n* \n{1} \n= \n{2}", num_3, num_1, M(num_3, num_1));

                        Console.WriteLine("\nСтепень числа: a в степени b равно c");
                        Console.WriteLine("\n\n{0} \n** \n{1} \n% \n{2} \n= \n{3}", "ADBBDA", "12D", "EAE", ModPow("ADBBDA", "12D", "EAE"));

                        Console.WriteLine("\n\nПРОВЕРКА: ");

                        Console.WriteLine("\nСимметричность сложения: a + b = b + a");
                        Console.WriteLine("\n\n{0} \n+ \n{1} \n= \n{2} \n== \n{3} \n= \n{1} \n+ \n{0}", num_1, num_2, S(num_1, num_2), S(num_2, num_1));

                        Console.WriteLine("\nСимметричность умножения: a * b = b * a");
                        Console.WriteLine("\n\n{0} \n* \n{1} \n= \n{2} \n== \n{3} \n= \n{1} \n* \n{0}", num_1, num_2, M(num_1, num_2), M(num_2, num_1));

                        Console.WriteLine("\nДистрибутивность: (a + b) * c = (a * c) + (b * c)");
                        Console.WriteLine("\n\n({0} + {1}) \n* \n{2} \n= \n{3} \n== \n{4} \n= \n({0} * {2}) \n+ \n({1} * {2})", num_1, num_2, num_3, (M(S(num_1, num_2), num_3)), S(M(num_1, num_3), M(num_2, num_3)));

                        Console.WriteLine("\nДеление и умножение: a \\ b * b = a");
                        Console.WriteLine("\n\n{0} \n\\ \n{1} \n* \n{1} \n= \n{2}", num_1, num_2, M(F(num_1, num_2), num_1));

                        break;
                    default:
                        Console.WriteLine("Для завершения нажмите любую клавишу.");
                        break;
                }
            }




            // Ожидаем нажатие произвольной клавиши для завершения работы программы
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
