using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float maxDistance;
    public LayerMask mask;

    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float hitForce;
    public float hitDamage;

    public bool isShooting;
    public bool isReloading;
    public bool isShootingParticles;

    public float ReloadTime;

    private EnemyBehaviour targetEnemy;
    private ParticleSystem particles;
    //public Animator animacion;
	
	// Use this for initialization
	void Start ()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        isShooting = false;
        isReloading = false;
        currentAmmo = maxAmmo;
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void Shot()
    {

        if(isShooting || isReloading) return;
        if (currentAmmo <= 0)
        {
            ShootParticles();
            return;
        }

        //animacion.SetTrigger("shot2");
        Debug.Log("Shoooooot");
        isShooting = true;
        ShootParticles();
        currentAmmo--;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Coje el punto de la posicion del mouse y lanza un rayo
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit, maxDistance, mask))
        {
            Debug.Log(hit.transform.name);
            //Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.red, 1.0f);

            if(hit.rigidbody != null)
            {
                Debug.Log("No null");
                hit.rigidbody.AddForce(ray.direction * hitForce, ForceMode.Impulse);

                EnemyBehaviour target = hit.transform.gameObject.GetComponent<EnemyBehaviour>();
                
                targetEnemy = target;
                targetEnemy.LoseLife(hitDamage);
            }
        }
        StartCoroutine(WaitFireRate());
    }
    public void ShootParticles()
    {
        if (isReloading || currentAmmo <= 0 || !isShootingParticles)
        {
            particles.Stop();
        }
        else particles.Play();
    }
    private IEnumerator WaitFireRate() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = false;

       // yield return null;//cierra la corutina
    }

    public void Reload()
    {
        if(isReloading) return;
        isReloading = true;
        ShootParticles();
        //animacion.SetTrigger("recharge");

        //reload.SetTrigger ("reload");
        StartCoroutine(WaitForReload());
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(ReloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
