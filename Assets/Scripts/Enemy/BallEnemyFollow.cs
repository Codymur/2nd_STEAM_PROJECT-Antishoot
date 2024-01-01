using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemyFollow : MonoBehaviour
{


    public Transform target;  // The target position to move towards
    public float speed = 5.0f; // The movement speed
    public PlayerHealth PlayerHealthScript;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
        PlayerHealthScript = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (target != null && PlayerHealthScript.HealthBar.fillAmount > 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
