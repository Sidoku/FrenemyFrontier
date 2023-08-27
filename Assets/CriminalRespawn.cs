using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalRespawn : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector3[] options;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            foreach(Vector3 op in options)
            {
                Collider[] colliders = Physics.OverlapSphere(op, 10f, layerMask);
                if(colliders.Length > 0)
                {
                    continue;
                }
                else
                {
                    this.gameObject.transform.position = op;
                }

            }




            //int randomIndex = Random.Range(0, options.Length);
            //Vector3 randomPosition = options[randomIndex];
            //this.gameObject.transform.position = randomPosition;

        }
    }

    public void Respawn()
    {
        foreach (Vector3 op in options)
        {
            Collider[] colliders = Physics.OverlapSphere(op, 10f, layerMask);
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
