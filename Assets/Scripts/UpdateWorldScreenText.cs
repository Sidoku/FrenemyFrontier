using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class UpdateWorldScreenText : MonoBehaviourPunCallbacks
{
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void ChangeText() { 
    
     this.gameObject.SetActive(true);

    }

    [PunRPC]
    public void RemoveText()
    {

        this.gameObject.SetActive(false);

    }
}
