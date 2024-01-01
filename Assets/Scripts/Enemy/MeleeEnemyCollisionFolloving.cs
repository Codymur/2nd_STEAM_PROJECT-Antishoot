using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyCollisionFolloving : MonoBehaviour
{
    private NavMeshAgent EnemyAgent;

    public Transform target;  // The target position to move towards
    //public float speed = 5.0f; // The movement speed

    public PlayerHealth HealthBarScript;

    public DetectPlayer DetectPlayerScript;


    private void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
        HealthBarScript = GameObject.FindGameObjectWithTag("MainPlayerTag").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (DetectPlayerScript.playerInTheRoom == true)
        {
            if (target != null && HealthBarScript.HealthBar.fillAmount > 0f)
            {
                EnemyAgent.SetDestination(target.position);
                //// Calculate the move direction in the XZ plane
                //Vector3 moveDirection = target.position - transform.position;
                //moveDirection.y = 0; // Ignore the Y component

                //// Calculate the new XZ position based on the move direction and speed
                //Vector3 newPosition = transform.position + moveDirection.normalized * speed * Time.deltaTime;

                //// Update the object's position (only modify the X and Z coordinates)
                //transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
            }
            else if (HealthBarScript.HealthBar.fillAmount <= 0f)
            {
                EnemyAgent.SetDestination(transform.position);
            }
        }
        else
        {
            EnemyAgent.SetDestination(transform.position);
        }

    }


}
