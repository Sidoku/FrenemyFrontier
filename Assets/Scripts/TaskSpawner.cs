using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSpawner : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector3[] options;
    public void TaskRespawn(GameObject taskObject)
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
                taskObject.transform.position = op;
            }
        }
    }
}
