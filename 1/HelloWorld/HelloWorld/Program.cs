using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;


class HelloWorld
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

    public static void PrintDictionary(Dictionary<string, string> dict)
    {
        foreach(var item in dict)
        {
            Console.WriteLine(item.Key + ": " + item.Value);
        }
    }

    public static Dictionary<string, int> GetFrequencyNgramsDictionary(List<List<string>> text, out List<string> allStartWordsOfBigram, int N)
    {
        var frequencyBigram = new Dictionary<string, int>();
        allStartWordsOfBigram = new List<string>();

        for (var i = 0; i < text.Count; i++)
        {
            for (var j = N - 1; j < text[i].Count; j++)
            {
                var NgramWords = text[i].GetRange(j - N + 1, N);
                allStartWordsOfBigram.Add(String.Join(" ", NgramWords.GetRange(0, N - 1)));

                var bigram = string.Join(" ", NgramWords);
                if (!frequencyBigram.ContainsKey(bigram))
                    frequencyBigram[bigram] = 1;
                else
                    frequencyBigram[bigram]++;
            }
        }

        return frequencyBigram;
    }

    public static List<String> FindAllMatchedNgramsForWordWithMaxFrequency(Dictionary<string, int> frequencyBigram, string word)
    {
        var maxFrequency = -1;
        var findingMaxFrequencyDict = new Dictionary<int, List<String>>();
        foreach (var item in frequencyBigram)
        {
            if (item.Key.StartsWith(word))
            {
                if (item.Value >= maxFrequency)
                {
                    maxFrequency = item.Value;
                    if (!findingMaxFrequencyDict.ContainsKey(maxFrequency))
                        findingMaxFrequencyDict[maxFrequency] = new List<String>() { item.Key };
                    else
                        findingMaxFrequencyDict[maxFrequency].Add(item.Key);
                }
                
            }
        }
        return findingMaxFrequencyDict[maxFrequency];
    }

    public static string FindMostAppropriateSecondWordOfBigram(List<String> allMatchedBigramsForWord)
    {
        var mostAppropriateSecondWordOfBigram = allMatchedBigramsForWord[0].Split(' ')[1];
        for (var j = 0; j < allMatchedBigramsForWord.Count; j++)
        {
            var splittedBigram = allMatchedBigramsForWord[j].Split(' ');
            var comparingWithAppropriateValue = string.CompareOrdinal(splittedBigram[1], mostAppropriateSecondWordOfBigram);
            if (comparingWithAppropriateValue < 0)
                mostAppropriateSecondWordOfBigram = splittedBigram[1];
        }
        return mostAppropriateSecondWordOfBigram;
    }

    public static string FindMostAppropriateLastWordOfNgram(List<String> allMatchedBigramsForWord, int N)
    {
        var mostAppropriateSecondWordOfBigram = allMatchedBigramsForWord[0].Split(' ')[N - 1];
        return mostAppropriateSecondWordOfBigram;
    }


    public static Dictionary<string, string> FindNgramResult(List<string> allStartWordsOfNgram, Dictionary<string, int> frequencyNgram, int N)
    {
        var NgramResult = new Dictionary<string, string>();

        Stopwatch stopwatch = new Stopwatch();

        Console.WriteLine(allStartWordsOfNgram.Count);
        for (var i = 0; i < allStartWordsOfNgram.Count; i++)
        {
            
            var word = allStartWordsOfNgram[i];

            stopwatch.Start();
            var allMatchedBigramsForWord = FindAllMatchedNgramsForWordWithMaxFrequency(frequencyNgram, word);
            stopwatch.Stop();
            Console.WriteLine("FindAllMatchedNgramsForWord: " + stopwatch.Elapsed);

            var mostAppropriateSecondWordOfBigram = (N == 2)
                ? FindMostAppropriateSecondWordOfBigram(allMatchedBigramsForWord)
                : FindMostAppropriateLastWordOfNgram(allMatchedBigramsForWord, N);

            NgramResult[word] = mostAppropriateSecondWordOfBigram;
            
        }
        
        return NgramResult;
    }



    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();

        List<string> allStartWordsOfBigram;
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        var frequencyBigram = GetFrequencyNgramsDictionary(text, out allStartWordsOfBigram, 2);
        stopwatch.Stop();
        Console.WriteLine("GetDictFrequency: " + stopwatch.Elapsed);
        allStartWordsOfBigram.Distinct().ToList(); // delete all repeats

        stopwatch.Start();
        var bigramResult = FindNgramResult(allStartWordsOfBigram, frequencyBigram, 2);
        stopwatch.Stop();
        Console.WriteLine("FindNgramResult: " + stopwatch.Elapsed);
        bigramResult.ToList().ForEach(x => result.Add(x.Key, x.Value)); // join dictionaries


        List<string> allStartWordsOfTrigram;

        var frequencyTrigram = GetFrequencyNgramsDictionary(text, out allStartWordsOfTrigram, 3);
        allStartWordsOfTrigram.Distinct().ToList(); // delete all repeats

        var trigramResult = FindNgramResult(allStartWordsOfTrigram, frequencyTrigram, 3);
        trigramResult.ToList().ForEach(x => result.Add(x.Key, x.Value)); // join dictionaries

        // sorry for awful code, will fix later

        return result;
    }











    public static Dictionary<string, Dictionary<string, int>> GetFrequencyNgramsDictionary2(List<List<string>> text, int N)
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

    public static Dictionary<string, string> FindNgramResult2(Dictionary<string, Dictionary<string, int>> frequencyNgram, int N)
    {
        var NgramResult = new Dictionary<string, string>();

        foreach(var startWord in frequencyNgram)
        {
            var allEndWordAndTheirValues = startWord.Value;
            var endWord = FindMostAppropriateLastWordOfNgram2(allEndWordAndTheirValues, N);
            NgramResult[startWord.Key] = endWord;
        }

        return NgramResult;
    }

    public static string FindMostAppropriateLastWordOfNgram2(Dictionary<string, int> allMatchedEndWordsWithValues, int N)
    {
        var maxFrequency = -1;
        var lastElementWithMaxFrequency = "";
        foreach (var item in allMatchedEndWordsWithValues)
        {
            if (item.Value > maxFrequency)
            {
                maxFrequency = item.Value;
                lastElementWithMaxFrequency = item.Key;
            }
        }

        /*
        if (N > 2)
            return lastElementWithMaxFrequency;
        else
        {
        */
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
        //}
    }

    public static Dictionary<string, string> GetMostFrequentNextWords2(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();

        List<string> allStartWordsOfBigram;

        var frequencyBigram = GetFrequencyNgramsDictionary2(text, 2);
        var bigramResult = FindNgramResult2(frequencyBigram, 2);
        bigramResult.ToList().ForEach(x => result.Add(x.Key, x.Value)); // join dictionaries


        List<string> allStartWordsOfTrigram;
        var frequencyTrigram = GetFrequencyNgramsDictionary2(text, 3);
        var trigramResult = FindNgramResult2(frequencyTrigram, 3);
        //trigramResult.ToList().ForEach(x => result.Add(x.Key, x.Value)); // join dictionaries
        trigramResult.ToList().ForEach(x => result[x.Key] = x.Value); // join dictionaries
        // sorry for awful code, will fix later

        return result;
    }


















    static void Main()
    {
        
        Stopwatch stopwatch = new Stopwatch();
        var text = File.ReadAllText("HarryPotterText.txt");
        var textList = ParseSentences(text);
        stopwatch.Start();
        var grams = GetMostFrequentNextWords2(textList);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        //PrintDictionary(grams);
        
        /*
        var s1 = new List<string> { "a", "e", "c", "d", "a" };
        var y = new List<List<string>>();
        y.Add(s1);
        var smt = GetMostFrequentNextWords(y);
        PrintDictionary(smt);
        Console.WriteLine("Yes");
        */

    }
}