using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;


public class DamageControlBon : MonoBehaviourPunCallbacks
{
    private int Health = 100;
    public TMP_Text healthtxt;
    public float attackCooldown = 0.2f;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {

        // this.gameObject.GetComponent<WeaponTracker>().weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && canAttack)
            {
                PerformAttack();
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
        if (other.gameObject.tag == "weaponInHand")
        {
            GameObject weapon = other.gameObject;

            if (weapon.transform.root.gameObject.layer == 11)
            {
               
                    
                
                this.gameObject.GetComponent<PhotonView>().RPC("OnDamageRecievedBon", RpcTarget.AllBuffered, weapon.GetComponent<WeaponDamage>().damage);

            }

        }
    }

    [PunRPC]
    public void OnDamageRecievedBon(int damage)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        this.gameObject.GetComponentInChildren<AnimationsManager>().GetHitAnim();
        Health = Health - damage;

        if (Health <= 0)
        {
            StartCoroutine(ResetSpawnAndHealth());


        }

        healthtxt.text = Health.ToString();


    }

    IEnumerator ResetSpawnAndHealth()
    {

        this.gameObject.transform.position = new Vector3(-5.532719612121582f, 1f, 119.69508361816406f);
        yield return new WaitForSeconds(1.5f);
        Health = 100;
        healthtxt.text = Health.ToString();
    }

    private IEnumerator ResetAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        this.gameObject.GetComponent<WeaponTracker>().weaponCollider.enabled = false;
    }
}
