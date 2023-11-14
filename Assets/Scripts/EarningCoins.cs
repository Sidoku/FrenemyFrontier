using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EarningCoins : MonoBehaviourPunCallbacks
{ 
    public void EarnCoins()
    {
            if(photonView.IsMine)
            {
                this.GetComponent<PhotonView>().RPC("SetCoins", RpcTarget.AllBuffered, 5000);
                this.GetComponent<PhotonView>().RPC("SetBounty", RpcTarget.AllBuffered, 1000);
                this.GetComponent<PhotonView>().RPC("BountyPlusCR", RpcTarget.AllBuffered, 1000);
                this.GetComponent<PhotonView>().RPC("CoinsPlusCR", RpcTarget.AllBuffered, 5000);
            }
    }
}
