using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Prologue3 : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private Text title;
    [SerializeField] private Image hanza;
    [SerializeField] private Text desc;
    [SerializeField] private Image nextButton;

    public override void OnEnable()
    {
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

        sequence.Append(title.rectTransform.DOAnchorPosY(title.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(0, title.DOColor(Color.black, duration))
            .Append(hanza.rectTransform.DOAnchorPosY(hanza.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(duration, hanza.DOColor(Color.white, duration))
            .Append(desc.rectTransform.DOAnchorPosY(desc.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(2 * duration, desc.DOColor(Color.black, duration))
            .Append(nextButton.rectTransform.DOAnchorPosY(nextButton.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(3 * duration, nextButton.DOColor(Color.grey, duration));
    }

    public override void OnDisable()
    {
    }
}