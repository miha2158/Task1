using System;
using System.IO;

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
                    if (Array.IndexOf(result, s[i] + s2) == -1)
                        result[k++] = s[i] + s2;
            }

            {
                int size = 0;
                foreach (string s1 in result)
                    if (!string.IsNullOrEmpty(s1))
                        size++;
                var temp = new string[size];

                int i = 0;
                foreach (var s1 in result)
                    if (!string.IsNullOrEmpty(s1))
                        temp[i++] = s1;
                result = temp;
            }

            return result;
        }

        static void Main(string[] args)
        {
            string input;
            {
                var fs1 = new FileStream("INPUT.TXT", FileMode.OpenOrCreate);
                var sr = new StreamReader(fs1);
                input = sr.ReadToEnd();
                sr.Close();
                fs1.Close();
            }
            //input = input.Replace("\n", "").Replace("\r", "").Replace(" ", "");

            string[] outputArray = MakeVariations(input);
            string output = string.Empty;
            foreach (string s in outputArray)
                output = output + s.Trim() + Environment.NewLine;
            output = output.Trim();

            {
                var fs2 = new FileStream("OUTPUT.TXT", FileMode.Create);
                var sw = new StreamWriter(fs2);
                sw.Write(output);
                sw.Close();
                fs2.Close();
            }
        }
    }
}
