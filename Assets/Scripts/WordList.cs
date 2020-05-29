using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WordList : MonoBehaviour
{
    [SerializeField] private Text rankText;
    [SerializeField] private Text wordText;
    [SerializeField] private Text countText;

    private RectTransform rect;
    private float listHeight = 150f;

    public void Visualize(int rank, string word, int count)
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -listHeight * rank);

        rankText.text = "";
        wordText.text = "";
        countText.text = "";

        rankText.DOText(rank.ToString(), 1f);
        wordText.DOText(word, 1f);
        countText.DOText(count.ToString(), 1f);
    }
}
