using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Text yearText;
    [SerializeField] private Image knob;
    [SerializeField] private Text onOffText;
    [SerializeField] private int targetYear;

    private bool isOn;
    private BarChart barChart;
    private float deltaX = 38;

    private void Start()
    {
        barChart = transform.parent.parent.GetComponent<BarChart>();

        onOffText.text = "OFF";
        onOffText.color = Color.blue;
        yearText.text = targetYear.ToString();
    }

    public void Toggle()
    {
        isOn = !isOn;

        float endValue;
        

        if (isOn)
        {
            endValue = deltaX;
            onOffText.text = "ON";
            onOffText.DOColor(Color.red, 0.5f);
        }
        else
        {
            endValue = -deltaX;
            onOffText.text = "OFF";
            onOffText.DOColor(Color.blue, 0.5f);
        }
        knob.rectTransform.DOAnchorPosX(endValue, 0.5f);

        barChart.ChangeData(targetYear, isOn);
    }
}
