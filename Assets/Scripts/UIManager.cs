using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private StartPanel startPanel;
    [SerializeField] private EndPanel endPanel;
    [SerializeField] private LoadingPanel loadingPanel;
    [SerializeField] private ResultPanel resultPanel;

    [Header("값")]
    [SerializeField] private int startYear;
    [SerializeField] private int endYear;

    public int StartYear { get { return startYear; } }
    public int EndYear { get { return endYear; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startPanel.gameObject.SetActive(false);
        endPanel.gameObject.SetActive(false);
        loadingPanel.gameObject.SetActive(false);
        resultPanel.gameObject.SetActive(false);

        startPanel.StartStartPanel();
    }

    public void StartToEndPanel(int startYear)
    {
        this.startYear = startYear;
        startPanel.EndStartPanel();
        endPanel.StartEndPanel();
    }

    public void EndToLoadingPanel(int endYear)
    {
        this.endYear = endYear;
        endPanel.EndEndPanel();
        loadingPanel.StartLoadingPanel();
    }

    public void LoadingToResultPanel()
    {
        loadingPanel.EndLoadingPanel();
        resultPanel.StartResultPanel();
    }
}
