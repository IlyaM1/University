using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly char[] _separators = new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        public Dictionary<int, string> TextWordsID { get; set; }
        private readonly Dictionary<int, Dictionary<string, List<int>>> wordsAndPosByIndex = new Dictionary<int, Dictionary<string, List<int>>>();
        private readonly Dictionary<string, HashSet<int>> indexesByWord = new Dictionary<string, HashSet<int>>();


        public void Add(int id, string documentText)
        {
            var words = documentText.Split(_separators);
            for (int i = 0; i < words.Length; i++)
                TextWordsID.Add(i, words[i]);
        }

        public List<int> GetIds(string word)
        {
            var result = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                var documentText = File.ReadAllText(String.Format("Texts/{0}.txt", i));
                if (documentText.Split(_separators).Contains(word))
                    result.Add(i);
            }

            return result;
        }

        public List<int> GetPositions(int id, string word)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
