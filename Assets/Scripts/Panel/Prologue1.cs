using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Prologue1 : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private Text text1;
    [SerializeField] private Image image1;
    [SerializeField] private Text text2;
    [SerializeField] private Image book;
    [SerializeField] private Text names;

    public override void OnEnable()
    {
        text1.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        text1.color = Color.clear;
        image1.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        image1.color = Color.clear;
        text2.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        text2.color = Color.clear;
        book.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book.color = Color.clear;
        names.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        names.color = Color.clear;

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;

        sequence.Append(text1.rectTransform.DOAnchorPosY(text1.rectTransform.anchoredPosition.y + 200f, duration))
            .Append(image1.rectTransform.DOAnchorPosY(image1.rectTransform.anchoredPosition.y + 200f, duration))
            .Append(text2.rectTransform.DOAnchorPosY(text2.rectTransform.anchoredPosition.y + 200f, duration))
            .Append(book.rectTransform.DOAnchorPosY(book.rectTransform.anchoredPosition.y + 200f, duration))
            .Append(names.rectTransform.DOAnchorPosY(names.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(0, text1.DOColor(Color.white, duration))
            .Insert(duration, image1.DOColor(Color.white, duration))
            .Insert(2 * duration, text2.DOColor(Color.white, duration))
            .Insert(3 * duration, book.DOColor(Color.white, duration))
            .Insert(4 * duration, names.DOColor(Color.white, duration));
    }

    public override void OnDisable()
    {
    }
}