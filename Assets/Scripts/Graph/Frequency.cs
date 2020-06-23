using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public struct FrequencyData 
{
    public int oldCnt;
    public int nowCnt;
    public string word;

    public FrequencyData(int oldCnt, int nowCnt, string word)
    {
        this.oldCnt = oldCnt;
        this.nowCnt = nowCnt;
        this.word = word;
    }
}

public class Frequency : MonoBehaviour
{
    [SerializeField] private Transform frequencyGraphTransform;

    [Header("Prefab")]
    [SerializeField] private GameObject barPrefab;

    private List<FrequencyData> frequencyData;
    private List<Bar> bars = new List<Bar>();

    public List<FrequencyData> FrequencyData { set { frequencyData = value; } }

    public void Visualize()
    {
        DestroyAll();

        frequencyGraphTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(900, frequencyData.Count * 70f);
        int maxCnt = frequencyData[0].oldCnt + frequencyData[0].nowCnt;
        for (int i = 0; i < frequencyData.Count; i++)
        {
            FrequencyData data = frequencyData[i];
            Bar bar = Instantiate(barPrefab, frequencyGraphTransform).GetComponent<Bar>();
            bars.Add(bar);
            bar.Visualize(i, data.word, data.oldCnt + data.nowCnt, maxCnt);
        }
    }

    private void DestroyAll()
    {
        foreach (Bar bar in bars)
            Destroy(bar.gameObject);

        bars = new List<Bar>();
    }
}
