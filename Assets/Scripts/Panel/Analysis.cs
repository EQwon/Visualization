using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Analysis : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private Text title;
    [SerializeField] private Text desc;
    [SerializeField] private RectTransform resultPanel;

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
            .Append(resultPanel.DOAnchorPosY(resultPanel.anchoredPosition.y + 1000f, duration));
    }

    public override void OnDisable()
    {
    }
}