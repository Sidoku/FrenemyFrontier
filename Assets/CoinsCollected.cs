using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class CoinsCollected : MonoBehaviourPunCallbacks
{
    private int _coinsCollected = 0;
    //public string playerName;
    [SerializeField] TMP_Text coinscollectedText;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    Timer timer;
    bool doneCal = false;
    public int coinsCollected
    {
        get => _coinsCollected;
        set => _coinsCollected = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer =FindObjectOfType<Timer>();
        // playerName =   PhotonNetwork.NickName;
        NetworkSetCoins();
    }

    // Update is called once per frame
    void Update()
    {
        coinscollectedText.text = "Coins collected " + this.coinsCollected.ToString();
        if(timer.abouttoEnd && !doneCal)
        {
            NetworkSetCoins();
            doneCal= true;
        }
       


    }

    [PunRPC]
    public void SetCoins(int coins)
    {
        this.coinsCollected = _coinsCollected + coins;
        //PhotonNetwork.LocalPlayer.SetScore(this.coinsCollected);
    
    }

    [PunRPC]
    public void LossCoins(int coins)
    {
        this.coinsCollected = _coinsCollected - coins;
        if (this.coinsCollected < 0)
        {
            _coinsCollected = 0;
        }
      
    }


    public void NetworkSetCoins()
    {
        playerProperties["Collected"] = this.coinsCollected;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
}
