using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using StarterAssets;
using UnityEngine.UI;

public class CoinsCollected : MonoBehaviourPunCallbacks
{
    private int _coinsCollected = 0;
    //public string playerName;
    [SerializeField] TMP_Text coinscollectedText;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    Timer timer;
    bool doneCal = false;
    [SerializeField] GameObject endgamePanel;
    public TMP_Text coinsPlus;
    public TMP_Text coinsLoss;
    public AudioSource coinsAddedSound;
    public AudioSource coinsLossSound;
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
        {   if(photonView.IsMine)
            {
                this.GetComponent<ThirdPersonController>().JumpHeight = 0f;
                this.GetComponent<ThirdPersonController>().onHold = true;
                endgamePanel.SetActive(true);
            }
           
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
    public void CoinsPlusCR(int coins)
    {
        StartCoroutine(CRShowCoinsplus(coins));
    }

    [PunRPC]
    public void CoinsLossCR(int coins)
    {
        StartCoroutine(CRShowCoinsloss(coins));
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
        if (photonView.IsMine)
        {
            playerProperties["Collected"] = this.coinsCollected;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }

        
    }

    IEnumerator CRShowCoinsplus(int coins)
    {

        coinsPlus.text = "Coins Added + " + coins.ToString();
        coinsAddedSound.Play();
        yield return new WaitForSeconds(2f);
        coinsPlus.text = " ";

    }


    IEnumerator CRShowCoinsloss(int coins)
    {

        coinsPlus.text = "Coins Removed - " + coins.ToString();
        coinsLossSound.Play();
        yield return new WaitForSeconds(2f);
        coinsPlus.text = " ";

    }
}
