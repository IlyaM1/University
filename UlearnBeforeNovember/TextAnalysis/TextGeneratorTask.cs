using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
    		var words = new List<string>(phraseBeginning.Split(' '));
    		
    		for (var i = 0; i < wordsCount; i++)
    		{
    			string nextWord = "";
    			if (words.Count == 1)	
    				nextWord = GetWordToContunuePhrase(nextWords, words[0]);
    			else
    				nextWord = GetWordToContunuePhrase(nextWords, words[words.Count - 2] + " " + words[words.Count - 1]);
    			
    			if (nextWord == "")
    				return string.Join(" ", words);
    			
    			words.Add(nextWord);
    		}
    		
            return string.Join(" ", words);
        }
    	
    	public static string GetWordToContunuePhrase(Dictionary<string, string> nextWords, string phrase)
    	{
    		var value = "";
    		
    		var isFound = nextWords.TryGetValue(phrase, out value);
    		if (isFound)
    			return value;
    		
    		var words = phrase.Split(' ');
    		if (words.Length == 2)
    		{
    			isFound = nextWords.TryGetValue(words[1], out value);
    			if (isFound)
    				return value;
    		}
    		
    		return "";
    	}
    }
}