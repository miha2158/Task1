using System;
using System.IO;
using System.Text;

namespace Task1
{
    class Program
    {
        static int find(StringBuilder s)
        {
            StringBuilder r;
            return find(s, out r);
        }
        static int find(StringBuilder s, out StringBuilder r)
        {
            string rr = string.Empty;
            for (int i = 0; i < s.Length; i++)
                if (!rr.Contains(s[i].ToString()))
                    rr = rr + s[i];
            r = new StringBuilder(rr);

            if (s.Length == r.Length)
                return factorial(s.Length);
            return factorial(s.Length) / (factorial(s.Length - r.Length) * 1);
        }
        static int factorial(int num)
        {
            switch (num)
            {
                case 0:
                case 1:
                    return 1;
                default:
                    return num * factorial(num - 1);
            }
        }

        static StringBuilder[] MakeVariations(StringBuilder s)
        {
            if (s.Length <= 1)
                return new[] { s };
            {
                StringBuilder r;
                find(s, out r);
                if (s.Length == 2)
                    if (r.Length == 2)
                        return new[] { s, new StringBuilder(s[1].ToString() + s[0]) };
                    else
                        return new[] { s };

                if (r.Equals(s))
                    return DumbMakeVariations(s);
            }

            var result = new StringBuilder[find(s)];
            for (int i = 0; i < result.Length; i++)
                result[i] = new StringBuilder(0);

            for (int i = 0, k = 0; i < s.Length; i++)
            {
                var s1 = new StringBuilder(0);
                for (int j = 0; j < s.Length; j++)
                    if (j != i)
                        s1.Append(s[j]);

                bool skip = false;
                for (int j = 0; j < result.Length && result[j].Length != 0 && !skip; j++)
                    if ((s[i] + s1.ToString()).Equals(result[j].ToString()))
                        skip = true;
                if (skip)
                    continue;

                var res1 = MakeVariations(s1);
                for (int index = 0; index < res1.Length; index++)
                {
                    var temp = new StringBuilder(s[i].ToString());
                    if (res1[index].Length != 0)
                        temp.Append(res1[index]);
                    else
                        break;

                    for (int j = 0; j < result.Length; j++)
                    {
                        if (result[j].Equals(temp))
                            break;
                        if (result[j].Length != 0)
                            continue;
                        result[k++] = temp;
                        break;
                    }
                }
            }
            return result;
        }
        static StringBuilder[] DumbMakeVariations(StringBuilder s)
        {
            if (s.Length <= 1)
                return new[] { s };

            var result = new StringBuilder[factorial(s.Length)];
            for (int i = 0; i < result.Length; i++)
                result[i] = new StringBuilder(0);

            for (int i = 0, k = 0; i < s.Length; i++)
            {
                StringBuilder s1 = new StringBuilder(0);
                for (int j = 0; j < s.Length; j++)
                    if (j != i)
                        s1.Append(s[j]);


                var res1 = DumbMakeVariations(s1);
                foreach (StringBuilder str in res1)
                {
                    result[k].Append(s[i]);
                    result[k++].Append(str);
                }
            }
            return result;
        }

        static string[] RemoveDuplicates(string[] s)
        {
            var result = new string[find(new StringBuilder(s[0]))];
            for (int i = 0; i < result.Length; i++)
                result[i] = string.Empty;

            for (int i = 0, k = 0; i < s.Length; i++)
                for (int j = 0; j < result.Length; j++)
                {
                    if (s[i] == result[j])
                        break;
                    if (result[j].Length != 0)
                        continue;
                    result[k++] = s[i];
                    break;
                }
            return result;
        }

        static string[] AnotherAlgorithm(StringBuilder s)
        {
            var result = new string[factorial(s.Length) + 1];
            for (int i = 0; i < result.Length; i++)
                result[i] = string.Empty;

            step(s.ToString(), result, 0);

            return result;
        }
        static void step(string s, string[] result, int indexRes)
        {
            if (s.Length == 1)
            {
                result[indexRes] = s;
                return;
            }

            for (int i = 0; i < s.Length; i++)
            {
                string si = string.Empty;
                for (int j = 0; j < s.Length; j++)
                    if (j != i)
                        si = si + s[j];

                step(si, result, indexRes);

                for (int j = indexRes; j < result.Length && result[j] != string.Empty; j++, indexRes++)
                    result[j] = s[i] + result[j];
            }
        }



        static void Main(string[] args)
        {
            StringBuilder input;
            using (var fs = new FileStream("INPUT.TXT", FileMode.Open))
                using (var sr = new StreamReader(fs))
                    input = new StringBuilder(sr.ReadLine());
            
            using (var fs = new FileStream("OUTPUT.TXT", FileMode.Create))
                using (var sw = new StreamWriter(fs))
                {
                    var outputArray = RemoveDuplicates(AnotherAlgorithm(input));
                    sw.Write(outputArray[0]);
                    for (int i = 1; i < outputArray.Length && outputArray[i] != string.Empty; i++)
                        sw.Write(Environment.NewLine + outputArray[i]);
                }
        }
    }
}