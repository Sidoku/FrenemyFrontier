using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EarningCoins : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "tressure")
        {
            if(photonView.IsMine)
            {
                this.GetComponent<PhotonView>().RPC("SetCoins", RpcTarget.AllBuffered, 5000);
                this.GetComponent<PhotonView>().RPC("SetBounty", RpcTarget.AllBuffered, 1000);
            }
        }
    }
}
