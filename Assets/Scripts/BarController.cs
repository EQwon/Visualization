using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BarController : MonoBehaviour
{
    [SerializeField] private Text word;
    [SerializeField] private RectTransform bar;
    [SerializeField] private Text count;

    private string myWord;
    private int myCount;
    private int maxCount;

    private float maxWidth = 600f;
    private float height = 80f;

    public void AssignValues(string word, int count, int maxCount)
    {
        this.myWord = word;
        this.myCount = count;
        this.maxCount = maxCount;

        AdjustValues();
    }

    private void AdjustValues()
    {
        word.DOText(myWord, 1f);
        count.DOText(myCount.ToString(), 1f);
        bar.DOSizeDelta(new Vector2((float)myCount / maxCount * maxWidth, height), 1f)
            .SetEase(Ease.InOutCubic);
    }

    public void Initialize()
    {
        word.DOText("", 1f);
        count.DOText("", 1f);
        bar.DOSizeDelta(new Vector2(0, height), 1f)
            .SetEase(Ease.InOutCubic);
    }
}
