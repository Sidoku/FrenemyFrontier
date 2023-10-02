using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class GotHold : MonoBehaviourPunCallbacks
{

    Animator anim;
    GameObject bountyHunter = null;
    public bool gotHold = false;
    public Vector3 jailPos;
    int cvid;
    public AudioSource getiingHoldSound;
    public AudioSource getiingFreeSound;

    Vector3 offset = new Vector3(1f, 0f, -1f);
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        cvid = this.gameObject.GetComponent<PhotonView>().ViewID;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B) && photonView.IsMine)
        {
            //this.gameObject.GetComponent<PhotonView>().RPC("GotFree", RpcTarget.AllBuffered);
        }

      


    }

    [PunRPC]

    public void GotFree()
    {

        gotHold = false;
        getiingHoldSound.Stop();
        anim.Play("GettingUnHold");
        getiingFreeSound.Play();
        StartCoroutine(ConfirmParticleDisable());
        this.GetComponent<DamageControlCrim>().SetHealth(50);
        //bountyHunter.GetComponent<CatchCrim>().gotCrim = false;
        
    }

    [PunRPC]

    public void OnCatch()
    {
        if (photonView.IsMine)
        {
            anim.Play("GettingGot");
            StartCoroutine(ConfirmParticleEnable());

            gotHold = true;
            anim.SetBool("GotHold", gotHold);
            getiingHoldSound.Play();
            StartCoroutine(GetFreeSoon());



      
        }

      

        if(bountyHunter!= null)
        {
            bountyHunter.GetComponent<UpdateSlider>().MainReduce(cvid);
        }
      

    }

    [PunRPC]

    public void GotJailed()
    {

        gotHold = false;
        getiingHoldSound.Stop();
        anim.Play("GettingUnHold");
        StartCoroutine(ConfirmParticleDisable());
        this.transform.position = jailPos;
        if (bountyHunter != null && photonView.IsMine)
        {
            bountyHunter.GetComponent<PhotonView>().RPC("SetBountyCollected", RpcTarget.AllBuffered, this.GetComponent<BountyAdded>().bountyAdded);
            bountyHunter.GetComponent<PhotonView>().RPC("BountyPlusBH", RpcTarget.AllBuffered, this.GetComponent<BountyAdded>().bountyAdded);
        }

        StartCoroutine(CriminalCaluculations());

        StartCoroutine(RestFromJail());
        //bountyHunter.GetComponent<CatchCrim>().gotCrim = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "catchattack" && this.gameObject.GetComponent<DamageControlCrim>().Health <= 20)
        {

            bountyHunter = other.gameObject.transform.root.gameObject;
          
           // bountyHunter.GetComponent<CatchCrim>().gotCrim = true;
            if (!gotHold)
            {
                StartCoroutine(GetHold());

            }

        }

        if (other.gameObject.tag == "jail" && gotHold)
        {
            if (photonView.IsMine)
            {

                this.gameObject.GetComponent<PhotonView>().RPC("GotJailed", RpcTarget.AllBuffered);
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
        if (photonView.IsMine)
            this.gameObject.GetComponent<PhotonView>().RPC("OnCatch", RpcTarget.AllBuffered);
    }

    IEnumerator RestFromJail()
    {
        yield return new WaitForSeconds(10f);

        this.gameObject.GetComponent<PhotonView>().RPC("ResetHealthSPwn", RpcTarget.AllBuffered);
    }

    [PunRPC]

    public void ResetHealthSPwn()
    {
        StartCoroutine(GetComponent<DamageControlCrim>().ResetSpawnAndHealth());
    }

    IEnumerator ConfirmParticleDisable()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponentInChildren<RaysControl>().DisableParticleSystems();

    }

    IEnumerator ConfirmParticleEnable()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponentInChildren<RaysControl>().EnableParticleSystems();

    }

    IEnumerator CriminalCaluculations()
    {
        yield return new WaitForSeconds(10f);
        if (this.GetComponent<CoinsCollected>().coinsCollected >= 0 && photonView.IsMine)
        {
            this.GetComponent<PhotonView>().RPC("LossCoins", RpcTarget.AllBuffered, (int)this.GetComponent<BountyAdded>().bountyAdded);
            this.GetComponent<PhotonView>().RPC("CoinsLossCR", RpcTarget.AllBuffered, (int)this.GetComponent<BountyAdded>().bountyAdded);
        }
        if (photonView.IsMine)
            this.GetComponent<PhotonView>().RPC("LossBounty", RpcTarget.AllBuffered);
    }


    IEnumerator GetFreeSoon()
    {
        if(photonView.IsMine)
        {
            this.GetComponent<CrimUpdateSlider>().mainReduceSlider();
        }
      
        yield return new WaitForSeconds(30f);
        if(gotHold && photonView.IsMine)
        {
            this.gameObject.GetComponent<PhotonView>().RPC("GotFree", RpcTarget.AllBuffered);
        }
      
    }

}

