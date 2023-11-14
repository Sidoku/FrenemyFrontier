using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class EndGameResults : MonoBehaviourPunCallbacks
{
    GameObject c1;
    GameObject c2;
    GameObject b1;
    GameObject b2;
    public int criminal1coins;
    public int criminal2coins;
    public int criminal1Avatar;
    public int criminal2Avatar;
    public int bountyhunter1bounty;
    public int bountyhunter2bounty;
    public int bountyhunter1Avatar;
    public int bountyhunter2Avatar;
    public string criminal1Name;
    public string criminal2Name;
    public string bountyhunter1Name;
    public string bountyhunter2Name;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    // Update is called once per frame
    void Update()
    {
       // CalculateAllValues();

       
    }
   
    public void CalculateAllValues()
    {
        GameObject[] criminals = GameObject.FindGameObjectsWithTag("CR");
        GameObject[] bountyhunters = GameObject.FindGameObjectsWithTag("BH");

        try
        {
            if (criminals[0] != null)
            {
                c1 = criminals[0];
                criminal1coins = c1.GetComponent<CoinsCollected>().coinsCollected;
                criminal1Avatar = c1.GetComponent<CharacterLooks>().currentPlayerAvatar;
                 
            }
            if (criminals[1] != null)
            {
                c2 = criminals[1];
                criminal2coins = c2.GetComponent<CoinsCollected>().coinsCollected;
                criminal2Avatar = c2.GetComponent<CharacterLooks>().currentPlayerAvatar;
               
            }

            if (bountyhunters[0] != null)
            {
                b1 = bountyhunters[0];
                bountyhunter1bounty = b1.GetComponent<BountyCollected>().bountyCollected;
                bountyhunter1Avatar = b1.GetComponent<CharacterLooks>().currentPlayerAvatar;
                
            }
            if (bountyhunters[1] != null)
            {
                b2 = bountyhunters[1];
                bountyhunter2bounty = b2.GetComponent<BountyCollected>().bountyCollected;
                bountyhunter2Avatar = b2.GetComponent<CharacterLooks>().currentPlayerAvatar;
               }
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
