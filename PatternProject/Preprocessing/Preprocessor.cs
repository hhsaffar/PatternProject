using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternProject.Preprocessing
{
    public class Preprocessor
    {
        private static string[] _stopWords;
        public static void SetStopWordsSet(string []stopWords)
        {
            _stopWords = new string[stopWords.Length];
            for (int i = 0; i < stopWords.Length; i++)
            {
                _stopWords[i] = stopWords[i];
            }
        }

        public static string RemoveStopWords(string document)
        {
            return string.Join(" ", document.Split().Where(x => !_stopWords.Contains(x)).ToArray());
        }

        public static string StemTerm(string term)
        {
            var stemmer = new PorterStemmer();
            return stemmer.stemTerm(term);
        }


    }
}
