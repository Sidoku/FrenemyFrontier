using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using StarterAssets;

public class WeaponTracker : MonoBehaviourPunCallbacks
{
    public GameObject currentWeapon = null;
    public Collider weaponCollider;
    public bool weaponNotinHand = true;
    [SerializeField] GameObject doorPanel;
    public AudioSource weaponPickupSound;
    public TMP_Text weaponStatText;
    private GameObject weaponOnScreen = null;
    //[SerializeField] GameObject pickUPweaponPanel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (currentWeapon != null)
        {
            weaponNotinHand = false;
            weaponCollider = currentWeapon.GetComponent<BoxCollider>();
        }
        else
        {
            weaponNotinHand = true;
        }

        if(this.GetComponent<ThirdPersonController>().weaponSeen != null)
        {
            weaponOnScreen = FindChildGameObject(this.GetComponent<ThirdPersonController>().weaponSeen);
            if(weaponOnScreen != null) {
                weaponStatText.text = "Weapon Damage is " + weaponOnScreen.GetComponent<WeaponDamage>().damage.ToString();
            }
            else
            {
                weaponStatText.text = " ";
            }
           
        }else
        {
            weaponOnScreen = null;
            weaponStatText.text = " ";
        }

        
           
    }

   

   

    GameObject FindChildGameObject(string childGameObject)
    {
        string childObjectName = childGameObject;

        // Get the Transform component of the parent GameObject
        Transform parentTransform = this.transform;

        // Use the Find method to search for the child GameObject by name
        Transform childTransform = parentTransform.FindRecursively(childObjectName);

        // Check if the child GameObject was found
        if (childTransform != null)
        {
            // Access the child GameObject or its components
            GameObject childObject = childTransform.gameObject;
            // Do something with the child GameObject
            return childObject;
        }
        else
        {
            Debug.Log("Child GameObject not found!");
            return null;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "weapon")
        {
            //pickUPweaponPanel.SetActive(true);
            
                
                    Debug.Log("Selected Weapon " + other.gameObject.name);
                    string weoponName = other.gameObject.name;
                    if (currentWeapon != null && currentWeapon.activeSelf)
                    {
                        currentWeapon.SetActive(false);
                        currentWeapon = FindChildGameObject(weoponName);
                        currentWeapon.SetActive(true);
                        //pickUPweaponPanel.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Tried to find weapon");
                        currentWeapon = FindChildGameObject(weoponName);
                        currentWeapon.SetActive(true);
                        //pickUPweaponPanel.SetActive(false);
                    }
                
               

                Destroy(other.gameObject);
                weaponPickupSound.Play();
            
            
        }

        if(other.gameObject.tag == "door")
        {
            doorPanel.SetActive(true);
        }

       
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "door")
        {
            doorPanel.SetActive(false);
        }

      
    }


    [PunRPC]

    public void RemoveWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            currentWeapon = null;
        }
    }

}
