﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : EnemyBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private bool isAttacking;
    public float fistRate;
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //if (isDead) return;
        agent.SetDestination(player.transform.position);
        if (distance <= attackDistance)
        {
            agent.isStopped = true;
            Attack();

        }
        else if (distance > attackDistance && !isAttacking && canMove)
        {
            agent.isStopped = false;
        }
        else agent.isStopped = true;

        if(agent.isStopped)
        {
            animator.SetBool("Walking", false);
        }
        else if(!agent.isStopped)
        {
            animator.SetBool("Walking", true);
        }
    }
    private void Attack()
    {
        if (isAttacking) return;
        if (isDead) return;
        Debug.Log("Shot");
        animator.SetTrigger("Punch");
        isAttacking = true;
        StartCoroutine(WaitFistRate());
    }
    private IEnumerator WaitFistRate() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(fistRate);
        isAttacking = false;

        // yield return null;//cierra la corutina
    }
}
