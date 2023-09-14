using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

using Photon.Pun;

public class AimCriminal : MonoBehaviourPunCallbacks
{


    
    private GameObject Ping;
    public GameObject childping;

    // Start is called before the first frame update
    void Start()
    {
        Ping = childping;
        childping.transform.position = new Vector3(0f, 0f, 0f);
        childping.transform.parent = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        AimC();
    }


    private void AimC()
    {
       if(photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(2) && this.GetComponent<ThirdPersonController>().isAiming)
            {
                //PingSystem.AddPing(mouseWorldPosition);
                Ping.transform.position = this.GetComponent<ThirdPersonController>().mouseWorldPosition;

            }
        }

      
    }


    
}
