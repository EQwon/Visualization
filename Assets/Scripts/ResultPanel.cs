using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] RectTransform menuRect;
    [SerializeField] RectTransform viewRect;
    [SerializeField] WordCloudPanel wordCloudPanel;

    private Vector2 menuRectDeactivePos = new Vector2(0f, 500f);
    private Vector2 menuRectActivePos = new Vector2(0f, 0f);
    private Vector2 viewRectDeactivePos = new Vector2(1200f, -300f);
    private Vector2 viewRectActivePos = new Vector2(0, -300f);

    public void StartResultPanel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => Init())
            .Append(menuRect.DOAnchorPos(menuRectActivePos, 0.5f))
            .AppendCallback(() => wordCloudPanel.AssignData())
            .Append(viewRect.DOAnchorPos(viewRectActivePos, 0.5f))
            .AppendCallback(() => wordCloudPanel.Visualize());
    }

    private void Init()
    {
        gameObject.SetActive(true);
        menuRect.anchoredPosition = menuRectDeactivePos;
        viewRect.anchoredPosition = viewRectDeactivePos;
    }
}
