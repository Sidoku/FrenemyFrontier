using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


using TMPro;
using System;
using UnityEngine.UI;

public class GameProgressBar : MonoBehaviourPunCallbacks
{

    public int TotalCoinsCollected = 0;
    public int maximumCoins = 20000;
    public Image Image;
    
    [SerializeField] TMP_Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        coinsText.text = TotalCoinsCollected.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] criminals = GameObject.FindGameObjectsWithTag("CR");

        try
        {
            /*
            if (criminals[0] != null && criminals[1] == null)
            {
                TotalCoinsCollected = criminals[0].GetComponent<CoinsCollected>().coinsCollected;
                coinsText.text = TotalCoinsCollected.ToString();
            }

            if (criminals[1] != null && criminals[0] == null)
            {
                TotalCoinsCollected = criminals[1].GetComponent<CoinsCollected>().coinsCollected;
                coinsText.text = TotalCoinsCollected.ToString();
            }
            */
            if (criminals[0] != null && criminals[1] != null)
            {
                TotalCoinsCollected = criminals[0].GetComponent<CoinsCollected>().coinsCollected + criminals[1].GetComponent<CoinsCollected>().coinsCollected;
                coinsText.text = TotalCoinsCollected.ToString() + " / 20000";
            }
            
            GetCurrentFill();
           
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    void GetCurrentFill()
    {
        float fillAmount = (float)TotalCoinsCollected / (float)maximumCoins;
        Image.fillAmount = fillAmount;
    }
}
