using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CatchCrim : MonoBehaviourPunCallbacks
{
    public LayerMask layer;
    bool inTrigger = false;
    GameObject criminal = null;
    Animator anim;
    public bool gotHold;
    public TMP_Text HelpUI;
   
    // Start is called before the first frame update
    void Start()
    {
        anim= GetComponentInChildren<Animator>();
     
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            inTrigger = false;
           criminal = null;
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        Collider[] criminals = Physics.OverlapSphere(this.transform.position, 10f, layer);

        if(criminals.Length > 0)
        {
            
            foreach(Collider c in criminals)
            {
               
                

                    criminal = c.gameObject;
                    inTrigger = true;

                
              
            }
        }
        else
        {
            inTrigger = false;
            criminal = null;
        }

       

        if (inTrigger)
        {
            
          
            if (Input.GetKeyDown(KeyCode.F) && photonView.IsMine)
            {
                if (this.gameObject.GetComponent<WeaponTracker>().currentWeapon != null)
                   StartCoroutine(WeaponControl());
                 
                 
                if(criminal != null)
                {
                    this.transform.LookAt(criminal.transform.position);
                    anim.Play("Catch");
                    gotHold = true;
                    //criminal.GetComponent<PhotonView>().RPC("OnCatch", RpcTarget.AllBuffered);
                }
                  
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && photonView.IsMine)
        {
            if (this.gameObject.GetComponent<WeaponTracker>().currentWeapon != null)
                StartCoroutine(WeaponControl());
            //this.transform.LookAt(criminal.transform.position);
            anim.Play("Catch");
       
            

        }


    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11)
        {
            inTrigger = true;
            criminal = other.gameObject;
        }
    }

    */

    

    IEnumerator WeaponControl()
    {
        this.gameObject.GetComponent<WeaponTracker>().currentWeapon.SetActive(false);
        yield return new WaitForSeconds(1.3f);
        this.gameObject.GetComponent<WeaponTracker>().currentWeapon.SetActive(true);
    }



  


}
