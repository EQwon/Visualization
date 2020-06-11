using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Prologue2 : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private Image deco;
    [SerializeField] private Text title;
    [SerializeField] private Image book1;
    [SerializeField] private Image book2;
    [SerializeField] private Image book3;
    [SerializeField] private Image book4;
    [SerializeField] private Image book5;
    [SerializeField] private Image book6;
    [SerializeField] private Image nextButton;

    public override void OnEnable()
    {
        deco.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        deco.color = Color.clear;
        title.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        title.color = Color.clear;
        book1.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book1.color = Color.clear;
        book2.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book2.color = Color.clear;
        book3.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book3.color = Color.clear;
        book4.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book4.color = Color.clear;
        book5.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book5.color = Color.clear;
        book6.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        book6.color = Color.clear;
        nextButton.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        nextButton.color = Color.clear;

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;

        sequence.Append(deco.rectTransform.DOAnchorPosY(deco.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(0, deco.DOColor(Color.white, duration))
            .Append(title.rectTransform.DOAnchorPosY(title.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(duration, title.DOColor(Color.black, duration))
            .Append(book1.rectTransform.DOAnchorPosY(book1.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(2 * duration, book1.DOColor(Color.white, duration))
            .Insert(2 * duration, book2.rectTransform.DOAnchorPosY(book2.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(2 * duration, book2.DOColor(Color.white, duration))
            .Insert(2 * duration, book3.rectTransform.DOAnchorPosY(book3.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(2 * duration, book3.DOColor(Color.white, duration))
            .Append(book4.rectTransform.DOAnchorPosY(book4.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(3 * duration, book4.DOColor(Color.white, duration))
            .Insert(3 * duration, book5.rectTransform.DOAnchorPosY(book5.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(3 * duration, book5.DOColor(Color.white, duration))
            .Insert(3 * duration, book6.rectTransform.DOAnchorPosY(book6.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(3 * duration, book6.DOColor(Color.white, duration))
            .Append(nextButton.rectTransform.DOAnchorPosY(nextButton.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(4 * duration, nextButton.DOColor(Color.grey, duration));
    }

    public override void OnDisable()
    {
    }
}