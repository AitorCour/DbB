using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    protected NavMeshAgent agent;
    public float iniLife;
    protected float life;
    protected PlayerController player;
    private CapsuleCollider colliderEnemy;
    public Animator animator;
    //public List<Collider> ragdoll = new List<Collider>();
    public float distance;
    public float distancePath;
    public float attackDistance;
    public float speed;
    protected Vector3 iniPos;

    public bool canMove;
    protected bool isDead;
    protected bool playerDetected;
    protected bool followingPlayer;
    public bool isInUse;
    private int deadCount;
    protected int radius = 5;

    public GameObject[] path;
    protected Transform[] transformPath;
    public GameObject newPoint;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        path = GameObject.FindGameObjectsWithTag("Path");
        animator = GetComponentInChildren<Animator>();
        colliderEnemy = GetComponent<CapsuleCollider>();
        //SetRagdollParts();
        iniPos = transform.position;
        canMove = false;
        animator.enabled = false;
        life = iniLife;
        isDead = false;
        deadCount = 1;

        transformPath = new Transform[path.Length];
        for(int i = 0; i < path.Length; i++)
        {
            transformPath[i] = path[i].GetComponent<Transform>();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!canMove) return;
        if (isDead) return;
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance < radius)
        {
            playerDetected = true;
            followingPlayer = true;
        }
        else
        {
            playerDetected = false;
        }
    }
    public void LoseLife(float damage)
    {
        if (isDead) return;
        life -= damage;
        animator.SetTrigger("Damage");
        if(life <= 0 && !isDead)
        {
            Dead();
            isDead = true;
        }
    }
    protected virtual void Dead()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetTrigger("Dead");
        animator.SetBool("Death", true);
        colliderEnemy.enabled = false;
        //animator.enabled = false;
        canMove = false;
        //TurnOnRagdoll();
        deadCount += 1;
        StartCoroutine(WaitForResetEnemy());
        int value = Random.Range(0, 100);
        if(value > 60 && value < 70)
        {
            Debug.Log("ShootGun");
        }
        else if (value > 50 && value < 60)
        {
            Debug.Log("Axe");
        }
        else if (value > 95 && value < 99)
        {
            Debug.Log("MiniGun");
        }
    }
    private IEnumerator WaitForResetEnemy()
    {
        yield return new WaitForSeconds(3);
        ResetEnemy();
    }
    private void ResetEnemy()
    {
        transform.position = iniPos;
        colliderEnemy.enabled = true;
        canMove = false;
        isDead = false;
        animator.SetLayerWeight(1, 0.5f);
        animator.SetBool("Death", false);
        animator.SetTrigger("Reset");
        //this.enabled = false;
        animator.enabled = false;
        life = iniLife;
        isInUse = false;
        playerDetected = false;
        followingPlayer = false;
        //ResetBuf();
    }
    void ResetBuf()
    {
        if (deadCount >= 5) deadCount = 5;
        transform.localScale = new Vector3(deadCount, deadCount, deadCount);
        speed += deadCount;
        life *= deadCount;
    }
    public virtual void ActiveNavmesh()
    {
        Debug.Log("First Step to work");
    }
    /*protected virtual void GoNearestPath()
    {
        Debug.Log("1st Path");
        int i = Random.Range(0, path.Length);
        //for(int d = 0; d < path.Lenght, d++)
        //distance = Vector3.Distance(path[i].position, transform.position);
        //path[i] = newPoint;
    }*/
}
