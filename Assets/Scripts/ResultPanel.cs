using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] RectTransform menuRect;
    [SerializeField] WordCloudPanel wordCloudPanel;

    private Vector2 menuRectDeactivePos = new Vector2(0f, 500f);
    private Vector2 menuRectActivePos = new Vector2(0f, 0f);

    public void StartResultPanel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => Init())
            .Append(menuRect.DOAnchorPos(menuRectActivePos, 0.5f));
    }

    private void Init()
    {
        gameObject.SetActive(true);
        menuRect.anchoredPosition = menuRectDeactivePos;
    }
}
