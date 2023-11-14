using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
using StarterAssets;
using UnityEngine.UIElements;

public class BountyCollected : MonoBehaviourPunCallbacks
{
    private int _bountyCollected = 0;
    public string playerName;
    [SerializeField] TMP_Text bountycollectedText;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    Timer timer;
    bool doneCal = false;
    [SerializeField] GameObject endgamePanel;
    public GameObject killed;
    public TMP_Text BountyPlus;
    public AudioSource BountyCollectedSound;
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
            if (photonView.IsMine)
            {
                this.GetComponent<ThirdPersonController>().JumpHeight = 0f;
                this.GetComponent<ThirdPersonController>().onHold = true;
                endgamePanel.SetActive(true);
            }
          
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

    [PunRPC]
    public void KillAckBH()
    {
        StartCoroutine(BHShowKill());

        // PhotonNetwork.LocalPlayer.SetScore(this.bountyCollected);

    }
    [PunRPC] 
    public void BountyPlusBH(int bounty)
    {
        StartCoroutine(BHShowBounty(bounty));
    }

    public void NetworkSetBounty()
    {
        if(photonView.IsMine)
        {
            playerProperties["Collected"] = this.bountyCollected;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }


    }

    IEnumerator BHShowKill()
    {
       killed.SetActive(true);
        yield return new WaitForSeconds(2f);
       killed.SetActive(false);
    }

    IEnumerator BHShowBounty(int bounty)
    {
        //BountyPlus.gameObject.SetActive(true);
        BountyPlus.text = "BountyCollected + " + bounty.ToString();
        BountyCollectedSound.Play();
        yield return new WaitForSeconds(2f);
        BountyPlus.text = " ";
        //BountyPlus.gameObject.SetActive(false);
    }
}
