using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        const string SeparatingSentencesSymbols = @"[.!?;:()]";

        static public List<string> ForEach(List<string> arr, Func<string, string> function)
        {
            // Uses function to every element of List and return changed List
            for (var i = 0; i < arr.Count; i++)
            {
                arr[i] = function(arr[i]);
            }
            return arr;
        }

        static public List<List<string>> ForEachTwoDimensional(List<List<string>> arr, Func<string, string> function)
        {
            // Same as ForEach but for Two-Dimensional Lists
            for (var i = 0; i < arr.Count; i++)
                for (var j = 0; j < arr[i].Count; j++)
                    arr[i][j] = function(arr[i][j]);
            return arr;
        }

        static public bool IsLetterOrApostrophe(char c)
        {
            return char.IsLetter(c) || c == '\'';
        }

        static public List<List<string>> SplitOnWordsInAllSentences(List<string> sentences)
        {
            var sentencesList = new List<List<string>>();

            for (var i = 0; i < sentences.Count; i++)
            {
                var wordsList = SplitOnWordsInOneSentence(sentences[i]);

                if (wordsList.Count > 0) // this condition if there are no words in this sentence
                    sentencesList.Add(wordsList);
            }

            return sentencesList;
        }

        public static List<string> SplitOnWordsInOneSentence(string sentence)
        {
            var wordsList = new List<string>();
            var currentWord = new StringBuilder();

            for (var i = 0; i < sentence.Length; i++)
            {
                if (IsLetterOrApostrophe(sentence[i]))
                    currentWord.Append(sentence[i]);

                else if (currentWord.Length > 0)
                {
                    wordsList.Add(currentWord.ToString());
                    currentWord = new StringBuilder();
                }
            }

            if (currentWord.Length > 0) // Add final word in wordsList
            {
                wordsList.Add(currentWord.ToString());
                currentWord = new StringBuilder();
            }

            return wordsList;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();

            var sentences = new List<string>(text.Split(SeparatingSentencesSymbols.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

            sentences = ForEach(sentences, s => s.Trim()); // Deletes start and end spaces in every sentence

            sentencesList = SplitOnWordsInAllSentences(sentences);
            sentencesList = ForEachTwoDimensional(sentencesList, sentence => sentence.ToLower()); // Changes every word to lower case

            return sentencesList;
        }
    }
}
