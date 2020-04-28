using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMod;

namespace TextMod
{
    public static class Extensions
    {
        public static Dictionary<char, string> nato;
        public static Dictionary<char, string> sounds;
        public static readonly char[] numbers = "1234567890".ToCharArray();
        public static readonly string[] _numbers = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "zero" };


        /// <summary>
        /// Wepwaces evewy occuwwence of L and R with W.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToFurry(this String str)
        {
            return str.ToLower().Replace('l', 'w').Replace('r', 'w');
        }
        /// <summary>
        /// mAkEs TeXt lOoK lIkE tHiS.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Oddify(this String str, int type)
        {
            char[] ca = str.ToCharArray();
            string[] sclone = new string[ca.Length];
            int i = -1;
            foreach (char c in ca)
            {
                string s = c.ToString();
                i++;
                if (i % 2 == 0)
                {
                    s = s.ToLower();
                }
                else
                { s = s.ToUpper(); }
                sclone[i] = s;
            }
            if(type == 0) {
                return string.Join(" ", sclone);
            } else {
                return string.Join("", sclone);
            }
            
        }
        /// <summary>
        /// Reverses the text.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(this String str)
        {
            char[] ca = str.ToCharArray();
            Array.Reverse(ca);
            return new string(ca);
        }
        /// <summary>
        /// Use a dictionary to replace the words in a string.
        /// Example:
        /// mydictionary.Add("one", "Pee Pee Lmao");
        /// string mystring = "test one, test two";
        /// string yes = mystring.Dictionary(mydictionary);
        /// 
        /// (yes = "test Pee Pee Lmao, test two")
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dic">A dictionary of the words to process.</param>
        /// <returns></returns>
        public static string Dictionary(this String str, Dictionary<string, string> dic)
        {
            string s = str.Replace("b", ":b:");
            string[] words = s.Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach(string word in words)
            {
                dic.TryGetValue(word.ToLower(), out string newword);
                if(newword == null)
                {
                    sb.Append(" " + word);
                    continue;
                }
                sb.Append(" " + word + " " + newword);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Clappy bois
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Clap(this String str)
        {
            return str.Replace(" ", ":clap:");
        }
        public static string RegionalIndicators(this String str, char[] alphabet)
        {
            char[] chars = str.ToCharArray();
            string[] clone = new string[chars.Length];
            int i = -1;
            foreach(char c in chars)
            {
                i++;
                if(c.Equals(' '))
                {
                    clone[i] = ":blue_circle: ";
                    continue;
                }
                if (alphabet.Contains(c))
                {
                    clone[i] = ":regional_indicator_" + c + ": ";
                    continue;
                } else if (numbers.Contains(c)) {
                    int index = numbers.ToList().IndexOf(c);
                    string word = _numbers[index];
                    clone[i] = ":" + word + ":";
                    continue;
                } else
                    {

                    clone[i] = c.ToString();
                    continue;
                }
            }
            return String.Join("", clone);
        }
        public static string Spoiler(this String str)
        {
            char[] chars = str.ToCharArray();
            string[] clone = new string[chars.Length];
            int i = -1;
            foreach(char c in chars)
            {
                i++;
                clone[i] = "||" + c + "||";
                continue;
            }
            return String.Join("", clone);
        }
        public static string NATO(this String str)
        {
            char[] chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach(char oldc in chars)
            {
                string _c = oldc.ToString();
                char c = _c.ToLower().ToCharArray()[0];
                if(nato.ContainsKey(c))
                {
                    nato.TryGetValue(c, out string value);
                    sb.Append(" " + value);
                } else
                {
                    sb.Append(c.ToString());
                }
            }
            try
            {
                sb = sb.Remove(0, 1);
            } catch(ArgumentOutOfRangeException){}
            return sb.ToString();
        }
        public static string Sounds(this String str)
        {
            char[] chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (char oldc in chars)
            {
                string _c = oldc.ToString();
                char c = _c.ToLower().ToCharArray()[0];
                if (sounds.ContainsKey(c))
                {
                    sounds.TryGetValue(c, out string value);
                    sb.Append("-" + value);
                }
                else if(c.Equals(' '))
                {
                    sb.Remove(sb.Length - 1, 1).Append(' ');
                } else
                {
                    sb.Append(c);
                }
            }
            try
            {
                sb = sb.Remove(0, 1);
            }
            catch (ArgumentOutOfRangeException) { }
            return sb.ToString();
        }
        public static string MapChars(this String str, char[] start, char[] end)
        {
            char[] chars = str.ToCharArray();
            char[] clone = new char[chars.Length];
            List<char> _start = start.ToList();
            int i = -1;
            foreach(char c in chars)
            {
                i++;
                if(start.Contains(c))
                {
                    int index = _start.IndexOf(c);
                    clone[i] = end[index];
                    continue;
                } else {
                    clone[i] = c;
                    continue;
                }
            }
            return new string(clone);
        }
        public static string[] MapCharsToArray(this String str, char[] start, string[] end)
        {
            char[] chars = str.ToCharArray();
            string[] clone = new string[chars.Length];
            List<char> _start = start.ToList();
            int i = -1;
            foreach (char c in chars)
            {
                i++;
                if (start.Contains(c))
                {
                    int index = _start.IndexOf(c);
                    clone[i] = end[index];
                    continue;
                }
                else
                {
                    clone[i] = c.ToString();
                    continue;
                }
            }
            return clone;
        }
        public static string MapAppendChars(this String str, char[] start, string[] end)
        {
            string[] a = str.MapCharsToArray(start, end);
            string b = str;
            StringBuilder sb = new StringBuilder();
            int i = -1;
            foreach(char c in b)
            {
                i++;
                sb.Append(c + a[i]);
            }
            return sb.ToString();
        }
        public static string ToNumbers(this String str)
        {
            char[] chars = str.ToCharArray();
            char[] ab = Form1.alphabet;
            List<char> _ab = ab.ToList();
            StringBuilder sb = new StringBuilder();
            int i = -1;
            foreach (char c in chars)
            {
                i++;
                if(ab.Contains(c))
                {
                    int index = _ab.IndexOf(c)+1;
                    sb.Append(" " + index);
                } else
                {
                    sb.Append(" 0");
                }
            }
            try
            {
                sb.Remove(0, 1);
            } catch(ArgumentOutOfRangeException){}
            return sb.ToString();
        }
        public static string ToBinary(this String str)
        {
            char[] chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            //int i = -1;
            foreach (char c in chars)
            {
                string cs = Convert.ToString(c, 2).PadLeft(8, '0');
                sb.Append(cs + " ");
            }
            try
            {
                sb.Remove(sb.Length - 1, 1);
            }
            catch (ArgumentOutOfRangeException) { }
            return sb.ToString();
        }
        public static string Spaced(this String str)
        {
            char[] chars = str.ToCharArray();
            return string.Join(" ", chars);
        }
        public static IEnumerable<String> SplitInParts(this String s, int partLength)
        {
            var whole = s;
            var partSize = partLength;
            return Enumerable.Range(0, (whole.Length + partSize - 1) / partSize)
                .Select(i => whole.Substring(i * partSize, Math.Min(whole.Length - i * partSize, partSize)));
        }
        public static string Pyramid(this String str)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 1; i < str.Length; i++)
            {
                string sub = str.Substring(0, i);
                if(sub.EndsWith(" ")) { continue; }
                sb.Append(sub + "\n");
            }
            for (int i = str.Length; i >= 0; i--)
            {
                string sub = str.Substring(0, i);
                if (sub.EndsWith(" ")) { continue; }
                sb.Append(sub + "\n");
            }
            return sb.ToString();
        }
        public static string Scramble(this String str)
        {
            // dont deadlock dammit
            Random rand = new Random();
            List<int> available = new List<int>(str.Length);
            char[] s = new char[str.Length];
            for(int i = 0; i < str.Length; i++ )
            {
                available.Add(i);
            }
            int x = -1;
            while(available.Count() > 0)
            {
                x++;
                int r = rand.Next(str.Length);
                while (!available.Contains(r))
                {
                    r = rand.Next(str.Length);
                }
                available.Remove(r);
                s[x] = str[r];
            }
            return new string(s);
        }

        public static List<String> GetSeperateNames(this int i)
        {
            int[] ints;
            if (i > 9) {
                string s = i.ToString();
                char[] chars = s.ToCharArray();
                ints = new int[chars.Length];
                for (int _i = 0; _i < chars.Length; _i++)
                {
                    char ch = chars[i];
                    ints[i] = int.Parse(ch.ToString());
                }
            } else {
                ints = new int[] { i };
            }
            List<String> ls = new List<string>();
            foreach(int integer in ints)
            {
                ls.Add(integer.GetName());
            }
            return ls;
        }
        public static string GetName(this int i)
        {
            switch(i)
            {
                case 0:
                    return "Zero";
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                case 8:
                    return "Eight";
                case 9:
                    return "Nine";
                default:
                    return "Null";
            }
        }
    }
}
