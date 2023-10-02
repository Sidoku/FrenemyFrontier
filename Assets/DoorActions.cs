using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActions : MonoBehaviour
{

    Animator anim;
    bool endtereddoor = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && endtereddoor)
        {
            Debug.Log("After pressign E");

            if (anim.GetBool("DoorOpen"))
            {
                anim.SetBool("DoorOpen", false);
            }
            else
            {
                anim.SetBool("DoorOpen", true);
                Debug.Log("OpenDoor");
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        endtereddoor = true;
  


    }


    private void OnTriggerExit(Collider other)
    {
        endtereddoor = false;
    }
}
