using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Database : MonoBehaviour
{
    private List<Dictionary<string, int>> data;
    // Dict는 한 년도의 단어들의 딕셔너리
    // Dict에서 string으로 단어를 부르면 List<Word>를 이용해 분석

    private void Start()
    {
        data = new List<Dictionary<string, int>>();
        GetAllResources();
    }

    private void GetAllResources()
    {
        for (int i = 2013; i <= 2019; i++)
        {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(i.ToString());
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (TextAsset asset in textAssets)
            {
                Parser.AssetParser(asset, ref dict);
            }

            data.Add(dict);
        }
    }

    public Dictionary<string, int> GetTopWords(int startYear, int endYear, int top, List<string> blackList)
    {
        Dictionary<string, int> ret = new Dictionary<string, int>();
        Dictionary<string, int> sum = new Dictionary<string, int>();

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
}
