using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Parser
{
    public static Dictionary<string, int> AssetParser(TextAsset asset, ref Dictionary<string, int> dict)
    {
        StringReader sr = new StringReader(asset.text);
        string source = sr.ReadLine();
        source = sr.ReadLine();

        while (source != null)
        {
            string word = Word(source);
            int cnt;

            if (dict.TryGetValue(word, out cnt))
                dict[word] += 1;
            else dict.Add(word, 1);

            source = sr.ReadLine();
        }

        return dict;
    }

    private static string Word(string source)
    {
        string[] values = source.Split(',');
        string netWord = values[1].Trim('"');

        return netWord;
    }

    public static List<Word> WordParser(TextAsset asset)
    {
        List<Word> ret = new List<Word>();

        StringReader sr = new StringReader(asset.text);
        string source = sr.ReadLine();
        source = sr.ReadLine();

        while (source != null)
        {
            string[] values = source.Split('\t');
            Word word = new Word(values);
            ret.Add(word);

            source = sr.ReadLine();
        }

        return ret;
    }

    public static ScatterData ScatterParser(TextAsset asset)
    {
        Dictionary<string, WordFreq> data = new Dictionary<string, WordFreq>();
        int maxOld = 0, maxNow = 0;

        StringReader sr = new StringReader(asset.text);
        sr.ReadLine();
        string source = sr.ReadLine();

        while (source != null)
        {
            string[] values = source.Split(',');
            string word = values[0];
            int oldCnt = 0, nowCnt = 0;

            for (int i = 1; i <= 20; i++) oldCnt += int.Parse(values[i]);
            for (int i = 21; i <= 30; i++) nowCnt += int.Parse(values[i]);

            maxOld = maxOld > oldCnt ? maxOld : oldCnt;
            maxNow = maxNow > nowCnt ? maxNow : nowCnt;

            data.Add(word, new WordFreq(oldCnt, nowCnt));

            source = sr.ReadLine();
        }

        return new ScatterData(maxOld, maxNow, data);
    }
}