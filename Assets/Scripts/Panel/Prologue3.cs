using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Prologue3 : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private List<Text> writers;
    [SerializeField] private Image hanza;
    [SerializeField] private Text title;
    [SerializeField] private Text desc;
    [SerializeField] private Image nextButton;

    public override void OnEnable()
    {
        foreach (Text writer in writers)
        {
            writer.rectTransform.anchoredPosition -= new Vector2(0, 200f);
            writer.color = Color.clear;
        }
        title.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        title.color = Color.clear;
        hanza.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        hanza.color = Color.clear;
        desc.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        desc.color = Color.clear;
        nextButton.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        nextButton.color = Color.clear;

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;
        float interval = 0.3f * writers.Count;

        for (int i = 0; i < writers.Count; i++)
        {
            Text writer = writers[i];
            RectTransform rect = writer.rectTransform;

            sequence.Insert(i * 0.2f, rect.DOAnchorPosY(rect.anchoredPosition.y + 200f, duration))
                .Insert(i * 0.2f, writer.DOColor(Color.black, duration));
        }
        sequence.AppendInterval(interval)
            .Insert(interval, hanza.rectTransform.DOAnchorPosY(hanza.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(interval, hanza.DOColor(Color.white, duration))
            .Insert(interval + duration, title.rectTransform.DOAnchorPosY(title.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(interval + duration, title.DOColor(Color.black, duration))
            .Insert(interval + 2 * duration, desc.rectTransform.DOAnchorPosY(desc.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(interval + 2 * duration, desc.DOColor(Color.black, duration))
            .Insert(interval + 3 * duration, nextButton.rectTransform.DOAnchorPosY(nextButton.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(interval + 3 * duration, nextButton.DOColor(Color.grey, duration));
    }

    public override void OnDisable()
    {
    }
}