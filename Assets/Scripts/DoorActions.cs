using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorActions : MonoBehaviourPunCallbacks
{

    Animator anim;
    bool endtereddoor = false;
    public AudioSource doorOpenSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && endtereddoor)
        {
            Debug.Log("After pressign E");
            this.GetComponent<MeshCollider>().enabled = false;
            
            if (anim.GetBool("DoorOpen"))
            {
                anim.SetBool("DoorOpen", false);
                doorOpenSound.Play();
            }
            else
            {
                anim.SetBool("DoorOpen", true);
                doorOpenSound.Play();
                Debug.Log("OpenDoor");
            }

        }

      
    }


    private void OnTriggerEnter(Collider other)
    {
        endtereddoor = true;
  


    }


    private void OnTriggerExit(Collider other)
    {
        endtereddoor = false;
    }


   public void EnableCollider()
    {
        this.gameObject.GetComponent<MeshCollider>().enabled = true;
    }

    public void DisableCollider()
    {
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
    }

}
