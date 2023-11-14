using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalRespawn : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector3[] options;


    public void Respawn()
    {
        foreach (Vector3 op in options)
        {
            Collider[] colliders = Physics.OverlapSphere(op, 15f, layerMask);
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
