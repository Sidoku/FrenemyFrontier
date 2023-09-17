using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class BountyCollected : MonoBehaviourPunCallbacks
{
    private int _bountyCollected = 0;
    public string playerName;
    [SerializeField] TMP_Text bountycollectedText;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    Timer timer;
    bool doneCal = false;
    public int bountyCollected
    {
        get => _bountyCollected;
        set => _bountyCollected = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        // playerName = PhotonNetwork.NickName;
        NetworkSetBounty();
    }

    // Update is called once per frame
    void Update()
    {
        bountycollectedText.text = "Bounty collected " + this.bountyCollected.ToString();
        if (timer.abouttoEnd && !doneCal)
        {
            NetworkSetBounty();
            doneCal= true;
        }
           
    }

    [PunRPC]
    public void SetBountyCollected(int bounty)
    {
        this.bountyCollected = _bountyCollected + bounty;

       // PhotonNetwork.LocalPlayer.SetScore(this.bountyCollected);
   
    }

    public void NetworkSetBounty()
    {
        playerProperties["Collected"] = this.bountyCollected;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);

    }
}
