using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class BountyAdded : MonoBehaviourPunCallbacks
{
    private int _bountyAdded = 500;

    [SerializeField] TMP_Text bountyAddedText;


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
    public void SetBounty(int bounty)
    {
        this.bountyAdded = _bountyAdded + bounty;
    }

    [PunRPC]
    public void LossBounty()
    {
        _bountyAdded = 500;
       
    }
}
