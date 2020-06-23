using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ScatterData
{
    public int maxOld;
    public int maxNow;
    public Dictionary<string, WordFreq> data;

    public ScatterData(int maxOld, int maxNow, Dictionary<string, WordFreq> data)
    {
        this.maxOld = maxOld;
        this.maxNow = maxNow;
        this.data = data;
    }
}

public class ScatterText : MonoBehaviour
{
    [SerializeField] private Transform plotTransform;

    private ScatterData data;
    private float width;
    private float height;

    private List<GameObject> points = new List<GameObject>();

    [Header("Prefab")]
    [SerializeField] private GameObject pointPrefab;

    public ScatterData ScatterData { set { data = value; } }

    public void Visualize()
    {
        DestroyAll();

        width = plotTransform.GetComponent<RectTransform>().rect.width;
        height = plotTransform.GetComponent<RectTransform>().rect.height;

        int maxOld = data.maxOld;
        int maxNow = data.maxNow;

        Dictionary<string, WordFreq> target = data.data;

        foreach(KeyValuePair<string, WordFreq> pair in target)
        {
            GameObject point = Instantiate(pointPrefab, plotTransform);
            points.Add(point);
            RectTransform pointRect = point.GetComponent<RectTransform>();
            Text text = point.GetComponentInChildren<Text>();
            float ratioX = (float)pair.Value.nowCount / maxNow;
            float ratioY = (float)pair.Value.oldCount / maxOld;
            float posY = ratioY * height;
            float posX;

            if (ratioY == 0)
            {
                posX = width + Random.Range(-50f, 50f);
            }
            else
            {
                float r = ratioX / ratioY;
                if (r <= 1) posX = 0.5f * r * (width - 100f);
                else
                {
                    posX = (0.5f + (r - 1) / 3f) * (width - 100f);
                    if (posX > width) posX = width - 50f;
                }
            }
            
            pointRect.anchoredPosition = new Vector2(posX, 0);
            text.text = "";

            Sequence sequence = DOTween.Sequence();

            sequence.Append(pointRect.DOAnchorPosY(posY, 0.5f))
                .AppendCallback(() => text.text = pair.Key);

            if (posX < 800f / 3)
            {
                Image image = point.GetComponent<Image>();
                Color color = new Color(0.39f, 0.45f, 0.8f); 
                sequence.Append(image.DOColor(color, 0.5f))
                    .Insert(0.5f, text.DOColor(color, 0.5f));
            }
            else if (posX > 1600f / 3)
            {
                Image image = point.GetComponent<Image>();
                Color color = new Color(0.8f, 0.35f, 0.47f);
                sequence.Append(image.DOColor(color, 0.5f))
                    .Insert(0.5f, text.DOColor(color, 0.5f));
            }
        }
    }

    private void DestroyAll()
    {
        foreach (GameObject gameObject in points) Destroy(gameObject);
    }
}
