using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItem : MonoBehaviour
{

    public TMP_Text roomName;

    LobbyManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager= FindObjectOfType<LobbyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }


    public void OnClickRoomItem()
    {
        manager.JoinRoom(roomName.text);
    }
}
