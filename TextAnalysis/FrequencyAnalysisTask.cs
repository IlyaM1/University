using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, Dictionary<string, int>> GetFrequencyNgramsDictionary(List<List<string>> text, int n)
        {
            var frequencyBigram = new Dictionary<string, Dictionary<string, int>>();

            for (var i = 0; i < text.Count; i++)
            {
                for (var j = n - 1; j < text[i].Count; j++)
                {
                    var ngramWords = text[i].GetRange(j - n + 1, n);
                    var startWords = String.Join(" ", ngramWords.GetRange(0, n - 1));
                    var endWord = ngramWords[ngramWords.Count - 1];
                    if (!frequencyBigram.ContainsKey(startWords))
                        frequencyBigram[startWords] = new Dictionary<string, int>();
                    if (!frequencyBigram[startWords].ContainsKey(endWord))
                        frequencyBigram[startWords][endWord] = 0;

                    frequencyBigram[startWords][endWord] += 1;
                }
            }

            return frequencyBigram;
        }

        public static Dictionary<string, string> FindNgramResult(Dictionary<string, Dictionary<string, int>> frequencyNgram, int n)
        {
            var ngramResult = new Dictionary<string, string>();

            foreach (var startWord in frequencyNgram)
            {
                var allEndWordAndTheirValues = startWord.Value;
                var endWord = FindMostAppropriateLastWordOfNgram(allEndWordAndTheirValues, n);
                ngramResult[startWord.Key] = endWord;
            }

            return ngramResult;
        }

        public static string FindMostAppropriateLastWordOfNgram(Dictionary<string, int> allMatchedEndWordsWithValues, int n)
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

            return result;
        }
    }
}
