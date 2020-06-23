using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Word
{
    public string word;
    public int year;
    public string novel;
    public string writer;
    public string sentence;

    public Word(string[] values)
    {
        this.word = values[0];
        this.year = int.Parse(values[1]);
        this.novel = values[2];
        this.writer = values[3];
        this.sentence = values[4];
    }
}

public struct WordFreq
{
    public int oldCount;
    public int nowCount;

    public WordFreq(int old, int now)
    {
        oldCount = old;
        nowCount = now;
    }

    public void IncreaseOld(int value)
    {
        oldCount += value;
    }

    public void IncreaseNow(int value)
    {
        nowCount += value;
    }
}

public class Database : MonoBehaviour
{
    private List<Word> words;
    Dictionary<string, WordFreq>[,] scatter;
    // Dict는 한 년도의 단어들의 딕셔너리
    // Dict에서 string으로 단어를 부르면 List<Word>를 이용해 분석

    private int maxOld = 1;
    private int maxNow = 1;

    public Dictionary<string, WordFreq>[,] Scatter { get { return scatter; } }
    public int MaxOld { get { return maxOld; } }
    public int MaxNow { get { return maxNow; } }

    private void Start()
    {
        StartCoroutine(DataRoutine());
    }

    private IEnumerator DataRoutine()
    {
        yield return new WaitUntil(() => words.Count != 0);

        scatter = GetScatterData();
    }

    public Dictionary<string, int> GetTopWords(int startYear, int endYear, int top, List<string> blackList)
    {
        Dictionary<string, int> ret = new Dictionary<string, int>();
        Dictionary<string, int> sum = new Dictionary<string, int>();

        /*
        for (int i = startYear; i <= endYear; i++)
        {
            Dictionary<string, int> targetData = data[i - 2013];

            foreach (KeyValuePair<string, int> pair in targetData)
            {
                int temp;
                if (sum.TryGetValue(pair.Key, out temp))
                    sum[pair.Key] += pair.Value;
                else sum.Add(pair.Key, pair.Value);
            }
        }
        */

        foreach (Word word in words)
        {
            if (startYear > word.year || word.year > endYear) continue;
            
            int temp;
            if (sum.TryGetValue(word.word, out temp))
                sum[word.word] += 1;
            else sum.Add(word.word, 1);
        }

        var orderedDict = sum.OrderByDescending(x => x.Value);
        int cnt = 0;

        foreach (var pair in orderedDict)
        {
            if (cnt >= top) break;
            if (blackList.Contains(pair.Key)) continue;

            ret.Add(pair.Key, pair.Value);

            cnt += 1;
        }

        return ret;
    }

    public List<int> GetWordCount(string targetWord)
    {
        List<int> ret = new List<int>();
        for (int i = 1990; i <= 2020; i++) ret.Add(0);

        foreach (Word word in words)
        {
            if (word.word != targetWord) continue;
            ret[word.year - 1990] += 1;
        }

        return ret;
    }

    public Dictionary<string, WordFreq>[,] GetScatterData()
    {
        Dictionary<string, WordFreq> dict = new Dictionary<string, WordFreq>();
        int totalOld = 0;
        int totalNow = 0;

        foreach (Word word in words)
        {
            if (dict.ContainsKey(word.word))
            {
                if (word.year < 2010)
                {
                    dict[word.word] = new WordFreq(dict[word.word].oldCount + 1, dict[word.word].nowCount);
                    if (maxOld < dict[word.word].oldCount) maxOld = dict[word.word].oldCount;
                }
                else
                {
                    dict[word.word] = new WordFreq(dict[word.word].oldCount, dict[word.word].nowCount + 1);
                    if (maxNow < dict[word.word].nowCount) maxNow = dict[word.word].nowCount;
                }
            }
            else
            {
                if (word.year < 2010) dict.Add(word.word, new WordFreq(1, 0));
                else dict.Add(word.word, new WordFreq(0, 1));
            }

            if (word.year < 2010) totalOld += 1;
            else totalNow += 1;
        }

        Dictionary<string, WordFreq>[,] scatter = new Dictionary<string, WordFreq>[3, 3];
        
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                scatter[i, j] = new Dictionary<string, WordFreq>();
            }
        }

        foreach (KeyValuePair<string, WordFreq> pair in dict)
        {
            float oldFreq = (float)pair.Value.oldCount / maxOld;
            float nowFreq = (float)pair.Value.nowCount / maxNow;

            int nowX;
            int oldY;

            if (oldFreq < 1f / 3) oldY = 0;
            else if (oldFreq < 2f / 3) oldY = 1;
            else oldY = 2;

            if (nowFreq < 1f / 3) nowX = 0;
            else if (nowFreq < 2f / 3) nowX = 1;
            else nowX = 2;

            scatter[nowX, oldY].Add(pair.Key, pair.Value);
        }

        return scatter;
    }
}
