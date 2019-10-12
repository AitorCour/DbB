using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float life;
    protected PlayerController player;
    protected Animator animator;
    public float distance;
    public float attackDistance;
    public float speed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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
        transform.position = new Vector3(0, 0, 0);

    }
}
