using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace lab12
{
    public sealed class TextHandler
    {
        public static Dictionary<string, int> CountWords(string filePath)
        {
            var wordsInfo = new Dictionary<string, int>();
            // File.exists() + throw exception?
            try
            {

                var words = File
                    .ReadAllText(filePath)
                    .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    var count = 0;
                    wordsInfo.TryGetValue(word, out count);
                    wordsInfo[word] = count + 1;
                }
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.Message);
            }
            catch (UnauthorizedAccessException uae)
            {
                Console.WriteLine(uae.Message);
            }

            //var sortedDict = from word in wordsInfo orderby word.Value ascending select word;
            //var sortedDict = wordsInfo.OrderBy(x => x.Value);

            return wordsInfo;
        }


        public static int SearchWord(Dictionary<string, int> wordsInfo, string word)
        {
            bool ok = wordsInfo.TryGetValue(word, out var counter);

            return ok ? counter : 0;
        }


        public static List<string> GetMostCommonWord(Dictionary<string, int> wordsInfo)
        {
            var res = new List<string>();
            var maxValue = wordsInfo.Values.Max();

            foreach (var word in wordsInfo)
            {
                if (word.Value == maxValue)
                {
                    res.Add(word.Key);
                }
            }

            return res;
        }
    }
}
