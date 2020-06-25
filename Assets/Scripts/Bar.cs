using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bar : MonoBehaviour
{
    [SerializeField] private Text word;
    [SerializeField] private Text count;
    [SerializeField] private RectTransform oldRect;
    [SerializeField] private RectTransform nowRect;

    private RectTransform barRect;
    private string myWord;

    private float maxWidth = 700f;
    private float barHeight = 60f;

    public void Visualize(int index, string word, int old, int now, int maxCnt)
    {
        barRect = GetComponent<RectTransform>();
        this.count.text = "";
        myWord = word;

        barRect.anchoredPosition = new Vector2(200, -index * 70f);
        oldRect.sizeDelta = new Vector2(0, barHeight);
        nowRect.anchoredPosition += new Vector2((float)old / maxCnt * maxWidth, 0);
        nowRect.sizeDelta = new Vector2(0, barHeight);

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => this.word.text = word)
            .Append(oldRect.DOSizeDelta(new Vector2((float)old / maxCnt * maxWidth, barHeight), 0.2f).SetEase(Ease.OutBounce))
            .Append(nowRect.DOSizeDelta(new Vector2((float)now / maxCnt * maxWidth, barHeight), 0.2f).SetEase(Ease.OutBounce))
            .Append(count.DOText((old + now).ToString(), 0.5f));
    }
}
