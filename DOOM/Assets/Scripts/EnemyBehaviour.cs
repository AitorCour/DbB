using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float iniLife;
    private float life;
    protected PlayerController player;
    public Animator animator;
    public float distance;
    public float attackDistance;
    public float speed;
    protected Vector3 iniPos;

    public bool canMove;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        iniPos = transform.position;
        canMove = false;
        animator.enabled = false;
        life = iniLife;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!canMove) return;
        distance = Vector3.Distance(player.transform.position, transform.position);
    }
    public void LoseLife(float damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Dead();
        }
    }
    private void Dead()
    {
        transform.position = iniPos;
        canMove = false;
        //this.enabled = false;
        animator.enabled = false;
        life = iniLife;
    }
}
