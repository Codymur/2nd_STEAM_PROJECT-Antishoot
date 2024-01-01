using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAtPlayer : MonoBehaviour
{
    public Transform target;
    public bool Machine;
    private void Start()
    {
        if (Machine == true)
        {
            target = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
