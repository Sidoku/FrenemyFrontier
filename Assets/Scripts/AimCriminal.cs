using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

using Photon.Pun;

public class AimCriminal : MonoBehaviourPunCallbacks
{


    
    private GameObject Ping;
    private GameObject PingForEnemy;
    public GameObject childping;
    public GameObject childpingForEnemy;
    private bool useEnemyPing = false;

    // Start is called before the first frame update
    void Start()
    {
        Ping = childping;
        childping.transform.position = new Vector3(0f, 0f, 0f);
        childping.transform.parent = null;

        PingForEnemy = childpingForEnemy;
        childpingForEnemy.transform.position = new Vector3(0f, 0f, 0f);
        childpingForEnemy.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        TogglePing();
        AimC();
    }


    private void AimC()
    {
       if(photonView.IsMine)
        {
            if (!useEnemyPing)
            {
                if (Input.GetMouseButtonDown(2) && this.GetComponent<ThirdPersonController>().isAiming)
                {
                    //PingSystem.AddPing(mouseWorldPosition);
                    Ping.transform.position = this.GetComponent<ThirdPersonController>().mouseWorldPosition;

                }

            }
            else
            {
                if (Input.GetMouseButtonDown(2) && this.GetComponent<ThirdPersonController>().isAiming)
                {
                    //PingSystem.AddPing(mouseWorldPosition);
                    PingForEnemy.transform.position = this.GetComponent<ThirdPersonController>().mouseWorldPosition;

                }

            }

        }

      
    }

    private void TogglePing()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            useEnemyPing = !useEnemyPing;
        }
    }


    
}
