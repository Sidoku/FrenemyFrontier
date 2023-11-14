using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class GameEneCal : MonoBehaviourPunCallbacks
{
  
    public int criminal1coins1;
    public int criminal2coins1;
    public int criminal1Avatar1;
    public int criminal2Avatar1;
    public int bountyhunter1bounty1;
    public int bountyhunter2bounty1;
    public int bountyhunter1Avatar1;
    public int bountyhunter2Avatar1;
    public string criminal1Name1;
    public string criminal2Name1;
    public string bountyhunter1Name1;
    public string bountyhunter2Name1;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
       // DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GetData()
    {
        EndGameResults bh = FindObjectOfType<EndGameResults>();

        if(bh != null )
        {
            criminal1coins1 = bh.criminal1coins;

            criminal2coins1 = bh.criminal2coins;
            criminal1Avatar1 = bh.criminal1Avatar;
            criminal2Avatar1 = bh.criminal2Avatar;
            bountyhunter1bounty1 = bh.bountyhunter1bounty;
            bountyhunter2bounty1 = bh.bountyhunter2bounty;
            bountyhunter1Avatar1 = bh.bountyhunter1Avatar;
            bountyhunter2Avatar1 = bh.bountyhunter2Avatar;
            criminal1Name1 = bh.criminal1Name;
            criminal2Name1 = bh.criminal2Name;
            bountyhunter1Name1 = bh.bountyhunter1Name;
            bountyhunter2Name1 = bh.bountyhunter2Name;
        }
    }
     


  
}