using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3
{
    class Program
    {
        public const string ALPHABET_DEFAULT = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Тип шифрования:");
                Console.WriteLine("1 - Многоалфавитная");
                Console.WriteLine("2 - Моноалфавитная");
                Console.Write("Введите тип шифрования:");

                var actionNumber = Console.ReadLine();

                switch (actionNumber)
                {
                    case "1":
                        {
                            Console.WriteLine("Действия:");
                            Console.WriteLine("1 - Шифровать");
                            Console.WriteLine("2 - Расшифровать");
                            Console.Write("Введите номер действия:");

                            var innerActionNumber = Console.ReadLine();

                            switch (innerActionNumber)
                            {
                                case "1":
                                    {
                                        Encrypt();
                                        break;
                                    }
                                case "2":
                                    {
                                        Decrypt();
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Введите корректное действие");
                                        break;
                                    }
                            }
                            break;
                        }
                    case "2":
                        {

                            Console.WriteLine("Действия:");
                            Console.WriteLine("1 - Шифровать");
                            Console.WriteLine("2 - Расшифровать");
                            Console.Write("Введите номер действия:");

                            var innerActionNumber = Console.ReadLine();

                            switch (innerActionNumber)
                            {
                                case "1":
                                    {
                                        MonoEncrypt();
                                        break;
                                    }
                                case "2":
                                    {
                                     
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Введите корректное действие");
                                        break;
                                    }
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Введите корректное действие");
                            break;
                        }
                }

                
            }
        }

        private static void Encrypt()
        {
            List<string> keyMatrix = LoadKeyMatrix();

            Console.WriteLine("Введите текст:");
            var text = Console.ReadLine().ToUpper();
            Console.WriteLine("Введите ключ:");
            var key = Console.ReadLine().ToUpper();

            var checkKey = CheckKey(text, key);

            if (!checkKey)
            {
                Console.WriteLine("Некорректный ключ");
            }

            var checkSymbols = CheckSymbols(text, key);

            if (!checkSymbols)
            {
                Console.WriteLine("Используйте только буквы русского алфавита");
            }

            var textResult = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    textResult += ' ';
                }
                else
                {
                    var textSymbol = text[i];
                    var keySymbol = key[i];

                    var symbolPosition = ALPHABET_DEFAULT.IndexOf(textSymbol);

                    var replacedSymbol = keyMatrix.First(x => x[0] == keySymbol)[symbolPosition];

                    textResult += replacedSymbol;
                }
            }

            Console.WriteLine($"Результат шифрования: {textResult}");
            Console.ReadKey();
        }

        private static void Decrypt()
        {
            List<string> keyMatrix = LoadKeyMatrix();

            Console.Write("Введите зашифрованный текст: ");
            var text = Console.ReadLine().ToUpper();
            Console.Write("Введите ключ:");
            var key = Console.ReadLine().ToUpper();

            var checkKey = CheckKey(text, key);

            if (!checkKey)
            {
                Console.WriteLine("Некорректный ключ");
            }

            var checkSymbols = CheckSymbols(text, key);

            if (!checkSymbols)
            {
                Console.WriteLine("Используйте только буквы русского алфавита");
            }

            var textResult = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    textResult += ' ';
                }
                else
                {
                    var textSymbol = text[i];
                    var keySymbol = key[i];

                    var symbolPosition = keyMatrix.First(x => x[0] == keySymbol).IndexOf(textSymbol);

                    var replacedSymbol = ALPHABET_DEFAULT[symbolPosition];

                    textResult += replacedSymbol;
                }
            }

            Console.WriteLine($"Результат расшифровки: {textResult}");
            Console.ReadKey();
        }

        private static void MonoEncrypt()
        {
            Console.WriteLine($"Алфавит: {ALPHABET_DEFAULT}");
            Console.Write("Введите зашифрованный текст:");
            var text = Console.ReadLine().ToUpper();
            Console.Write($"Введите коэфициент сдвига от 1 до {ALPHABET_DEFAULT.Length}: ");
            var s = Console.ReadLine();

            var isCorrectS = int.TryParse(s, out int completedS);

            if(!isCorrectS || completedS < 1 || completedS > ALPHABET_DEFAULT.Length)
            {
                Console.WriteLine("Некорректный сдвиг.");
                return;
            }

            var crypt = ALPHABET_DEFAULT.Substring(completedS) + ALPHABET_DEFAULT.Substring(0, completedS);

            Console.WriteLine($"Шифр: {crypt}");

            var textResult = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                var symbolIndex = ALPHABET_DEFAULT.IndexOf(text[i]);
                textResult += crypt[symbolIndex];
            }

            Console.WriteLine($"Результат шифрования: {textResult}");
        }

        private static void MonoDecrypt()
        {
           
        }

        private static bool CheckSymbols(string text, string key) 
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!ALPHABET_DEFAULT.Contains(text[i]) || !ALPHABET_DEFAULT.Contains(key[i]))
                    return false;
            }

            return true;
        }


        private static bool CheckKey(string text, string key)
        {
            if (text.Length != key.Length)
                return false;

            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] == ' ' && text[i] != key[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static List<string> LoadKeyMatrix()
        {
            var result = new List<string>();
            result.Add(ALPHABET_DEFAULT);

            for (int i = 1; i < ALPHABET_DEFAULT.Length; i++) 
            {
                var str = ALPHABET_DEFAULT.Substring(i) + ALPHABET_DEFAULT.Substring(0, i);
                result.Add(str);
            }

            return result;
        } 
    }
}
