using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class PlayerItem : MonoBehaviourPunCallbacks
{

    public TMP_Text playerName;

   // Image backgroundImage;
   // public Color highlightedColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    public GameObject leftArrowButton2;
    public GameObject rightArrowButton2;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    public Image playerAvatar;
    public Sprite[] avatars;
    public TMP_Text playerRole;
    string[] roleText = { "Bounty Hunter","Criminal"};
    public int imagenumber;

    Player player;
    void Start()
    {
        //backgroundImage= GetComponent<Image>();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerInfo(Player _Player) {

        playerName.text = _Player.NickName;
        player = _Player;
        UpdatePlayerItem(player);
    } 

    public void ApplyLocalChanges()
    {
        //backgroundImage.color = highlightedColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
        leftArrowButton2.SetActive(true);
        rightArrowButton2.SetActive(true);
    }

    public void OnclickLeftArrow()
    {

        if((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }


    public void OnclickRightArrow()
    {
        
        if ((int)playerProperties["playerAvatar"] == avatars.Length -1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }



    public void OnclickLeftArrow2()
    {

        if ((int)playerProperties["playerRole"] == 0)
        {
            playerProperties["playerRole"] = 1;
        }
        else
        {
            playerProperties["playerRole"] = 0;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }


    public void OnclickRightArrow2()
    {

        if ((int)playerProperties["playerRole"] == 0)
        {
            playerProperties["playerRole"] = 1;
        }
        else
        {
            playerProperties["playerRole"] = 0;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
       if(player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        if(player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            imagenumber = (int)player.CustomProperties["playerAvatar"];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }

        if(player.CustomProperties.ContainsKey("playerRole"))
        {
            playerRole.text = roleText[(int)player.CustomProperties["playerRole"]];
            playerProperties["playerRole"] = (int)player.CustomProperties["playerRole"];

        }
        else
        {
            playerProperties["playerRole"] = 0;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

   
}
