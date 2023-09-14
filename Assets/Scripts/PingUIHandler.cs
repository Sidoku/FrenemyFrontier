using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PingUIHandler : MonoBehaviourPunCallbacks
{
    private RectTransform rectTransform;
    private Vector3 pingPosition;
    public Camera playerCamera;
    public void Setup(Vector3 pingPosition)
    {
        this.pingPosition = pingPosition;
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (photonView.IsMine) {

            if(pingPosition!= null)
            {
                Vector3 fromPosition = playerCamera.transform.position;
                fromPosition.z = 0f;
                Vector3 dir = (pingPosition - fromPosition).normalized;

                float uiRadius = 270f;
                rectTransform.anchoredPosition = dir * uiRadius;
            }
        

        }

  
    }
}
