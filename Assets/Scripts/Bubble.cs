using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private List<RectTransform> nodes;
    [SerializeField] private Transform plotTransform;
    [SerializeField] private RectTransform linePrefab;

    private void Start()
    {
        Vector2 myPos = GetComponent<RectTransform>().anchoredPosition;
        plotTransform = transform.parent;

        foreach (RectTransform node in nodes)
        {
            Vector2 nodePos = node.anchoredPosition;
            Vector2 mid = (myPos + nodePos) / 2;
            float length = Vector2.Distance(myPos, nodePos) - 50f;
            float angle = Mathf.Atan2((nodePos - myPos).y, (nodePos - myPos).x) * Mathf.Rad2Deg;

            RectTransform line = Instantiate(linePrefab, plotTransform);
            line.anchoredPosition = mid;
            line.sizeDelta = new Vector2(length, 3f);
            line.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
