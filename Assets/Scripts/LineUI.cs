using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUI : MonoBehaviour
{
    static Transform mainCamTransform;

    TextMesh distanceText;

    private void Awake()
    {
        distanceText = GetComponent<TextMesh>();
        mainCamTransform = Camera.main.transform;
    }

    public void UpdateDistance(Vector3 start, Vector3 end)
    {
        transform.position = (start + end) / 2 + Vector3.up * 5;
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamTransform.position);
        distanceText.text = string.Format("{0}", Vector3.Distance(start, end).ToString("00#.##"));
    }
}
