using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WordList : MonoBehaviour
{
    [SerializeField] private Text rankText;
    [SerializeField] private Text wordText;
    [SerializeField] private Text oldCountText;
    [SerializeField] private Text nowCountText;

    private RectTransform rect;
    private float listHeight = 150f;

    public void Visualize(int rank, string word, int oldCount, int nowCount)
    {
        rect = GetComponent<RectTransform>();

        rankText.text = "";
        wordText.text = "";
        oldCountText.text = "";
        nowCountText.text = "";

        rankText.DOText(rank.ToString(), 1f);
        wordText.DOText(word, 1f);
        oldCountText.DOText(oldCount.ToString(), 1f);
        nowCountText.DOText(nowCount.ToString(), 1f);
    }
}
