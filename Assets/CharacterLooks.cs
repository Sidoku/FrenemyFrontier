using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterLooks : MonoBehaviourPunCallbacks
{

    Player player;
    
    public List<GameObject> EnableItems1 = new List<GameObject>();
    public List<GameObject> DisableItems1 = new List<GameObject>();
    public List<GameObject> EnableItems2 = new List<GameObject>();
    public List<GameObject> DisableItems2 = new List<GameObject>();
    public List<GameObject> EnableItems3 = new List<GameObject>();
    public List<GameObject> DisableItems3 = new List<GameObject>();
    public List<GameObject> EnableItems4 = new List<GameObject>();
    public List<GameObject> DisableItems4 = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

       
     
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void UpdatePlayerLooks(int playerAvatar)
    {

        
        

            if (playerAvatar == 0)
            {
                foreach (GameObject item in EnableItems1)
                {
                    item.SetActive(true);
                }

                foreach (GameObject item in DisableItems1)
                {
                    item.SetActive(false);
                }

            }
            else if (playerAvatar == 1)
            {
                foreach (GameObject item in EnableItems2)
                {
                    item.SetActive(true);
                }

                foreach (GameObject item in DisableItems2)
                {
                    item.SetActive(false);
                }
            }
        
        

        
    }
}
