using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, Dictionary<string, int>> GetFrequencyNgramsDictionary(List<List<string>> text, int N)
        {
            var frequencyBigram = new Dictionary<string, Dictionary<string, int>>();

            for (var i = 0; i < text.Count; i++)
            {
                for (var j = N - 1; j < text[i].Count; j++)
                {
                    var NgramWords = text[i].GetRange(j - N + 1, N);
                    var startWords = String.Join(" ", NgramWords.GetRange(0, N - 1));
                    var endWord = NgramWords[NgramWords.Count - 1];
                    if (!frequencyBigram.ContainsKey(startWords))
                        frequencyBigram[startWords] = new Dictionary<string, int>();
                    if (!frequencyBigram[startWords].ContainsKey(endWord))
                        frequencyBigram[startWords][endWord] = 0;

                    frequencyBigram[startWords][endWord] += 1;
                }
            }

            return frequencyBigram;
        }

        public static Dictionary<string, string> FindNgramResult(Dictionary<string, Dictionary<string, int>> frequencyNgram, int N)
        {
            var NgramResult = new Dictionary<string, string>();

            foreach (var startWord in frequencyNgram)
            {
                var allEndWordAndTheirValues = startWord.Value;
                var endWord = FindMostAppropriateLastWordOfNgram(allEndWordAndTheirValues, N);
                NgramResult[startWord.Key] = endWord;
            }

            return NgramResult;
        }

        public static string FindMostAppropriateLastWordOfNgram(Dictionary<string, int> allMatchedEndWordsWithValues, int N)
        {
            var maxFrequency = -1;
            foreach (var item in allMatchedEndWordsWithValues)
                if (item.Value > maxFrequency)
                    maxFrequency = item.Value;
            
            var listWordsWithMaxFrequency = new List<string>();
            foreach (var item in allMatchedEndWordsWithValues)
                if (item.Value == maxFrequency)
                    listWordsWithMaxFrequency.Add(item.Key);

            var bestWord = listWordsWithMaxFrequency[0];
            for (var i = 1; i < listWordsWithMaxFrequency.Count; i++)
            {
                var comparingWithAppropriateValue = string.CompareOrdinal(listWordsWithMaxFrequency[i], bestWord);
                if (comparingWithAppropriateValue < 0)
                    bestWord = listWordsWithMaxFrequency[i];
            }
            return bestWord;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            List<string> allStartWordsOfBigram;

            var frequencyBigram = GetFrequencyNgramsDictionary(text, 2);
            var bigramResult = FindNgramResult(frequencyBigram, 2);
            bigramResult.ToList().ForEach(x => result.Add(x.Key, x.Value)); // join dictionaries


            List<string> allStartWordsOfTrigram;
            var frequencyTrigram = GetFrequencyNgramsDictionary(text, 3);
            var trigramResult = FindNgramResult(frequencyTrigram, 3);
            //trigramResult.ToList().ForEach(x => result.Add(x.Key, x.Value)); // join dictionaries
            trigramResult.ToList().ForEach(x => result[x.Key] = x.Value); // join dictionaries
            // sorry for awful code, will fix later

            return result;
        }
    }
}
