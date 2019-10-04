using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : EnemyBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
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
        agent.SetDestination(player.transform.position);
        if(distance <= attackDistance)
        {
            agent.isStopped = true;
        }
        else if(distance > attackDistance)
        {
            agent.isStopped = false;
        }
    }
}
