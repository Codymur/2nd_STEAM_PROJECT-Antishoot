using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLook : MonoBehaviour
{
    public Transform target;
    public float RotationSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
    }

    void Update()
    {
        Vector3 targetPostition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        //this.transform.LookAt(targetPostition);



        Quaternion rotTarget = Quaternion.LookRotation(targetPostition - this.transform.position);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTarget, RotationSpeed * Time.deltaTime);
    }
}
