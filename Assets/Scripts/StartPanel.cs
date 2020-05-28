using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : MonoBehaviour
{
    [Header("UI 요소")]
    [SerializeField] private Text question;
    [SerializeField] private RectTransform yearRect;
    [SerializeField] private RectTransform buttonRect;
    [SerializeField] private Text errorMsg;
    [SerializeField] private InputField yearInput;

    [Header("정보")]
    [SerializeField] private bool isValid;
    [SerializeField] private int startYear;

    private string questionText = "언제부터의" + System.Environment.NewLine + "분석 결과가" + System.Environment.NewLine + "보고 싶나요?";
    private Vector2 panelPreactivePos = new Vector2(1500f, 0f);
    private Vector2 panelActivePos = new Vector2(0f, 0f);
    private Vector2 panelPostactivePos = new Vector2(-1500f, 0f);
    private Vector2 buttonRectDeactivePos = new Vector2(-90f, -200f);
    private Vector2 buttonRectActivePos = new Vector2(-90f, 150f);

    public void StartStartPanel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() => Init())
            .Append(GetComponent<RectTransform>().DOAnchorPos(panelActivePos, 0.5f))
            .AppendInterval(0.5f)
            .Append(question.DOText(questionText, 1f).SetEase(Ease.Linear));
    }

    public void EndStartPanel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(GetComponent<RectTransform>().DOAnchorPos(panelPostactivePos, 0.5f))
            .AppendCallback(() => gameObject.SetActive(false));
    }

    private void Init()
    {
        gameObject.SetActive(true);
        isValid = false;
        question.text = "";
        GetComponent<RectTransform>().anchoredPosition = panelPreactivePos;
        buttonRect.anchoredPosition = buttonRectDeactivePos;
        ChangeErrorLog("");
    }

    public void ErrorCheck()
    {
        if (yearInput.text.Length == 0)
        {
            ChangeErrorLog("");
            isValid = false;
            return;
        }

        int year = 0;

        try
        {
            year = int.Parse(yearInput.text);
        }
        catch
        {
            ChangeErrorLog("숫자만 입력할 수 있습니다.");
            isValid = false;
            return;
        }

        if (yearInput.text.Length == 4 && 1990 <= year && year <= 2020)
        {
            ChangeErrorLog("");
            isValid = true;
            startYear = year;
            return;
            
        }
        else
        {
            ChangeErrorLog("1990~2020 사이의 분석만 가능합니다.");
            isValid = false;
            return;
        }
    }

    public void ShowNextButton()
    {
        if(isValid) buttonRect.DOAnchorPos(buttonRectActivePos, 1f).SetEase(Ease.OutCubic);
        else buttonRect.DOAnchorPos(buttonRectDeactivePos, 1f).SetEase(Ease.OutCubic);
    }

    private void ChangeErrorLog(string msg)
    {
        errorMsg.text = "";

        errorMsg.DOText(msg, 0.5f);
    }

    public void NextButton()
    {
        UIManager.instance.StartToEndPanel(startYear);
    }
}
