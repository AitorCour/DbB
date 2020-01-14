using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyBehaviour
{
    public float shootDistance;
    public float fireRate;
    private bool isShooting;

    private Ecanon canon;
    public int pathNum;
    private bool changePoint;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        canMove = true;
        canon = GetComponentInChildren<Ecanon>();
    }
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, player.transform.position);
    }*/
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (agent.enabled == false) return;
        //if (isDead) return;
        if (followingPlayer)
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
            else if (!changePoint)
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
            Shoot();

        }
        else if (distance > attackDistance && !isShooting && canMove)
        {
            agent.isStopped = false;
        }
        else agent.isStopped = true;

        if (agent.isStopped)
        {
            animator.SetBool("Walking", false);
        }
        else if (!agent.isStopped)
        {
            animator.SetBool("Walking", true);
        }
    }


    /*Move();

        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
        {
            Debug.DrawRay(transform.position, player.transform.position - transform.position);
        }
        if(distance <= shootDistance)
        {
            canMove = false;
            Shoot();
        }
        else if(distance > shootDistance)
        {
            canMove = true;
        }
    }*/

    /*private void Move()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else return;
    }*/
    private void Shoot()
    {
        if (isShooting) return;

        Debug.Log("Shot");
        isShooting = true;
        canon.ShotBullet(player.transform.position);
        StartCoroutine(WaitFireRate());
    }
    private IEnumerator WaitFireRate() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = false;

        // yield return null;//cierra la corutina
    }
    void GoNearestPath()
    {
        //base.GoNearestPath();
        int i = Random.Range(0, path.Length);
        pathNum = i;
    }
}
