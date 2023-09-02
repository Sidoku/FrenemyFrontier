using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField roomNameInput;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public TMP_Text roomName;

    public RoomItem roomItem;
    List<RoomItem> roomItemsList = new List<RoomItem>();
    public Transform contentObj;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;
  
    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject PlayButton;
    public TMP_Text debug_text;
    void Start()
    {
        PhotonNetwork.JoinLobby();

          

      
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient /*&& PhotonNetwork.CurrentRoom.PlayerCount >=2*/) { 
        
          PlayButton.SetActive(true);
        }else
        {
           PlayButton.SetActive(false);
        }

    }

    //will create a room with the name given by the masterclient and 4 max players can join that room.
    public void OnClickCreateRoom()
    {
        if(roomNameInput.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomNameInput.text,new Photon.Realtime.RoomOptions() {MaxPlayers = 4 ,BroadcastPropsChangeToAll = true});
        }
    }

    //Once the player joins the room lobby panel is disabled and room panel is enabled and the text for room name is assigned.
    public override void OnJoinedRoom()
    {
      lobbyPanel.SetActive(false);
      roomPanel.SetActive(true);
      roomName.text = "Room Name : " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    //Udpate the room list with 1.5 seconds between each update.
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime) 
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }

        
    }

    //udpate the player list by clearing out the current list and creating a new list.
    void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach(RoomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach(RoomInfo room in roomList)
        {
            RoomItem newRoom = Instantiate(roomItem, contentObj);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    //join room when room name is selected.
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    //leave room 
    public void OnClickRoomLeave()
    {
        PhotonNetwork.LeaveRoom();
    }

    //after leaving a room lobby panel is activated.
    public override void OnLeftRoom()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    //After leaving a room need to connect to lobby.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach(PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach(KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
           PlayerItem newPlayerItem =  Instantiate(playerItemPrefab, playerItemParent);
           newPlayerItem.SetPlayerInfo(player.Value); 
            if(player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

           playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }


    public void OnClickPlayButton()
    {
        int BountyHunters =0;
        int Criminals=0;
        GameObject[] playeritems = GameObject.FindGameObjectsWithTag("playerItem");
        foreach(GameObject playeritem in playeritems)
        {
            
            if(playeritem.GetComponent<PlayerItem>().playerRole.text == "Bounty Hunter")
            {
                BountyHunters++;
            }else
            {
                Criminals++;
            }
        }

        //PhotonNetwork.LoadLevel("Game");


         if(BountyHunters == Criminals && all_values_are_different())
         {
             PhotonNetwork.LoadLevel("Game");
         }
         else
         {

             debug_text.gameObject.SetActive(true);
             debug_text.text = "Please select equal number of bounty hunters and criminals or make sure to select different character for each player";
         }

    }

   public  bool all_values_are_different()
    {
        GameObject[] playeritems = GameObject.FindGameObjectsWithTag("playerItem");
        Debug.Log("Length of playerItems" + playeritems.Length);
        List<int> imagenumbers = new List<int>();
        foreach(GameObject playeritem in playeritems)
        {
            imagenumbers.Add(playeritem.GetComponent<PlayerItem>().imagenumber);
        }
        Debug.Log("Length of imagenumber" + imagenumbers.Count);
        if(imagenumbers.Count != imagenumbers.Distinct().Count())
        {
            return false;
        }else
        { return true; }
    }

   

}
