using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PseudoLine : MonoBehaviour
{
    LineRenderer currentLine;

    private void Awake()
    {
        if (!currentLine)
            currentLine = GetComponent<LineRenderer>();
        if (!currentLine)
            currentLine = gameObject.AddComponent<LineRenderer>();
    }

    public void UpdatePoint(bool isStartPoint, Vector3 point)
    {
        currentLine.SetPosition(isStartPoint ? 0 : 1, point);
        DDOLNavigation.Instance.currentDistanceText.text = string.Format("Distance: {0} cm",
        (Vector3.Distance(currentLine.GetPosition(0), currentLine.GetPosition(1)) * 100).ToString("00#.00"));
    }
}
