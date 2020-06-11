using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Index : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private Image bar;
    [SerializeField] private List<RectTransform> texts;
    [SerializeField] private Image helpButton;

    public override void OnEnable()
    {
        bar.rectTransform.anchoredPosition -= new Vector2(1000f, 0);
        bar.color = Color.clear;
        foreach (RectTransform text in texts)
        {
            text.anchoredPosition -= new Vector2(1000f, 0);
        }
        helpButton.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        helpButton.color = Color.clear;

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;
        float delta = 0.1f;

        sequence.Append(bar.rectTransform.DOAnchorPosX(bar.rectTransform.anchoredPosition.x + 1000f, duration))
            .Insert(0, bar.DOColor(Color.black, duration));

        for(int i = 0; i < texts.Count; i++)
        {
            RectTransform text = texts[i];
            sequence.Insert(i * delta, text.DOAnchorPosX(text.anchoredPosition.x + 1000f, duration));
        }

        sequence.Insert(texts.Count * delta * 2, helpButton.rectTransform.DOAnchorPosY(helpButton.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(texts.Count * delta * 2, helpButton.DOColor(Color.grey, duration));
    }

    public override void OnDisable()
    {
    }
}