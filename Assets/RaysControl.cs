using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RaysControl : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> particleSystemObjects = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void EnableParticleSystems()
    {
       
        foreach (GameObject ps in particleSystemObjects)
        {
            
            ps.SetActive(true);
            ps.GetComponent<Hovl_Laser>().MaxLength = 0f;
        }
    }
    [PunRPC]
    public void DisableParticleSystems()
    {
        
        foreach (GameObject ps in particleSystemObjects)
        {
            ps.SetActive(false);
            ps.GetComponent<Hovl_Laser>().MaxLength = 0f;
        }
    }
}
