using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-4.3f, 109, 137);
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(-4.3f, 109, 137);
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
