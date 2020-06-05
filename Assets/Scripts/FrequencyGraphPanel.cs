using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FrequencyGraphPanel : MonoBehaviour
{
    [SerializeField] private Text description;
    [SerializeField] private Transform frequencyGraphTransform;
    [SerializeField] private Transform lineGraphTransform;
    [SerializeField] private Dropdown filter;

    [SerializeField] private LineGraph lineGraph;

    [Header("Prefab")]
    [SerializeField] private GameObject barPrefab;

    private int startYear;
    private int endYear;
    private int top = 5;
    private Dictionary<string, int> data;
    public Dictionary<string, int> Data { get { return data; } }
    private List<string> blackList = new List<string>();

    private List<Bar> bars = new List<Bar>();

    public void AssignData()
    {
        startYear = UIManager.instance.StartYear;
        endYear = UIManager.instance.EndYear;
        data = UIManager.instance.Database.GetTopWords(startYear, endYear, top, blackList);
        description.text = "";

        Visualize();
        lineGraph.AssignData();
        lineGraph.AssignFilterMenu();

        float width = GetComponent<RectTransform>().sizeDelta.x;
        float height = 300f + frequencyGraphTransform.GetComponent<RectTransform>().rect.height + lineGraphTransform.GetComponent<RectTransform>().rect.height;
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void Visualize()
    {
        description.DOText(startYear + " ~ " + endYear + "년의 신춘문예 당선작", 1f);
        RectTransform fgRect = frequencyGraphTransform.GetComponent<RectTransform>();
        RectTransform lgRect = lineGraphTransform.GetComponent<RectTransform>();
        fgRect.sizeDelta = new Vector2(fgRect.sizeDelta.x, (top + 1) * 100f);
        lgRect.anchoredPosition = fgRect.anchoredPosition + new Vector2(0, -fgRect.sizeDelta.y);

        int cnt = 0;
        int maxCnt = 9999;
        foreach (KeyValuePair<string, int> pair in data)
        {
            if (cnt == 0) maxCnt = pair.Value;
            Bar bar = Instantiate(barPrefab, frequencyGraphTransform).GetComponent<Bar>();
            bars.Add(bar);
            bar.Panel = this;
            bar.Visualize(cnt, pair.Key, pair.Value, maxCnt);

            cnt += 1;
        }
    }

    public void AddBlackList(string blackWord)
    {
        blackList.Add(blackWord);

        DestroyAll();
        AssignData();
    }

    private void DestroyAll()
    {
        foreach (Bar bar in bars)
            Destroy(bar.gameObject);

        bars = new List<Bar>();
    }

    public void ChangeFilter()
    {
        switch (filter.value)
        {
            case 0:
                top = 5;
                break;
            case 1:
                top = 10;
                break;
            case 2:
                top = 20;
                break;
        }

        DestroyAll();
        AssignData();
        lineGraph.AssignFilterMenu();
    }
}
