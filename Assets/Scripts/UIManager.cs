using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject prologue1;


    [Header("값")]
    [SerializeField] private int startYear;
    [SerializeField] private int endYear;

    private Database database;

    public Database Database { get { return database; } }
    public int StartYear { get { return startYear; } }
    public int EndYear { get { return endYear; } }

    private void Awake()
    {
        instance = this;
        database = GetComponent<Database>();
    }

    private void Start()
    {
        prologue1.SetActive(true);
    }

    public void StartToEndPanel(int startYear)
    {
        this.startYear = startYear;
    }

    public void EndToLoadingPanel(int endYear)
    {
        this.endYear = endYear;
    }

    public void LoadingToResultPanel()
    {
    }
}
