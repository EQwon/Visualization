using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WordCloudPanel : MonoBehaviour
{
    [SerializeField] private Text description;
    [SerializeField] private Transform cloudTransform;
    [SerializeField] private Transform listTransform;

    [Header("Prefab")]
    [SerializeField] private GameObject wordCloudPrefab;
    [SerializeField] private GameObject wordListPrefab;

    private int startYear;
    private int endYear;
    private Dictionary<string, int> data;
    private List<string> blackList = new List<string>();

    private List<WordCloud> clouds = new List<WordCloud>();
    private List<WordList> lists = new List<WordList>();
    private List<Vector2> cloudBasicPos = new List<Vector2>
    { new Vector2(152, 101), new Vector2(-243, -52), new Vector2(43, -276), new Vector2(-171, 276), new Vector2(305, -199) };
    private List<float> cloudSize = new List<float> { 420, 370, 300, 250, 180 };

    public void AssignData()
    {
        startYear = UIManager.instance.StartYear;
        endYear = UIManager.instance.EndYear;
        data = UIManager.instance.Database.GetTopWords(startYear, endYear, 5, blackList);
        description.text = "";
    }

    public void Visualize()
    {
        description.DOText(startYear + " ~ " + endYear + "년의 신춘문예 당선작", 1f);

        int cnt = 0;
        foreach (KeyValuePair<string, int> pair in data)
        {
            WordCloud cloud = Instantiate(wordCloudPrefab, cloudTransform).GetComponent<WordCloud>();
            clouds.Add(cloud);
            cloud.Panel = this;
            cloud.Visualize(pair.Key, cloudBasicPos[cnt], cloudSize[cnt]);

            WordList list = Instantiate(wordListPrefab, listTransform).GetComponent<WordList>();
            lists.Add(list);
            list.Visualize(cnt + 1, pair.Key, pair.Value);

            cnt += 1;
        }
    }

    public void AddBlackList(string blackWord)
    {
        blackList.Add(blackWord);

        DestroyAll();
        AssignData();
        Visualize();
    }

    private void DestroyAll()
    {
        foreach (WordCloud cloud in clouds)
            Destroy(cloud.gameObject);

        foreach (WordList list in lists)
            Destroy(list.gameObject);

        clouds = new List<WordCloud>();
        lists = new List<WordList>();
    }
}
