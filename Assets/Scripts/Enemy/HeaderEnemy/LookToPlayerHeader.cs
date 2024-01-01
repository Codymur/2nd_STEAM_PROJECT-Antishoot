using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayerHeader : MonoBehaviour
{
    public float RotationSpeed;
    public Transform PlayerTarget;

    private void Start()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
    }


    // Update is called once per frame
    void Update()
    {
        Quaternion rotTarget = Quaternion.LookRotation(PlayerTarget.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, RotationSpeed * Time.deltaTime);
    }
}
