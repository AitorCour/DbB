using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : EnemyBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private BoxCollider[] fists;
    private bool isAttacking;
    public float fistRate;
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.enabled = false;
        fists = GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider collider in fists)
        {
            collider.enabled = false;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (agent.enabled == false ) return;
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
        if(isAttacking)
        {
            foreach (BoxCollider collider in fists)
            {
                collider.enabled = true;
            }
        }
        else
        {
            foreach (BoxCollider collider in fists)
            {
                collider.enabled = false;
            }
        }
    }
    private void Attack()
    {
        if (isAttacking) return;
        if (isDead) return;
        //Debug.Log("Shot");
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
    private void OnTriggerEnter(Collider other)
    {
        if(isAttacking)
        {
            if (other.tag == "Player")
            {
                player.LoseLife(1);
            }
        }
    }
    public override void ActiveNavmesh()
    {
        StartCoroutine(WaitMesh());
    }
    private IEnumerator WaitMesh() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(5);
        agent.enabled = true;

        // yield return null;//cierra la corutina
    }
}
