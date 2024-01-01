using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderAttack : MonoBehaviour
{
    public LineRenderer TrailLaser;

    [SerializeField]
    private Transform startPoint;

    private void Start()
    {
        TrailLaser = gameObject.GetComponentInChildren<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TrailLaser.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                TrailLaser.SetPosition(1, hit.point);
            }
            if (hit.transform.tag == "Player")
            {
                Debug.Log("Olursen");
            }
        }
        else TrailLaser.SetPosition(1, transform.forward * 5000);
    }
}
