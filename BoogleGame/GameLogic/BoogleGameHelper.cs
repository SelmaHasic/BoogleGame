using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoogleGame.Models;

namespace BoogleGame.GameLogic
{
    public class BoogleGameHelper
    {

       public  char[,] cubicDice = new char[4, 4] { { 'G', 'I', 'Z', 'P' }, { 'U', 'E', 'K', 'R' },
                                        { 'Q', 'S', 'E', 'S' }, { 'P', 'M', 'N', 'A' } };
        public  int GetPointsOfWord(string word)
        {
            switch (word.Length)
            {
                case 0:
                case 1:
                case 2:
                    return 0;
                case 3:
                case 4:
                    return 1;
                case 5:
                    return 2;
                case 6:
                    return 3;
                case 7:
                    return 5;
                default:
                    return 11;
            }
        }

        public List<int> getNumberOfPoints(List<string> words)
        {
            List<int> points = new List<int>();
            for (int i = 0; i < words.Count; i++)
            {
                points.Add(GetPointsOfWord(words[i]));
            }

            return points;
        }

        public List<int> GetNumberOfPointsWithOutDuplicates(List<string> words, List<string> duplicates)
        {
            List<int> points = new List<int>();
            for (int i = 0; i < words.Count; i++)
            {
                if (duplicates.Contains(words[i]) || !IsWordCorrect(words[i], cubicDice))
                {
                    points.Add(0);
                }
                else
                {
                    points.Add(GetPointsOfWord(words[i]));
                }
            }
            return points;
        }

        public List<string> GetDuplicateWords(List<In> players)
        {
            List<string> allWords = new List<string>();
            foreach (var player in players)
            {
                player.Words = player.Words.ConvertAll(d => d.ToUpper());
                allWords.AddRange(player.Words);
            }

            var duplicates = allWords.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => y.Key)
              .ToList();

            return duplicates;
        }

        public List<Out> GetNumberOfPointsMP(List<In> players)
        {
            List<string> duplicates = GetDuplicateWords(players);
            List<Out> outs = new List<Out>();

            foreach (In player in players)
            {
                Out outResult = new Out();
                List<int> points = GetNumberOfPointsWithOutDuplicates(player.Words, duplicates);
                outResult.Score = points.Sum();
                outResult.UserName = player.UserName;
                outs.Add(outResult);
            }
            return outs;
        }

        public bool IsWordCorrect(string word, char[,] cubicDice)
        {
            word = word.ToUpper();

            for (int i = 0; i < cubicDice.GetLength(0); i++)
            {
                for (int j = 0; j < cubicDice.GetLength(1); j++)
                {
                    var dupeDice = cubicDice;
                    if (dupeDice[i, j] == word[0])
                    {
                        dupeDice[i, j] = '#';
                        bool isWord = IsWordCorrectRecursion(word.Substring(1), dupeDice, i, j);

                        if (isWord)
                            return isWord;
                    }
                }
            }
            return false;
        }

        public bool IsWordCorrectRecursion(string word, char[,] cubicDice, int currentRow, int currentColumn)
        {
            char letter = word[0];
            currentRow = currentRow == 0 ? currentRow + 1 : currentRow;
            currentColumn = currentColumn == 0 ? currentColumn + 1 : currentColumn;

            int maxRow = currentRow == 3 ? currentRow : currentRow + 1;
            int maxColumn = currentColumn == 3 ? currentColumn : currentColumn + 1;

            for (int i = currentRow - 1; i <= maxRow; i++)
            {
                for (int j = currentColumn - 1; j <= maxColumn; j++)
                {
                    if (cubicDice[i, j] == letter)
                    {
                        if (word.Length == 1)
                        {
                            return true;
                        }
                        cubicDice[i, j] = '#';
                        return IsWordCorrectRecursion(word.Substring(1), cubicDice, i, j);
                    }
                }
            }
            return false;
        }
    }
}
