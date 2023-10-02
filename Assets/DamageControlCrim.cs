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
    GameObject bountyHunter = null;

    // Start is called before the first frame update
    void Start()
    {
        healthtxt.text = "Health " + Health.ToString();

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
            Debug.Log("AttackWeapon " + other.gameObject.name);
            if (weapon.transform.root.gameObject.layer == 10 && !onHold)
            {

               bountyHunter = other.gameObject.transform.root.gameObject;
                Debug.Log("Attacked By " + bountyHunter.name);
                int vd = bountyHunter.GetComponent<PhotonView>().ViewID;
                //bountyHunter.GetComponent<PhotonView>().RPC("SetBountyCollected", RpcTarget.AllBuffered, this.GetComponent<BountyAdded>().bountyAdded);
               
                    this.gameObject.GetComponent<PhotonView>().RPC("OnDamageRecievedCrim", RpcTarget.AllBuffered, weapon.GetComponent<WeaponDamage>().damage, vd);
              
     
                
                

                
                
            }

        }
    }

    [PunRPC]
    public void OnDamageRecievedCrim(int damage,int vd)
    {
      
        if (photonView.IsMine)
        {
            if (!onHold)
            {
                this.gameObject.GetComponentInChildren<AnimationsManager>().GetHitAnim();

                Health = Health - damage;

                if (Health <= 0)
                {
                    StartCoroutine(ResetSpawnAndHealth());
                    StartCoroutine(AddDeathBounty(vd));
                    if (this.GetComponent<CoinsCollected>().coinsCollected >= 0 && photonView.IsMine)
                    {
                        this.GetComponent<PhotonView>().RPC("LossCoins", RpcTarget.AllBuffered, (int)this.GetComponent<CoinsCollected>().coinsCollected / 2);
                    }
                }


            }

            healthtxt.text = "Health " + Health.ToString();
        }
      
       
   
        
      
    }

   


   public IEnumerator ResetSpawnAndHealth()
    {

        this.gameObject.GetComponent<CriminalRespawn>().Respawn();
        yield return new WaitForSeconds(1.5f);
        SetHealth(100);
        // exl.gameObject.GetComponent<PhotonView>().RPC("RemoveText", RpcTarget.AllBuffered);
        this.GetComponent<PhotonView>().RPC("RemoveWeapon", RpcTarget.AllBuffered);
       

    }

    private IEnumerator ResetAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        this.gameObject.GetComponent<WeaponTracker>().weaponCollider.enabled = false;
    }

    IEnumerator AddDeathBounty(int vd)
    {
        yield return new WaitForSeconds(1f);
        PhotonView bh = PhotonView.Find(vd);
        if(photonView.IsMine)
        {
            bh.RPC("SetBountyCollected", RpcTarget.AllBuffered, (int)this.GetComponent<BountyAdded>().bountyAdded / 2);
        }
      
    }

    public void SetHealth(int health)
    {
        Health = health;
        healthtxt.text = "Health " + Health.ToString();
    }

    
  

}
