using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class OutlineManagerCrim : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            this.GetComponent<Outline>().enabled= false;
           Component[] components =  this.GetComponentsInChildren<Outline>();
            foreach(var component in components)
            {
                component.GetComponent<Outline>().enabled = false;
            }
        }


        GameObject[] BH = GameObject.FindGameObjectsWithTag("BH");
        try
        {
            foreach(GameObject b in BH) { 
            
                if(b.GetPhotonView().IsMine)
                {
                    this.GetComponent<Outline>().enabled = false;
                    Component[] components = this.GetComponentsInChildren<Outline>();
                    foreach (var component in components)
                    {
                        component.GetComponent<Outline>().enabled = false;
                    }
                }
            }
        }
        catch(IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}