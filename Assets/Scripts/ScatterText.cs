using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScatterText : MonoBehaviour
{
    [SerializeField] private Transform plotTransform;

    private int maxOld;
    private int maxNow;
    private int nowX;
    private int oldY;
    private float width = 800f;
    private float height = 800f;

    private List<GameObject> points = new List<GameObject>();

    [Header("Prefab")]
    [SerializeField] private GameObject pointPrefab;

    public void Visualize()
    {
        DestroyAll();

        //nowX = filter.value % 3;
        //oldY = filter.value / 3;

        maxOld = UIManager.instance.Database.MaxOld;
        maxNow = UIManager.instance.Database.MaxNow;
        int listCnt = 0;

        Dictionary<string, WordFreq> target = UIManager.instance.Database.Scatter[nowX, oldY];

        foreach(KeyValuePair<string, WordFreq> pair in target)
        {
            GameObject point = Instantiate(pointPrefab, plotTransform);
            points.Add(point);
            RectTransform pointRect = point.GetComponent<RectTransform>();
            float posX = ((((float)pair.Value.nowCount / maxNow) - nowX * (1f / 3)) / (1f / 3)) * width + Random.Range(-30f, 30f);
            float posY = ((((float)pair.Value.oldCount / maxOld) - oldY * (1f / 3)) / (1f / 3)) * height + Random.Range(-30f, 30f);
            pointRect.anchoredPosition = new Vector2(posX, 0);
            pointRect.DOAnchorPosY(posY, 0.5f);
        }
    }

    private void DestroyAll()
    {
        foreach (GameObject gameObject in points) Destroy(gameObject);
    }
}
