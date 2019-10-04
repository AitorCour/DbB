using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : EnemyBehaviour
{
    public float shootDistance;
    public float fireRate;
    private bool canMove;
    private bool isShooting;

    private Ecanon canon;
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
        Move();

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
    }

    private void Move()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else return;
    }
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
}
