using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEditor.Rendering;

public class DamageControlCrim : MonoBehaviourPunCallbacks
{
    public int Health = 100;
    public TMP_Text healthtxt;
    public float attackCooldown = 0.2f;
    private bool canAttack = true;
    public bool onHold = false;
    public TMP_Text catchme;
    public GameObject exl;

    // Start is called before the first frame update
    void Start()
    {

       // this.gameObject.GetComponent<WeaponTracker>().weaponCollider.enabled = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && canAttack && !onHold)
            {
                PerformAttack();
            }

            if (Health <= 20)
            {

                exl.SetActive(true);
                exl.gameObject.GetComponent<PhotonView>().RPC("ChangeText", RpcTarget.AllBuffered);

            }
            else
            {


                exl.gameObject.GetComponent<PhotonView>().RPC("RemoveText", RpcTarget.AllBuffered);
            }
        }
     
    }


    private void PerformAttack()
    {
        if (photonView.IsMine)
        {
            if (this.gameObject.GetComponent<WeaponTracker>().weaponNotinHand == false)
            {
                this.gameObject.GetComponent<WeaponTracker>().weaponCollider.enabled = true; // Enable attack trigger



                StartCoroutine(ResetAttackCooldown());
            }
        }
     
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weaponInHand" && !onHold)
        {
            GameObject weapon = other.gameObject;

            if (weapon.transform.root.gameObject.layer == 10 && !onHold)
            {
                
                  
                
              
                this.gameObject.GetComponent<PhotonView>().RPC("OnDamageRecievedCrim", RpcTarget.AllBuffered, weapon.GetComponent<WeaponDamage>().damage);

            }

        }
    }

    [PunRPC]
    public void OnDamageRecievedCrim(int damage)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if(!onHold)
        {
            this.gameObject.GetComponentInChildren<AnimationsManager>().GetHitAnim();

            Health = Health - damage;

            if (Health <= 0)
            {
                StartCoroutine(ResetSpawnAndHealth());


            }

           

            healthtxt.text = Health.ToString();
        }
   
        
      
    }

   


   public IEnumerator ResetSpawnAndHealth()
    {

        this.gameObject.GetComponent<CriminalRespawn>().Respawn();
        yield return new WaitForSeconds(1.5f);
        Health = 100;
        healthtxt.text = Health.ToString();
       // exl.gameObject.GetComponent<PhotonView>().RPC("RemoveText", RpcTarget.AllBuffered);
    
    }

    private IEnumerator ResetAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        this.gameObject.GetComponent<WeaponTracker>().weaponCollider.enabled = false;
    }



}
