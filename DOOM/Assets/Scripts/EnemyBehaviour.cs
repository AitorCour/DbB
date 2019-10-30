using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float iniLife;
    private float life;
    protected PlayerController player;
    public Animator animator;
    //public List<Collider> ragdoll = new List<Collider>();
    public float distance;
    public float attackDistance;
    public float speed;
    protected Vector3 iniPos;

    public bool canMove;
    protected bool isDead;
    public bool isInUse;
    private int deadCount;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        //SetRagdollParts();
        iniPos = transform.position;
        canMove = false;
        animator.enabled = false;
        life = iniLife;
        isDead = false;
        deadCount = 1;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!canMove) return;
        if (isDead) return;
        distance = Vector3.Distance(player.transform.position, transform.position);
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
    private void Dead()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetTrigger("Dead");
        //animator.enabled = false;
        canMove = false;
        //TurnOnRagdoll();
        deadCount += 1;
        StartCoroutine(WaitForResetEnemy());
    }
    private IEnumerator WaitForResetEnemy()
    {
        yield return new WaitForSeconds(3);
        ResetEnemy();
    }
    private void ResetEnemy()
    {
        transform.position = iniPos;
        canMove = false;
        isDead = false;
        animator.SetLayerWeight(1, 0.5f);
        animator.SetTrigger("Reset");
        //this.enabled = false;
        animator.enabled = false;
        life = iniLife;
        isInUse = false;
        ResetBuf();
    }
    void ResetBuf()
    {
        if (deadCount >= 5) deadCount = 5;
        transform.localScale = new Vector3(deadCount, deadCount, deadCount);
        speed += deadCount;
        life *= deadCount;
    }
}
