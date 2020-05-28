using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Word
{
    string word;
    string sentence;
}

public class Database : MonoBehaviour
{
    private List<Dictionary<string, List<Word>>> data;
    // Dict는 한 년도의 단어들의 딕셔너리
    // Dict에서 string으로 단어를 부르면 List<Word>를 이용해 분석

    private void Start()
    {
        GetAllResources();
    }

    private void GetAllResources()
    {
        for (int i = 2013; i <= 2019; i++)
        {
            TextAsset[] textAssets = Resources.LoadAll<TextAsset>(i.ToString());
            Dictionary<string, List<Word>> dict = new Dictionary<string, List<Word>>();

            foreach (TextAsset asset in textAssets)
            {
                Parser.AssetParser(asset, ref dict);
            }
        }
    }
}
