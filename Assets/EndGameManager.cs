using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Unity.VisualScripting;

public class EndGameManager : MonoBehaviourPunCallbacks
{
   // GameEneCal eneCal;
    [SerializeField] TMP_Text message;
    [SerializeField] TMP_Text message1;
    [SerializeField] TMP_Text message2;
    [SerializeField] TMP_Text message3;
    TMP_Text[] messages = new TMP_Text[4];

    
    // Start is called before the first frame update
    void Start()
    {

        messages[0] = message;
        messages[1] = message1;
        messages[2] = message2;
        messages[3] = message3;
                
        //eneCal = FindObjectOfType<GameEneCal>();
        //message.text = "The Player " + eneCal.criminal2Name1 + " Has Collected " + eneCal.criminal2coins1;
        int i = 0;
        foreach (var item in PhotonNetwork.PlayerList)
        {
            
            if (item.CustomProperties.ContainsKey("playerRole"))
            {
               if((int)item.CustomProperties["playerRole"] == 0)
                {
                    messages[i].text = "BountyHunter " + item.NickName + " has collected " + item.CustomProperties["Collected"];
                }
                else
                {  
                    messages[i].text = "Criminal " + item.NickName + " has collected " + item.CustomProperties["Collected"];
                }
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
