using System;
using System.IO;

namespace Task1
{
    class Program
    {
        static int find(string s)
        {
            string r;
            return find(s, out r);
        }
        static int find(string s, out string r)
        {
            r = string.Empty;
            foreach (char c in s)
                if (!r.Contains(c.ToString()))
                    r = r + c;

            if (s.Length == r.Length)
                return factorial(s.Length);
            return factorial(s.Length) / (factorial(s.Length - r.Length) * 1);
        }
        static int factorial(int num)
        {
            switch (num)
            {
                case 0: case 1:
                    return 1;
                default:
                    return num * factorial(num - 1);
            }
        }
        static string[] MakeVariations(string s)
        {
            if (s.Length <= 1)
                return new[] { s };
            {
                string r;
                find(s, out r);
                if (s.Length == 2)
                    if (r.Length == 2)
                        return new[] { s, s[1].ToString() + s[0] };
                    else
                        return new[] { s };

                if (r == s)
                    return DumbMakeVariations(s);
            }
            
            var result = new string[find(s)];
            for (int i = 0,k = 0; i < s.Length; i++)
            {
                string s1 = string.Empty;
                for (int j = 0; j < s.Length; j++)
                    if(j != i)
                        s1 = s1 + s[j];

                bool skip = false;
                for (int j = 0; j < result.Length && !string.IsNullOrEmpty(result[j]) && !skip; j++)
                    if (s[i] + s1 == result[j])
                        skip = true;
                if(skip)
                    continue;

                var res1 = MakeVariations(s1);
                for (int index = 0; index < res1.Length; index++)
                {
                    string temp = string.Empty;
                    if (!string.IsNullOrEmpty(res1[index]))
                        temp = s[i] + res1[index];
                    else
                        break;

                    for (int j = 0; j < result.Length; j++)
                    {
                        if (result[j] == temp)
                            break;
                        if (string.IsNullOrEmpty(result[j]))
                        {
                            result[k++] = temp;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        static string[] DumbMakeVariations(string s)
        {
            if (s.Length <= 1)
                return new[] { s };

            var result = new string[factorial(s.Length)];

            for (int i = 0, k = 0; i < s.Length; i++)
            {
                string s1 = string.Empty;
                for (int j = 0; j < s.Length; j++)
                    if (j != i)
                        s1 = s1 + s[j];


                var res1 = DumbMakeVariations(s1);
                foreach (string str in res1)
                    result[k++] = s[i] + str;
            }
            return result;
        }
        static string[] RemoveDuplicates(string[] s)
        {
            var result = new string[find(s[0])];
            for (int i = 0,k = 0; i < s.Length; i++)
            {
                for (int j = 0; j < result.Length; j++)
                {
                    if (s[i] == result[j])
                        break;
                    if (!string.IsNullOrEmpty(result[j]))
                        continue;
                    result[k++] = s[i];
                    break;
                }
            }
            return result;
        }


        static void Main(string[] args)
        {
            string input;
            using (var fs = new FileStream("INPUT.TXT", FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    input = sr.ReadLine();
                }
            }

            string output = string.Empty;
            {
                //var outputArray = RemoveDuplicates(DumbMakeVariations(input));
                var outputArray = MakeVariations(input);
                for (int i = 0; i < outputArray.Length && !string.IsNullOrEmpty(outputArray[i]); i++)
                        output = output + outputArray[i] + Environment.NewLine;
            }

            using (var fs = new FileStream("OUTPUT.TXT", FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < output.Length - 1; i++)
                        sw.Write(output[i]);
                }
            }
        }
    }
}
