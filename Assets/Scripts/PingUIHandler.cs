using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingUIHandler : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 pingPosition;
    public void Setup(Vector3 pingPosition)
    {
        this.pingPosition = pingPosition;
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (pingPosition - fromPosition).normalized;

        float uiRadius = 270f;
        rectTransform.anchoredPosition = dir * uiRadius;
    }
}
