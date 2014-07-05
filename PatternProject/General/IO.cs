using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PatternProject.General
{
    public class IO
    {
        public static DocumentClass[] ReadDocments(string docPath, string labelPath)
        {
            string[] allDocuments = File.ReadAllLines(docPath);
            int[] allLables = File.ReadAllLines(labelPath).Select(x => int.Parse(x)).ToArray();

            DocumentClass[] docs = new DocumentClass[allDocuments.Length];

            for (int i = 0; i < allDocuments.Length; i++)
            {
                docs[i] = new DocumentClass();
                docs[i].Document = allDocuments[i];
                docs[i].Label = allLables[i];
            }
            return docs;
        }
    }
}
