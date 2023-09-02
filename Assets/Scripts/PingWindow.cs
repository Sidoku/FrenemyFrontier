using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingWindow : MonoBehaviour
{

    public GameObject pfPingUI;
    public static void AddPing(Vector3 position)
    {
        Transform pingUITransform = PhotonNetwork.InstantiateRoomObject("pfPingUI", position, Quaternion.identity).transform;
        pingUITransform.GetComponent<PingUIHandler>().Setup(position);
    }
}
