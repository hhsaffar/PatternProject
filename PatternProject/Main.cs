using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PatternProject.General;
using PatternProject.Preprocessing;
using PatternProject.Features;


namespace PatternProject
{
    public static class MainClass
    {

        static void Main(string[] args)
        {
            //read all data

            string docPath = @"..\..\Data\data_train.txt";
            string labelPath = @"..\..\Data\labels_train.txt";
            string stopWordPath = @"..\..\Data\stop_words.txt";


            DocumentClass[] docs = IO.ReadDocments(docPath, labelPath);
            Preprocessor.SetStopWordsSet(File.ReadAllLines(stopWordPath));

            //var doc1 = string.Join(" ", Preprocessor.RemoveStopWords(docs[0].Document).Split().Select(x=>Preprocessor.StemTerm(x)).ToArray());

            var pdocs = docs.Select(x => string.Join(" ", Preprocessor.RemoveStopWords(x.Document).Split().Select(y => Preprocessor.StemTerm(y)).ToArray())).ToArray();

            //string[] docs2 = {"sun sky bright","sun bright sun" };

            var allTerms = TFIDF.GetTerms(pdocs);
            var vv = TFIDF.GetNormalizedTFIDFVectors(pdocs);

            double[] avg = new double[allTerms.Length];

            for (int j = 0; j < vv[0].Length; j++)
            {
                double s = 0;
                for (int i = 0; i < vv.Length; i++)
                {
                    s += vv[i][j];
                }
                avg[j] = -s / vv.Length;
            }

            Array.Sort(avg, allTerms);

            File.WriteAllLines("aaaa.txt", avg.Select(x=>x.ToString()));

            //var allTerms = TFIDF.GetTerms(docs.Select(x => x.Document).ToArray());

            var c0 = docs.Where(x => x.Label == 0).ToArray();
            var c1 = docs.Where(x => x.Label == 1).ToArray();
            var c2 = docs.Where(x => x.Label == 2).ToArray();
            var c3 = docs.Where(x => x.Label == 3).ToArray();

            var avg0 = c0.Average(x => x.Document.Length);
            var avg1 = c1.Average(x => x.Document.Length);
            var avg2 = c2.Average(x => x.Document.Length);
            var avg3 = c3.Average(x => x.Document.Length);

            Console.WriteLine(docs.Count(x => x.Label == 0) + " " + docs.Count(x => x.Label == 1) + " " + docs.Count(x => x.Label == 2) + " " + docs.Count(x => x.Label == 3));
            Console.WriteLine(c0.Average(x => x.Document.Length) + " " + c1.Average(x => x.Document.Length) +" "+ c2.Average(x => x.Document.Length) +" "+ c3.Average(x => x.Document.Length));
            Console.WriteLine(c0.Average(x => Math.Abs(avg0 - x.Document.Length)) + " " + c1.Average(x => Math.Abs(avg1 - x.Document.Length)) + " " + c2.Average(x => Math.Abs(avg2 - x.Document.Length)) + " " + c3.Average(x => Math.Abs(avg3 - x.Document.Length)));
            Console.WriteLine(c0.Average(x => x.Document.Split(' ').Distinct().Count()) + " " + c1.Average(x => x.Document.Split(' ').Distinct().Count()) + " " + c2.Average(x => x.Document.Split(' ').Distinct().Count()) + " " + c3.Average(x => x.Document.Split(' ').Distinct().Count()));
            Console.WriteLine(c0.Average(x => x.Document.Split(' ').Count(y=>y.StartsWith("19"))) + " " + c1.Average(x => x.Document.Split(' ').Count(y=>y.StartsWith("19")))+ " " + c2.Average(x => x.Document.Split(' ').Count(y=>y.StartsWith("19")))+ " " + c3.Average(x => x.Document.Split(' ').Count(y=>y.StartsWith("19"))));
        }




    }
}
