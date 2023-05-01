using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject[] playerPrefabs;
    public Transform bountyHunterSpawnPoint;
    public Transform CriminalSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
       
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerRole"]];
        int playerAvatar = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"];

        if ((int) PhotonNetwork.LocalPlayer.CustomProperties["playerRole"] == 0)
                {
            GameObject bountyHunterPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, bountyHunterSpawnPoint.position, Quaternion.identity);
           
            bountyHunterPlayer.GetComponent<PhotonView>().RPC("UpdatePlayerLooks", RpcTarget.AllBuffered, playerAvatar);
        }
        else
        {
            GameObject CriminalPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, CriminalSpawnPoint.position, Quaternion.identity);
            CriminalPlayer.GetComponent<PhotonView>().RPC("UpdatePlayerLooks", RpcTarget.AllBuffered,playerAvatar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
