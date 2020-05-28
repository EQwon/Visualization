using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingPanel : MonoBehaviour
{
    [SerializeField] private Text calcText;

    private Vector2 panelPreactivePos = new Vector2(1500f, 0f);
    private Vector2 panelActivePos = new Vector2(0f, 0f);
    private Vector2 panelPostactivePos = new Vector2(-1500f, 0f);

    public void StartLoadingPanel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => Init())
            .Append(GetComponent<RectTransform>().DOAnchorPos(panelActivePos, 0.5f))
            .Append(calcText.DOText("연산중..!", 2f).SetEase(Ease.OutCubic).SetLoops(3, LoopType.Restart))
            .AppendCallback(() => UIManager.instance.LoadingToResultPanel());
    }

    public void EndLoadingPanel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(GetComponent<RectTransform>().DOAnchorPos(panelPostactivePos, 0.5f))
            .AppendCallback(() => gameObject.SetActive(false));
    }

    private void Init()
    {
        gameObject.SetActive(true);
        calcText.text = "연산중";
        GetComponent<RectTransform>().anchoredPosition = panelPreactivePos;
    }
}
