using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour
{
    [Header("Bar Chart")]
    [SerializeField] private Transform barChartPanel;
    [SerializeField] private GameObject barPrefab;

    private Dictionary<string, int> dict = new Dictionary<string, int>();
    private List<BarController> bars;

    private TextAsset[] assets2019;
    private TextAsset[] assets2018;
    private TextAsset[] assets2017;

    private bool is2017On = false;
    private bool is2018On = false;
    private bool is2019On = false;

    private void Start()
    {
        bars = new List<BarController>();
        CreateBars();

        assets2017 = Resources.LoadAll<TextAsset>("2017");
        assets2018 = Resources.LoadAll<TextAsset>("2018");
        assets2019 = Resources.LoadAll<TextAsset>("2019");

        ShowBarChart();
    }

    private void CreateBars()
    {
        for(int i = 0; i < 14; i++)
        {
            GameObject bar = Instantiate(barPrefab, barChartPanel);
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * 100f);
            bars.Add(bar.GetComponent<BarController>());
        }
    }

    private void ShowBarChart()
    {
        if(!is2017On && !is2018On && !is2019On)
            foreach (BarController controller in bars) controller.Initialize();

        var orderedDict = dict.OrderByDescending(x => x.Value);

        int maxCount = 0;
        int cnt = 0;

        foreach (var pair in orderedDict)
        {
            if (cnt == 0) maxCount = pair.Value;
            if (cnt >= 14) break;

            bars[cnt].AssignValues(pair.Key, pair.Value, maxCount);

            cnt += 1;
        }
    }

    public void ChangeData(int targetYear, bool isOn)
    {
        if (targetYear == 2017) is2017On = isOn;
        if (targetYear == 2018) is2018On = isOn;
        if (targetYear == 2019) is2019On = isOn;


        dict.Clear();

        if (is2017On)
        {
            foreach (TextAsset asset in assets2017)
            {
                dict = Parser.AssetParser(asset, ref dict);
            }
        }

        if (is2018On)
        {
            foreach (TextAsset asset in assets2018)
            {
                dict = Parser.AssetParser(asset, ref dict);
            }
        }

        if (is2019On)
        {
            foreach (TextAsset asset in assets2019)
            {
                dict = Parser.AssetParser(asset, ref dict);
            }
        }

        ShowBarChart();
    }
}
