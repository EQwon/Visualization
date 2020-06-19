using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Node
{
    public string a;
    public string b;

    public Node(string a, string b)
    {
        this.a = a;
        this.b = b;
    }
}

public class Bigram : MonoBehaviour
{
    [SerializeField] private Transform plotTransform;
    [SerializeField] private TextAsset asset;
    [SerializeField] private Text bubblePrefab;
    [SerializeField] private RectTransform linePrefab;

    private List<Node> nodes;
    private List<Text> bubbles = new List<Text>();
    private List<string> words = new List<string>();

    private float lastClickTime = 0f;
    private float doubleClickDeltaTime = 0.5f;

    private void Start()
    {
        nodes = Parser.BigramParser(asset);
        MakeBigram();
    }

    private void MakeBigram()
    {
        int n = 0;
        foreach(Node node in nodes)
        {
            if (!words.Contains(node.a))
            {
                words.Add(node.a);
                Text bubble = Instantiate(bubblePrefab, plotTransform);
                bubble.GetComponent<RectTransform>().anchoredPosition = GetPos(n);
                bubbles.Add(bubble);
                bubble.text = node.a;
                bubble.name = node.a;
                n += 1;
            }
            if (!words.Contains(node.b))
            {
                words.Add(node.b);
                Text bubble = Instantiate(bubblePrefab, plotTransform);
                bubble.GetComponent<RectTransform>().anchoredPosition = GetPos(n);
                bubbles.Add(bubble);
                bubble.text = node.b;
                bubble.name = node.b;
                n += 1;
            }

            // make line between two word
            int indexA = 0, indexB = 0;
            for(int i = 0; i < words.Count; i++)
            {
                if(words[i] == node.a) indexA = i;
                if (words[i] == node.b) indexB = i;
            }
            Vector2 posA = bubbles[indexA].GetComponent<RectTransform>().anchoredPosition;
            Vector2 posB = bubbles[indexB].GetComponent<RectTransform>().anchoredPosition;
            Vector2 mid = (posA + posB) / 2;
            float length = Vector2.Distance(posA, posB) - 40f;
            float angle = Mathf.Atan2((posB - posA).y, (posB - posA).x) * Mathf.Rad2Deg;

            RectTransform line = Instantiate(linePrefab, plotTransform);
            line.anchoredPosition = mid;
            line.sizeDelta = new Vector2(length, 3f);
            line.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void OnPointerDown()
    {
        if (Time.time - lastClickTime <= doubleClickDeltaTime) Enlarge(Input.mousePosition);
        else lastClickTime = Time.time;
    }

    private void Enlarge(Vector2 pos)
    {
        Debug.Log(pos);
    }

    private Vector2 GetPos(int i)
    {
        int y = i / 8;
        int x = y % 2 == 0 ? i % 8 : 7 - (i % 8);

        return new Vector2(x, y) * 125f + new Vector2(Random.Range(-10f, 10f), Random.Range(-30f, 30f));
    }
}