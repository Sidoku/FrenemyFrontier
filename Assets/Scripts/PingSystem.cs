using Photon.Pun;

using UnityEngine;

public class PingSystem
{
    public GameObject Ping;

   /* public static void AddPing(Vector3 position)
    {
        // Spawn Ping prefab
        Debug.LogWarning(position);
        PhotonNetwork.InstantiateRoomObject(GameManager.instance.pfPingWorld.name, position, Quaternion.identity);
    }*/

    public static void AddPing(Vector3 position)
    {
        PhotonNetwork.InstantiateRoomObject("pfPing", position, Quaternion.identity);
        PingWindow.AddPing(position);
    }
}