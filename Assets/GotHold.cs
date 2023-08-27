using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class GotHold : MonoBehaviourPunCallbacks
{

    Animator anim;
    GameObject bountyHunter;
    public bool gotHold = false;
 

    Vector3 offset = new Vector3(1f, 0f, -1f);
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyUp(KeyCode.B) && photonView.IsMine)
        {
            this.gameObject.GetComponent<PhotonView>().RPC("GotFree", RpcTarget.AllBuffered);
        }

      
    }

    [PunRPC]

    public void GotFree()
    {
        gotHold = false;
        anim.Play("GettingUnHold");
    }

    [PunRPC]

    public void OnCatch()
    { 
        if(photonView.IsMine)
        {
            anim.Play("GettingGot");
            gotHold= true;
            anim.SetBool("GotHold", gotHold);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "catchattack" && this.gameObject.GetComponent<DamageControlCrim>().Health <= 20) {
        
             bountyHunter = other.gameObject.transform.root.gameObject;
            if(!gotHold)
            {
                StartCoroutine(GetHold());
             
            }
     
        }
    }


    private void FixedUpdate()
    {

        if (gotHold)
        {
            if (photonView.IsMine)
            {



                Vector3 targetPosition = bountyHunter.transform.position + offset;


                //targetPosition.y = transform.position.y;

                // Smooth follow.    
                transform.LookAt(bountyHunter.transform.position);
                transform.position = targetPosition;
                this.gameObject.GetComponent<DamageControlCrim>().onHold = true;
                this.gameObject.GetComponent<ThirdPersonController>().JumpHeight = 0f;
                this.gameObject.GetComponent<ThirdPersonController>().onHold = true;
                this.gameObject.GetComponentInChildren<AnimationsManager>().onHold = true;
            }
            
        }
        else
        {
            if (photonView.IsMine)
            {
                this.gameObject.GetComponent<DamageControlCrim>().onHold = false;
                this.gameObject.GetComponent<ThirdPersonController>().JumpHeight = 1.2f;
                this.gameObject.GetComponent<ThirdPersonController>().onHold = false;
                this.gameObject.GetComponentInChildren<AnimationsManager>().onHold = false;
            }

          
         

        }
    }

    IEnumerator GetHold()
    {
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<PhotonView>().RPC("OnCatch", RpcTarget.AllBuffered);
    }
}
