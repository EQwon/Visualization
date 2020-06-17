using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Analysis : Panel
{
    [Header("Scatter Text")]
    [SerializeField] private ScatterText scatter;
    [SerializeField] private TextAsset asset;

    [Header("UI Elements")]
    [SerializeField] private Text title;
    [SerializeField] private Text desc;
    [SerializeField] private RectTransform barRect;
    [SerializeField] private List<Text> subheadings;
    [SerializeField] private RectTransform resultPanel;
    [SerializeField] private RectTransform graphs;

    private void Start()
    {
        scatter.ScatterData = Parser.ScatterParser(asset);
    }

    public override void OnEnable()
    {
        title.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        title.color = Color.clear;
        desc.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        desc.color = Color.clear;
        resultPanel.anchoredPosition -= new Vector2(0, 1000f);

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;

        sequence.Append(title.rectTransform.DOAnchorPosY(title.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(0, title.DOColor(Color.black, duration))
            .Append(desc.rectTransform.DOAnchorPosY(desc.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(duration, desc.DOColor(Color.black, duration))
            .Append(resultPanel.DOAnchorPosY(resultPanel.anchoredPosition.y + 1000f, duration))
            .AppendCallback(() => scatter.Visualize());
    }

    public override void OnDisable()
    {
    }

    public void Focus(int i)
    {
        float barPosX = 310f * (i - 1);
        float graphPosX = -1200f * i;
        foreach (Text text in subheadings) text.color = new Color(0, 0, 0, 0.45f);
        Text subheading = subheadings[i];

        Sequence sequence = DOTween.Sequence();

        sequence.Append(barRect.DOAnchorPosX(barPosX, 0.3f))
            .Insert(0, subheading.DOColor(Color.black, 0.3f))
            .Insert(0, graphs.DOAnchorPosX(graphPosX, 0.3f));

        if (i == 0)
        {
            sequence.AppendCallback(() => scatter.Visualize());
        }
        else if (i == 1)
        { }
        else
        { }
    }
}