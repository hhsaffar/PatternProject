using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PatternProject.Features
{
    public class TFIDF
    {
        public static string[] GetTerms(string[] documents)
        {
            HashSet<string> allTerms = new HashSet<string>();

            foreach (var doc in documents)
            {
                foreach (var term in doc.Split())
                {
                    allTerms.Add(term);
                }
            }

            return allTerms.OrderBy(x => x).ToArray();
        }

        public static double[][] GetNormalizedTFIDFVectors(string[] documents)
        {

            Dictionary<string, int> wordsInDocuments = new Dictionary<string, int>();

            Dictionary<string, int>[] TF = new Dictionary<string,int>[documents.Length];

            for (int i = 0; i < documents.Length; i++)
            {
                TF[i] = new Dictionary<string,int>();

                foreach (var term in documents[i].Split())
                {
                    if (TF[i].ContainsKey(term))
                        TF[i][term]++;
                    else
                        TF[i][term] = 1;
                }

                foreach (var term in TF[i].Keys)
                {
                    if (wordsInDocuments.ContainsKey(term))
                        wordsInDocuments[term]++;
                    else
                        wordsInDocuments[term] = 1;
                }
            }


            string[] terms = wordsInDocuments.Keys.OrderBy(x => x).ToArray();

            double[][] vectors = new double[documents.Length][];

            for (int i = 0; i < documents.Length; i++)
            {
                vectors[i] = new double[terms.Length];
                for (int j = 0; j < terms.Length; j++)
                {
                    if (TF[i].ContainsKey(terms[j]))
                        vectors[i][j] = (Convert.ToDouble(TF[i][terms[j]]) / documents[i].Length) * Math.Log(((double)documents.Length) / (1 + wordsInDocuments[terms[j]]));
                    else
                        vectors[i][j] = 0;
                }

                double norm = norm2(vectors[i]);

                for (int j = 0; j < terms.Length; j++)
                    vectors[i][j] /= norm;

            }

            
            return vectors;
        }

        private static double norm2(double[] p)
        {
            double s = 0;
            for (int i = 0; i < p.Length; i++)
            {
                s += p[i] * p[i];
            }
            return Math.Sqrt(s);
        }
    }
}
