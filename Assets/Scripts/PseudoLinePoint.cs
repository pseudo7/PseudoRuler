using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoLinePoint : MonoBehaviour
{
    public bool isStartPoint;

    static Camera mainCam;
    static Transform camTransform;

    PseudoLine currentLine;
    int layerMask;

    private void Awake()
    {
        mainCam = Camera.main;
        camTransform = mainCam.transform;
        layerMask = LayerMask.NameToLayer("AbraKadabra");
    }

    void Start()
    {
        if (!currentLine) currentLine = GetComponentInParent<PseudoLine>();
        UpdatePointInParent();
    }

    private void OnMouseDrag()
    {
        MovePoint();
        UpdatePointInParent();
    }

    void UpdatePointInParent()
    {
        currentLine.UpdatePoint(isStartPoint, transform.position);
    }

    void MovePoint()
    {
        Vector3 mousePos = Input.mousePosition;
        RaycastHit hit;
        Vector3 mouseF = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCam.farClipPlane));
        Vector3 mouseN = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCam.nearClipPlane));

        if (Physics.Raycast(mouseN, mouseF - mouseN, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore)) transform.position = hit.point;
    }
}
