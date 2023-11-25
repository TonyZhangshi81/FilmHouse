using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils.PasswordGenerator
{
    public class DefaultPasswordGenerator : IPasswordGenerator
    {
        private static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();

        private static readonly char[] StartingChars = new char[2] { '<', '&' };

        public string GeneratePassword(PasswordRule rule = null)
        {
            if (rule != null)
            {
                return GeneratePasswordAspNetMembership(rule.Length, rule.LeastNumberOfNonAlphanumericCharacters);
            }

            return GeneratePasswordDefault();
        }

        public static string GeneratePasswordAspNetMembership(int length, int leastNumberOfNonAlphanumericCharacters)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            if (leastNumberOfNonAlphanumericCharacters > length || leastNumberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentOutOfRangeException("leastNumberOfNonAlphanumericCharacters");
            }

            string text;
            int matchIndex;
            do
            {
                byte[] array = new byte[length];
                char[] array2 = new char[length];
                int num = 0;
                RandomNumberGenerator.Create().GetBytes(array);
                for (int i = 0; i < length; i++)
                {
                    int num2 = (int)array[i] % 87;
                    if (num2 < 36)
                    {
                        if (num2 < 10)
                        {
                            array2[i] = (char)(48 + num2);
                        }
                        else
                        {
                            array2[i] = (char)(65 + num2 - 10);
                        }
                    }
                    else if (num2 < 62)
                    {
                        array2[i] = (char)(97 + num2 - 36);
                    }
                    else
                    {
                        array2[i] = Punctuations[num2 - 62];
                        num++;
                    }
                }

                if (num < leastNumberOfNonAlphanumericCharacters)
                {
                    Random random = new Random();
                    for (int j = 0; j < leastNumberOfNonAlphanumericCharacters - num; j++)
                    {
                        int num3;
                        do
                        {
                            num3 = random.Next(0, length);
                        }
                        while (!char.IsLetterOrDigit(array2[num3]));
                        array2[num3] = Punctuations[random.Next(0, Punctuations.Length)];
                    }
                }

                text = new string(array2);
            }
            while (IsDangerousString(text, out matchIndex));
            return text;
        }

        private static bool IsDangerousString(string s, out int matchIndex)
        {
            matchIndex = 0;
            int startIndex = 0;
            while (true)
            {
                int num = s.IndexOfAny(StartingChars, startIndex);
                if (num < 0)
                {
                    return false;
                }

                if (num == s.Length - 1)
                {
                    break;
                }

                matchIndex = num;
                switch (s[num])
                {
                    case '<':
                        if (IsAtoZ(s[num + 1]) || s[num + 1] == '!' || s[num + 1] == '/' || s[num + 1] == '?')
                        {
                            return true;
                        }

                        break;
                    case '&':
                        if (s[num + 1] == '#')
                        {
                            return true;
                        }

                        break;
                }

                startIndex = num + 1;
            }

            return false;
        }

        private static bool IsAtoZ(char c)
        {
            switch (c)
            {
                case 'A':
                case 'B':
                case 'C':
                case 'D':
                case 'E':
                case 'F':
                case 'G':
                case 'H':
                case 'I':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'O':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'U':
                case 'V':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                case 'a':
                case 'b':
                case 'c':
                case 'd':
                case 'e':
                case 'f':
                case 'g':
                case 'h':
                case 'i':
                case 'j':
                case 'k':
                case 'l':
                case 'm':
                case 'n':
                case 'o':
                case 'p':
                case 'q':
                case 'r':
                case 's':
                case 't':
                case 'u':
                case 'v':
                case 'w':
                case 'x':
                case 'y':
                case 'z':
                    return true;
                default:
                    return false;
            }
        }

        private static string GeneratePasswordDefault()
        {
            char[] array = Enumerable.Concat(second: "aquickbrownfoxjumpedoverthelazydog".ToUpper().ToArray(), first: "aquickbrownfoxjumpedoverthelazydog".ToArray()).ToArray();
            int[] array2 = Enumerable.Range(0, 10).ToArray();
            char[] array3 = new char[8] { '@', '$', '!', '%', '*', '#', '?', '&' };
            char[] array4 = new char[10];
            for (int i = 0; i < 6; i++)
            {
                int @int = RandomNumberGenerator.GetInt32(0, array.Length);
                array4[i] = array[@int];
            }

            for (int j = 0; j < 3; j++)
            {
                int int2 = RandomNumberGenerator.GetInt32(0, array2.Length);
                array4[6 + j] = (char)(array2[int2] + 48);
            }

            int int3 = RandomNumberGenerator.GetInt32(0, array3.Length);
            array4[9] = array3[int3];
            return new string(array4.OrderBy((char p) => Guid.NewGuid()).ToArray());
        }
    }
}
