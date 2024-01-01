using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    private NavMeshAgent EnemyAgent;

    public Transform target;  // The target position to move towards
    //public float speed = 5.0f; // The movement speed


    public bool playerInAttackRange;
    public float attackRange;

    public LayerMask WhatIsPlayer;

    private void Start()
    {
        EnemyAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("MainPlayerTag").transform;
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (target != null)
        {
            EnemyAgent.SetDestination(target.position);
        }

        if (playerInAttackRange)
        {
            Attack();
        }
    }

    public void Attack()
    {
        EnemyAgent.SetDestination(transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
