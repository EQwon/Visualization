using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bar : MonoBehaviour
{
    [SerializeField] private Text word;
    [SerializeField] private Text count;

    private RectTransform barRect;
    private string myWord;

    private float maxWidth = 700f;
    private float barHeight = 60f;

    public void Visualize(int index, string word, int count, int maxCnt)
    {
        barRect = GetComponent<RectTransform>();
        this.count.text = "";
        myWord = word;

        barRect.anchoredPosition = new Vector2(200, -index * 70f);
        barRect.sizeDelta = new Vector2(0, barHeight);

        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => this.word.text = word)
            .Append(barRect.DOSizeDelta(new Vector2((float)count / maxCnt * maxWidth, barHeight), 0.5f).SetEase(Ease.OutBounce))
            .Append(this.count.DOText(count.ToString(), 0.5f));
    }
}
