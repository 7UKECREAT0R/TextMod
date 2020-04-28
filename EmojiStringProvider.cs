using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextMod
{
    class EmojiStringProvider
    {
        string githubCorpus = "https://raw.githubusercontent.com/ntratcliff/emojipasta.club/master/corpus.txt";
        string corpus;
        Dictionary<string, string> provider;
        public EmojiStringProvider()
        {
            try
            {
                //MessageBox.Show("This may take up to a minute on slower pcs. Press OK to start the creation.", "Creating EmojiStringProvider...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (var wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    corpus = wc.DownloadString(githubCorpus);
                }
                provider = SynthesizeCorpus(corpus);
            } catch(Exception e)
            {
                MessageBox.Show("Ran out of memory. Exception: " + e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Emojifies a string.
        /// </summary>
        /// <param name="str">The string that will be emojified.</param>
        /// <returns>The emojified string.</returns>
        public string Emojify(string str)
        {
            string[] words = str.Split(' ');
            StringBuilder sb = new StringBuilder();
            int i = -1;
            foreach(string word in words)
            {
                i++;
                string s = word.ToLower();
                if(provider.ContainsKey(s))
                {
                    string emoji;
                    provider.TryGetValue(s, out emoji);
                    sb.Append(" " + word + " " + emoji);
                } else
                {
                    sb.Append(" " + word);
                }
            }
            try
            {
                sb = sb.Remove(0, 1);
            } catch (Exception) { };
            return sb.ToString();
        }
        private Dictionary<string, string> SynthesizeCorpus(string s)
        {
            // Stopwatch that baby so I can get an idea on how long this is going to take.
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Get some valid chars to use.
            char[] validchars = "!1@2#3$4%5^6&7*8(9)0_-+=|\\\"':;?/>.<,qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM ".ToCharArray();

            // Begin processing
            Dictionary<string, string> target = new Dictionary<string, string>();
            string[] words = s.Split(' ');
            
            // Use a loop that allows fetching future elements.
            for(int i = 0; i < words.Length; i++)
            {
                // Get the word.
                string word = words[i];

                // Is the word a word? (memory hog)
                bool status = true;
                foreach (char c in word.ToCharArray())
                { if(!validchars.Contains(c))
                    { status = false; break; } }
                if(!status) {
                    if (word.Any(c => validchars.Contains(c)))
                    {
                        List<char> _actualWord = new List<char>();
                        string rest = "";
                        for (int x = 0; x < word.Length; x++)
                        {
                            if (validchars.Contains(word[x]))
                            {
                                _actualWord.Add(word[x]);
                                continue;
                            }
                            else
                            {
                                rest = word.Substring(x);
                                break;
                            }
                        }
                        if (rest.Equals(""))
                        {
                            continue;
                        }
                        if (rest.Any(c => validchars.Contains(c)))
                        {
                            try
                            {
                                rest = rest.Substring(0, rest.IndexOfAny(validchars));
                            }
                            catch (Exception) { }
                        }
                        string actualWord = new string(_actualWord.ToArray());
                        if (target.ContainsKey(actualWord.ToLower()))
                        {
                            continue;
                        }
                        target.Add(actualWord.ToLower(), rest);
                        continue;
                    }
                }

                // Prone to have an indexoutofrange exception.
                try
                {
                    // Now, check if the next word is an emoji?
                    string nextword = words[i + 1];
                    if (nextword.ToCharArray().Any(c => !validchars.Contains(c))) {
                        // Get rid of trailing characters.
                        nextword = new string(nextword.ToCharArray().Where(p =>
                            !validchars.Contains(p)).ToArray());

                        // Yes, it is.
                        // Check if this word is already taken.
                        if (target.ContainsKey(word.ToLower())) { continue; }

                        // Add the word and emoji, then continue.
                        target.Add(word.ToLower(), nextword);
                        continue;
                    }
                } catch(IndexOutOfRangeException) {}
            }
            sw.Stop();
            //string time = sw.Elapsed.TotalSeconds + " seconds";
            /*MessageBox.Show("Elapsed time: " + time + "\n" +
                "Total words processed: " + words.Length + "\n" +
                "Total emojis added: " + target.Count
                , "Finished.", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
            return target;
        }
    }
}
