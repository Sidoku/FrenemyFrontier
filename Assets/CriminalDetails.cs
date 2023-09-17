using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using System;

public class CriminalDetails : MonoBehaviourPunCallbacks
{
    [SerializeField] Image pic1 = null;
    [SerializeField] Image pic2 = null;
    [SerializeField] Sprite p0;
    [SerializeField] Sprite p1;
    [SerializeField] Sprite p2;
    [SerializeField] Sprite p3;
    [SerializeField] TMP_Text c1Bounty;
    [SerializeField] TMP_Text c2Bounty;
    GameObject c1;
    GameObject c2;

    // Start is called before the first frame update
    void Start()
    {
       c1Bounty.text = string.Empty;
       c2Bounty.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] criminals = GameObject.FindGameObjectsWithTag("CR");

        try
        {
            if (criminals[0] != null)
            {
                c1 = criminals[0];
                c1Bounty.text = "Bounty  " + c1.GetComponent<BountyAdded>().bountyAdded;
                if (c1.GetComponent<CharacterLooks>().currentPlayerAvatar == 0)
                {
                    pic1.sprite = p0;

                }
                else if (c1.GetComponent<CharacterLooks>().currentPlayerAvatar == 1)
                {
                    pic1.sprite = p1;
                }
                else if (c1.GetComponent<CharacterLooks>().currentPlayerAvatar == 2)
                {
                    pic1.sprite = p2;
                }
                else if (c1.GetComponent<CharacterLooks>().currentPlayerAvatar == 3)
                {
                    pic1.sprite = p3;
                }
            }

            if (criminals[1] != null)
            {
                c2 = criminals[1];
                c2Bounty.text = "Bounty  " + c2.GetComponent<BountyAdded>().bountyAdded;
                if (c2.GetComponent<CharacterLooks>().currentPlayerAvatar == 0)
                {
                    pic2.sprite = p0;

                }
                else if (c2.GetComponent<CharacterLooks>().currentPlayerAvatar == 1)
                {
                    pic2.sprite = p1;
                }
                else if (c2.GetComponent<CharacterLooks>().currentPlayerAvatar == 2)
                {
                    pic2.sprite = p2;
                }
                else if (c2.GetComponent<CharacterLooks>().currentPlayerAvatar == 3)
                {
                    pic2.sprite = p3;
                }
            }
        }catch(IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }

        

       


      

    
    }
}
