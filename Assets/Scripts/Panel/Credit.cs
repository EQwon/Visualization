using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Credit : Panel
{
    [Header("Rect Transform")]
    [SerializeField] private Text title;
    [SerializeField] private List<Text> descs;

    public override void OnEnable()
    {
        title.rectTransform.anchoredPosition -= new Vector2(0, 200f);
        title.color = Color.clear;

        foreach (Text desc in descs)
        {
            desc.rectTransform.anchoredPosition -= new Vector2(0, 200f);
            desc.color = Color.clear;
        }

        Sequence sequence = DOTween.Sequence();
        float duration = 0.5f;

        sequence.Append(title.rectTransform.DOAnchorPosY(title.rectTransform.anchoredPosition.y + 200f, duration))
            .Insert(0, title.DOColor(Color.black, duration));

        for (int i = 0; i < descs.Count; i++)
        {
            Text desc = descs[i];
            sequence.Append(desc.rectTransform.DOAnchorPosY(desc.rectTransform.anchoredPosition.y + 200f, duration))
                .Insert(duration * (i + 1), desc.DOColor(Color.black, duration));
        }
    }

    public override void OnDisable()
    {
    }
}
