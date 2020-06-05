using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WordCloud : MonoBehaviour
{
    [SerializeField] private Text text;

    private FrequencyGraphPanel panel;
    private RectTransform rect;
    private string myWord;

    public FrequencyGraphPanel Panel { set { panel = value; } }

    public void Visualize(string word, Vector2 pos, float size)
    {
        rect = GetComponent<RectTransform>();
        text.text = "";
        myWord = word;

        rect.anchoredPosition = pos;
        rect.sizeDelta = Vector2.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(rect.DOSizeDelta(Vector2.one * size, 0.5f).SetEase(Ease.OutBounce))
            .Append(text.DOText(word, 0.5f));
    }

    public void AddBlackList()
    {
        panel.AddBlackList(myWord);
    }
}
