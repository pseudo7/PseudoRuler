using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PseudoLine : MonoBehaviour
{
    public LineUI lineUI;
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
        lineUI.UpdateDistance(currentLine.GetPosition(0), currentLine.GetPosition(1));
    }
}
