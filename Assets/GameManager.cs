using UnityEngine;
using Photon.Pun;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{


    private GameManager() { }


    public GameObject[] playerPrefabs;
   // public GameObject pfPingWorldB;
   // public GameObject pfPingWorldC;
    public Transform pingSpawnPoint;
    public Transform bountyHunterSpawnPoint;
    public Transform CriminalSpawnPoint;
   // public Transform PingSpawnPoint;

    public static GameManager instance;

    // Start is called before the first frame update

    void Start()
    {

        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerRole"]];

        int playerAvatar = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"];

        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerRole"] == 0)
        {
            GameObject bountyHunterPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, bountyHunterSpawnPoint.position, Quaternion.identity);

            bountyHunterPlayer.GetComponent<PhotonView>().RPC("UpdatePlayerLooks", RpcTarget.AllBuffered, playerAvatar);
        }
        else
        {
            GameObject CriminalPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, CriminalSpawnPoint.position, Quaternion.identity);
            CriminalPlayer.GetComponent<PhotonView>().RPC("UpdatePlayerLooks", RpcTarget.AllBuffered, playerAvatar);
        }

        //PhotonNetwork.InstantiateRoomObject(pfPingWorldB.name, pingSpawnPoint.position + new Vector3(1f,0,0), Quaternion.identity);
        //PhotonNetwork.InstantiateRoomObject(pfPingWorldC.name, pingSpawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EndGame()
    {
        StartCoroutine(TonextScene());
    }

    IEnumerator TonextScene()
    {
        yield return new WaitForSeconds(10f);
        PhotonNetwork.LoadLevel("GameOver");
    }
}