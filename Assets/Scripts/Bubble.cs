using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private List<Bubble> bubbles;
    [SerializeField] private RectTransform linePrefab;

    private Transform plotTransform;

    public void AddBubble(Bubble bubble)
    {
        bubbles.Add(bubble);
    }

    private void Start()
    {
        Vector2 myPos = GetComponent<RectTransform>().anchoredPosition;
        plotTransform = transform.parent;

        foreach (Bubble bubble in bubbles)
        {
            RectTransform bubbleRect = bubble.GetComponent<RectTransform>();
            Vector2 bubblePos = bubbleRect.anchoredPosition;
            Vector2 mid = (myPos + bubblePos) / 2;
            float length = Vector2.Distance(myPos, bubblePos) - 50f;
            float angle = Mathf.Atan2((bubblePos - myPos).y, (bubblePos - myPos).x) * Mathf.Rad2Deg;

            RectTransform line = Instantiate(linePrefab, plotTransform);
            line.anchoredPosition = mid;
            line.sizeDelta = new Vector2(length, 3f);
            line.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void AssignEach()
    {
        
    }
}
