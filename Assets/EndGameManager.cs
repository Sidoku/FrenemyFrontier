using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Unity.VisualScripting;
using Photon.Realtime;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviourPunCallbacks
{
   // GameEneCal eneCal;
    [SerializeField] TMP_Text message;
    [SerializeField] TMP_Text message1;
    [SerializeField] TMP_Text message2;
    [SerializeField] TMP_Text message3;
    [SerializeField] TMP_Text WinnerPlayer;
    [SerializeField] TMP_Text WinningTeam;
    [SerializeField] Image WinnerImage;
    [SerializeField] Sprite a1;
    [SerializeField] Sprite a2;
    [SerializeField] Sprite a3;
    [SerializeField] Sprite a4;
    [SerializeField] GameObject winnerPanel;
    [SerializeField] GameObject StatsPanel;
    TMP_Text[] messages = new TMP_Text[4];
    int totalBountyhunters;
    int totalCriminals;
   
    Player BountyHunter1;
    Player Criminal1;
    Player BountyHunter2;
    Player Criminal2;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible= true;
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
                if ((int)item.CustomProperties["playerRole"] == 0)
                {
                    messages[i].text = "BountyHunter " + item.NickName + " has collected " + item.CustomProperties["Collected"];
                    if(BountyHunter1 == null)
                    {
                      
                        BountyHunter1 = item;
                    }else
                    {
                        BountyHunter2 = item;
                    }
                   
                    
                  
                }
                else
                {
                    messages[i].text = "Criminal " + item.NickName + " has collected " + item.CustomProperties["Collected"];

                    if (Criminal1 == null)
                    {

                        Criminal1 = item;
                    }
                    else
                    {
                        Criminal2 = item;
                    }

                  
                }
            }

            i++;
        }

        StartCoroutine(CalculateWinners());

    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator CalculateWinners()
    {
        yield return new WaitForSeconds(1f);
        //totalBountyhunters = (int)BountyHunter1.CustomProperties["Collected"] + (int)BountyHunter2.CustomProperties["Collected"];
        totalCriminals = (int)Criminal1.CustomProperties["Collected"] + (int)Criminal2.CustomProperties["Collected"];
        if(totalCriminals >= 20000)
        {
            //crimials win
            WinningTeam.text = "Criminals win";
            if((int)Criminal1.CustomProperties["Collected"] > (int)Criminal2.CustomProperties["Collected"])
            {
                //c1 winner
                WinnerPlayer.text = "Winner is " + Criminal1.NickName;
                if ((int)Criminal1.CustomProperties["playerAvatar"] == 0)
                {
                    WinnerImage.sprite = a1;
                }else if((int)Criminal1.CustomProperties["playerAvatar"] == 1)
                {
                    WinnerImage.sprite = a2;
                }else if((int)Criminal1.CustomProperties["playerAvatar"] == 2)
                {
                    WinnerImage.sprite = a3;
                }
                else if ((int)Criminal1.CustomProperties["playerAvatar"] == 3)
                {
                    WinnerImage.sprite = a4;
                }

            }
            else
            {
                //c2 winner
                WinnerPlayer.text = "Winner is " + Criminal2.NickName;
                if ((int)Criminal2.CustomProperties["playerAvatar"] == 0)
                {
                    WinnerImage.sprite = a1;
                }
                else if ((int)Criminal2.CustomProperties["playerAvatar"] == 1)
                {
                    WinnerImage.sprite = a2;
                }
                else if ((int)Criminal2.CustomProperties["playerAvatar"] == 2)
                {
                    WinnerImage.sprite = a3;
                }
                else if ((int)Criminal2.CustomProperties["playerAvatar"] == 3)
                {
                    WinnerImage.sprite = a4;
                }
            }
        }else
        {
            //bountyHunters win
            WinningTeam.text = "BountyHunters win";
            if ((int)BountyHunter1.CustomProperties["Collected"] > (int)BountyHunter2.CustomProperties["Collected"])
            {
                //b1 winner
                WinnerPlayer.text = "Winner is " + BountyHunter1.NickName;
               
                if ((int)BountyHunter1.CustomProperties["playerAvatar"] == 0)
                {
                    WinnerImage.sprite = a1;
                }
                else if ((int)BountyHunter1.CustomProperties["playerAvatar"] == 1)
                {
                    WinnerImage.sprite = a2;
                }
                else if ((int)BountyHunter1.CustomProperties["playerAvatar"] == 2)
                {
                    WinnerImage.sprite = a3;
                }
                else if ((int)BountyHunter1.CustomProperties["playerAvatar"] == 3)
                {
                    WinnerImage.sprite = a4;
                }
            }
            else
            {
                //b2 winner
                WinnerPlayer.text = "Winner is " + BountyHunter2.NickName;
                if ((int)BountyHunter2.CustomProperties["playerAvatar"] == 0)
                {
                    WinnerImage.sprite = a1;
                }
                else if ((int)BountyHunter2.CustomProperties["playerAvatar"] == 1)
                {
                    WinnerImage.sprite = a2;
                }
                else if ((int)BountyHunter2.CustomProperties["playerAvatar"] == 2)
                {
                    WinnerImage.sprite = a3;
                }
                else if ((int)BountyHunter2.CustomProperties["playerAvatar"] == 3)
                {
                    WinnerImage.sprite = a4;
                }
            }
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickStats()
    {
        winnerPanel.SetActive(false);
        StatsPanel.SetActive(true);
    }

    public void OnClickBack()
    {
        winnerPanel.SetActive(true);
        StatsPanel.SetActive(false);
    }

}
