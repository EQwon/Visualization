using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LineGraph : MonoBehaviour
{
    [SerializeField] private FrequencyGraphPanel panel;
    [SerializeField] private Transform content;
    [SerializeField] private Dropdown filter;

    [Header("Prefab")]
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject linePrefab;

    private List<int> xList;
    private List<int> yList;
    private List<GameObject> graphObject = new List<GameObject>();

    private float width;
    private float height;

    public void AssignData()
    {
        DestroyAll();

        xList = new List<int>();
        yList = new List<int>();

        for (int i = 1990; i <= 2020; i++)
        {
            xList.Add(i);
            yList.Add(0);
        }

        yList = UIManager.instance.Database.GetWordCount(panel.Data.ElementAt(filter.value).Key);

        width = content.GetComponent<RectTransform>().rect.width;
        height = content.GetComponent<RectTransform>().rect.height;

        Visualize();
    }

    private void Visualize()
    {
        float deltaX = width / xList.Count;
        float deltaY = height / yList.Max();

        for (int i = 0; i < xList.Count; i++)
        {
            Vector2 pointPos = new Vector2(deltaX * i, deltaY * yList[i]);
            RectTransform point = Instantiate(pointPrefab, content).GetComponent<RectTransform>();
            graphObject.Add(point.gameObject);
            point.anchoredPosition = pointPos;

            if (i != 0)
            {
                RectTransform line = Instantiate(linePrefab, content).GetComponent<RectTransform>();
                graphObject.Add(line.gameObject);

                Vector2 startPoint = new Vector2(deltaX * (i - 1), deltaY * yList[i - 1]);
                Vector2 endPoint = new Vector2(deltaX * i, deltaY * yList[i]);
                Vector2 linePos = (startPoint + endPoint) / 2;
                float lineWidth = (endPoint - startPoint).magnitude;
                float angle = Mathf.Atan2((endPoint - startPoint).y, (endPoint - startPoint).x)*Mathf.Rad2Deg;

                line.anchoredPosition = linePos;
                line.sizeDelta = new Vector2(lineWidth, 3f);
                line.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    private void DestroyAll()
    {
        foreach (GameObject gameObject in graphObject)
            Destroy(gameObject);

        graphObject = new List<GameObject>();
    }

    public void AssignFilterMenu()
    {
        filter.ClearOptions();

        List<string> optionList = new List<string>();
        foreach (KeyValuePair<string, int> pair in panel.Data)
        {
            optionList.Add("'" + pair.Key + "'의 연도별 사용률 변화");
        }

        filter.AddOptions(optionList);
        filter.template.sizeDelta = new Vector2(filter.template.sizeDelta.x, 100f * optionList.Count);
    }
}
