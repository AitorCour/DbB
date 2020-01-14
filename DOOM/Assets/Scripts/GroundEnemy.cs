using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : EnemyBehaviour
{
    // Start is called before the first frame update
    //private NavMeshAgent agent;
    private BoxCollider[] fists;
    public bool isAttacking;
    public float fistRate;
    public int pathNum;
    private bool changePoint;
    protected override void Start()
    {
        base.Start();
        /*agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.enabled = false;
        changePoint = true;*/
        //GoNearestPath();
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
        if(followingPlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (changePoint)
            {
                GoNearestPath();
                Debug.Log("newPath");
                changePoint = false;
            }
            else if(!changePoint)
            {
                agent.SetDestination(path[pathNum].transform.position);
                Debug.Log(pathNum);
                distancePath = Vector3.Distance(path[pathNum].transform.position, transform.position);
                if (distancePath <= 2)
                {
                    changePoint = true;
                }
            }
        }

        if (distance <= attackDistance)
        {
            agent.isStopped = true;
            Attack();

        }
        else if (distance > attackDistance && !isAttacking && canMove )
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
        Debug.Log("corrutina");
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
    void GoNearestPath()
    {
        //base.GoNearestPath();
        int i = Random.Range(0, path.Length);
        pathNum = i;
    }


}
