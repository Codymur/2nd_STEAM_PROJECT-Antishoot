using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderPosition : MonoBehaviour
{

    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + 9, transform.position.z);
    }
}
