using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TaskSpawner : MonoBehaviourPunCallbacks
{
    public LayerMask layerMask;
    public Vector3[] options;

    [PunRPC]
    public void TaskRespawn()
    {
        
        Debug.LogWarning("Task re-spawned");
        foreach (Vector3 op in options)
        {
            
            
                Collider[] colliders = Physics.OverlapSphere(op, 5f, layerMask);
                if (colliders.Length > 0)
                {
                    continue;
                }
                else
                {
                    this.gameObject.transform.position = op;

                }
            
          
        }
    }
}
