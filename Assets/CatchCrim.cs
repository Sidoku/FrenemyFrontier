using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CatchCrim : MonoBehaviourPunCallbacks
{
    public LayerMask layer;
    
    public GameObject criminal = null;
    Animator anim;
    public bool gotCrim;
    public AudioSource kamehamehaSound;
    //public TMP_Text HelpUI;
   
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
        //Collider[] criminals = Physics.OverlapSphere(this.transform.position, 10f, layer);

        //if(criminals.Length > 0)
        //{

        //    foreach(Collider c in criminals)
        //    {



        //            criminal = c.gameObject;
        //            inTrigger = true;



        //    }
        //}
        //else
        //{
        //    inTrigger = false;
        //    criminal = null;
        //}



        //if (inTrigger)
        //{


        //    if (Input.GetKeyDown(KeyCode.F) && photonView.IsMine)
        //    {
        //        if (this.gameObject.GetComponent<WeaponTracker>().currentWeapon != null)
        //           StartCoroutine(WeaponControl());


        //        if(criminal != null)
        //        {
        //            this.transform.LookAt(criminal.transform.position);
        //            anim.Play("Catch");
        //            StartCoroutine(ConfirmPartcleDiable());

        //            this.GetComponentInChildren<AnimationsManager>().isAttacking = true;
        //            gotCrim= true;
        //            //criminal.GetComponent<PhotonView>().RPC("OnCatch", RpcTarget.AllBuffered);
        //        }

        //    }
        //}


       

        if (Input.GetKeyDown(KeyCode.F) && photonView.IsMine && gotCrim == false)
        {
            if (this.gameObject.GetComponent<WeaponTracker>().currentWeapon != null)
                StartCoroutine(WeaponControl());
            //this.transform.LookAt(criminal.transform.position);

            anim.SetBool("CatchCrim", true);
            anim.GetComponent<PhotonView>().RPC("RayActivate", RpcTarget.AllBuffered);
            StartCoroutine(IncreaseLengthofCatch());
            //kamehamehaSound.Play();
            StartCoroutine(DisableCatch());
            this.GetComponent<PhotonView>().RPC("PlayCatchSound", RpcTarget.AllBuffered);
            StartCoroutine(ConfirmPartcleDiable());
            this.GetComponentInChildren<AnimationsManager>().isAttacking= true;
            

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

    [PunRPC]
    public void SetCriminal(int vid)
    {
        if(photonView.IsMine)
        {
            gotCrim = true;
            PhotonView cr = PhotonView.Find(vid);
            criminal = cr.gameObject;
        }
   
    }

    IEnumerator WeaponControl()
    {
        this.gameObject.GetComponent<WeaponTracker>().currentWeapon.SetActive(false);
        yield return new WaitForSeconds(1.3f);
        this.gameObject.GetComponent<WeaponTracker>().currentWeapon.SetActive(true);
    }

    IEnumerator ConfirmPartcleDiable()
    {
        yield return new WaitForSeconds(1.5f);
        //this.GetComponentInChildren<RayControl>().RayDeactivate();
        anim.GetComponent<PhotonView>().RPC("RayDeactivate", RpcTarget.AllBuffered);
    }


    IEnumerator IncreaseLengthofCatch()
    {
        yield return new WaitForSeconds(0.4f);
        //this.GetComponentInChildren<RayControl>().RayDeactivate();
        anim.GetComponent<PhotonView>().RPC("IncreaseLenght", RpcTarget.AllBuffered);
    }


    [PunRPC]
    public void PlayCatchSound()
    {
        kamehamehaSound.Play();
    }


    IEnumerator DisableCatch()
    {
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("CatchCrim", false);
    }

}
