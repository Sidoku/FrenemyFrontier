using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RayControl : MonoBehaviourPunCallbacks
{
    public GameObject ray;
    ParticleSystemSync ps;
    // Start is called before the first frame update
    void Start()
    {
        
        //ps =  GetComponentInChildren<ParticleSystemSync>();
    }

    // Update is called once per frame
    void Update()
    {
        //ray.gameObject.transform.rotation = Quaternion.Euler(0f, this.transform.rotation.y, 0f);
    }

    [PunRPC]
    public void RayActivate()
    {
        
        ray.SetActive(true);
       // ps.ActivateParticleSystems();
        ray.GetComponent<Hovl_Laser>().MaxLength = 0f;
    }
    [PunRPC]
    public void IncreaseLenght()
    {
      
        ray.GetComponent<Hovl_Laser>().MaxLength = 10f;
    }
    [PunRPC]
    public void RayDeactivate()
    {
        
        //ps.DeactivateParticleSystems();
        ray.SetActive(false);
        ray.GetComponent<Hovl_Laser>().MaxLength = 0f;
    }


  
}
