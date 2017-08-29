using System;
using System.IO;
using System.Text;

namespace Task1
{
    class Program
    {
        static long Factorial(long num)
        {
            if (num == 0)
                return 1;
            return num * Factorial(num - 1);
        }
        static string[] MakeVariations(string s)
        {
            if (s.Length <= 1)
                return new[] { s };
            
            var result = new string[Factorial(s.Length)];
            for (int i = 0,k = 0; i < s.Length; i++)
            {
                string s1 = string.Empty;
                for (int j = 0; j < s.Length; j++)
                    if(j != i)
                        s1 = s1 + s[j];
                var res1 = MakeVariations(s1);

                foreach (string s2 in res1)
                    if (Array.IndexOf(result, s[i] + s2) != -1)
                        result[k++] = s[i] + s2;
            }

            return result;
        }

        static void Main(string[] args)
        {
            string input;

            using (var fs = new FileStream("INPUT.TXT",FileMode.Open))
            {
                using (var sr = new StreamReader(fs,Encoding.Default))
                {
                    input = sr.ReadToEnd();
                }
            }
            //input = input.Replace("\n", "").Replace("\r", "").Replace(" ", "");



        }
    }
}
