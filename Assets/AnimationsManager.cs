using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationsManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public Animator anim;
    public string[] attackAnimationNames; // Array to store the names of your combo attack animations
    public int comboIndex = 0;
    public bool isAttacking = true;
    int currentIntex = -1;
    public bool onHold = false;
    public AudioSource audioSourceSword;
    public AudioSource audioSourceGethit;

    void Start()
    {
        anim = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && !onHold)
            {
                if (isAttacking)
                {
                    anim.Play(attackAnimationNames[0]);
                    isAttacking = false;
                    audioSourceSword.Play();
                    //this.GetComponent<PhotonView>().RPC("PlaySwingSound", RpcTarget.AllBuffered);
                }

                currentIntex++;


            }

        }


     



    }



    public void GetHitAnim()
    {
        anim.Play("GetHit");
        isAttacking = true;
        //audioSourceGethit.Play();
       
        this.GetComponent<PhotonView>().RPC("PlayHitSound", RpcTarget.AllBuffered);
    }

    public void OnComboAttackAnimationFinished()
    {

        if (comboIndex >= attackAnimationNames.Length)
        {
            // Reset combo if we reach the end of the attackAnimationNames array
            comboIndex = 0;
            currentIntex = -1;
            isAttacking = true;
        }

        if (currentIntex>comboIndex)
        {
           
            
            
                isAttacking= false;
                comboIndex++;
                if (comboIndex >= attackAnimationNames.Length)
                {
                // Reset combo if we reach the end of the attackAnimationNames array
                comboIndex = 0;
                currentIntex = -1;
                isAttacking = true;
            }
            else
            {
                currentIntex = comboIndex;
                anim.Play(attackAnimationNames[comboIndex]);
                audioSourceSword.Play();
            }
                 
               
            
           
        }
        else
        {
            comboIndex = 0;
            currentIntex = -1;
            isAttacking = true;
        }

      

    }


    [PunRPC]
    public void PlayHitSound()
    {
        audioSourceGethit.Play();
    }





}
