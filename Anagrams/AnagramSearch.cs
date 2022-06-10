using System.Text;

namespace Interview.Algorithms
{
    public class AnagramSearch : IImplementation
    {
        // Two words are anagrams if one of them has exactly same characters as that of the another word.
        // Example : Anagram & Nagaram are anagrams(case-insensitive).
        // Find all anagrams in word and return them.
        // Example: "abcc" "dfgcbaccffffccabc" -> ["cbacc", "bacc", "ccab", "cabc"]
        public void Implementation()
        {
            string word = "ABCCafgcbaccffffccabc";
            string pattern = "abcc";
            var anagramsList = GetAnagrams(pattern, word);
            PrintResults(pattern, word, anagramsList);
        }

        private List<string> GetAnagrams(string smallWord, string bigWord)
        {
            if (string.IsNullOrEmpty(smallWord) || string.IsNullOrEmpty(bigWord) || smallWord.Length > bigWord.Length)
            {
                return new List<string>();
            }

            List<string> output = new();
            Queue<char> queue = new();
            Dictionary<char, int> patternMap = smallWord.ToLower().GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            Dictionary<char, int> currentMap = new();
            string lowerBigWord = bigWord.ToLower();

            for (int i = 0; i < bigWord.Length; i++)
            {
                var character = lowerBigWord[i];
                queue.Enqueue(lowerBigWord[i]);
                currentMap[character] = currentMap.ContainsKey(character) ? currentMap[character] + 1 : 1;

                if (queue.Count > smallWord.Length)
                {
                    currentMap[queue.Dequeue()]--;
                }

                if (queue.Count == smallWord.Length)
                {
                    if (patternMap.Keys.All(x => currentMap.ContainsKey(x) && currentMap[x] == patternMap[x]))
                    {
                        StringBuilder sb = new(smallWord.Length);
                        foreach (char item in queue)
                        {
                            sb.Append(item);
                        }
                        output.Add(sb.ToString());
                        sb.Clear();
                    }
                }
            }

            return output;
        }

        private void PrintResults(string pattern, string word, List<string> anagramsList)
        {
            Console.WriteLine($"Word \"{word}\"");
            Console.WriteLine($"Search for anagrams by \"{pattern}\" pattern");
            int i = 0;
            foreach (string anagram in anagramsList)
            {
                Console.WriteLine($"{++i} : \"{anagram}\" ");
            }
        }
    }
}
