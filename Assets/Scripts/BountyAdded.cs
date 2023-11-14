using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BountyAdded : MonoBehaviourPunCallbacks
{
    private int _bountyAdded = 500;
    public GameObject killed;
    [SerializeField] TMP_Text bountyAddedText;
    public TMP_Text bountyplus;
    public AudioSource BountyAddedSound;
    public int bountyAdded
    {
        get => _bountyAdded;
        set => _bountyAdded = value;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bountyAddedText.text = "Bounty " + this.bountyAdded.ToString();
    }


    [PunRPC]
    public void KillAckCR()
    {
        StartCoroutine(CRShowKill());

        // PhotonNetwork.LocalPlayer.SetScore(this.bountyCollected);

    }

    [PunRPC]
    public void BountyPlusCR(int bounty)
    {
        StartCoroutine(CRShowBounty(bounty));
    }

    [PunRPC]
    public void SetBounty(int bounty)
    {
        this.bountyAdded = _bountyAdded + bounty;
    }

    [PunRPC]
    public void LossBounty()
    {
        _bountyAdded = 500;
       
    }



    IEnumerator CRShowKill()
    {
        killed.SetActive(true);
        yield return new WaitForSeconds(2f);
        killed.SetActive(false);
    }

    IEnumerator CRShowBounty(int bounty)
    {

        bountyplus.text = "BountyAdded + " + bounty.ToString();
        BountyAddedSound.Play();
        yield return new WaitForSeconds(2f);
        bountyplus.text = " ";

    }

}
