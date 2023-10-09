using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ParticleSystemSync : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private List<ParticleSystem> particleSystemsToSync = new List<ParticleSystem>();
    [SerializeField] private List<GameObject> particleSystemObjects = new List<GameObject>();

    private Dictionary<ParticleSystem, bool> initialPlayOnAwakeState = new Dictionary<ParticleSystem, bool>();

    private void Awake()
    {
        // Store the initial "Play On Awake" state of each Particle System
        foreach (ParticleSystem ps in particleSystemsToSync)
        {
            initialPlayOnAwakeState[ps] = ps.playOnAwake;
        }
    }

    public void ActivateParticleSystems()
    {
        photonView.RPC("RPC_ActivateParticleSystems", RpcTarget.All);
    }

    public void EnableParticleSystems()
    {
        foreach (GameObject ps in particleSystemObjects)
        {
            ps.SetActive(true);
        }
    }

    public void DisableParticleSystems()
    {
        foreach (GameObject ps in particleSystemObjects)
        {
            ps.SetActive(false);
        }
    }
    public void DeactivateParticleSystems()
    {
        photonView.RPC("RPC_DeactivateParticleSystems", RpcTarget.All);
    }

  

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Implement custom synchronization logic if needed.
    }
}
