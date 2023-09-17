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
    GameObject criminal;
    // Start is called before the first frame update
    void Start()
    {
        healthtxt.text = "Health " + Health.ToString();
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

                criminal = other.gameObject.transform.root.gameObject;
                Debug.Log("Attacked By " + criminal.name);
                int vd = criminal.GetComponent<PhotonView>().ViewID;

                this.gameObject.GetComponent<PhotonView>().RPC("OnDamageRecievedBon", RpcTarget.AllBuffered, weapon.GetComponent<WeaponDamage>().damage, vd);

            }

        }
    }

    [PunRPC]
    public void OnDamageRecievedBon(int damage,int vd)
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

            StartCoroutine(AddDeathBounty(vd));
        }

        healthtxt.text = "Health "  + Health.ToString();


    }

    IEnumerator ResetSpawnAndHealth()
    {

        this.gameObject.transform.position = new Vector3(-5.532719612121582f, 1f, 119.69508361816406f);
        yield return new WaitForSeconds(1.5f);
        Health = 100;
        healthtxt.text = "Health " + Health.ToString();
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
        PhotonView cr = PhotonView.Find(vd);
        cr.RPC("SetBounty", RpcTarget.AllBuffered,10000);
    }
}

