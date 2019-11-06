﻿using System.Collections;
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

    private float xVar;
    private float yVar;

    public bool isShooting;
    public bool isReloading;
    public bool isShootingParticles;
    private bool shootgun;

    public float reloadTime;

    private EnemyBehaviour targetEnemy;
    private PilarBehaviour targetPilar;
    private ParticleSystem particles;
    ParticleSystem.MainModule psMain;
    ParticleSystem.ShapeModule psShape;
    ParticleSystem.EmissionModule psEmission;

    public int maxBlood;
    public GameObject bloodPrefab;
    public Transform bloodTransform;
    public ParticleSystem[] particleBlood;
    private int currentBlood = 0;
    //public Animator animacion;

    // Use this for initialization
    void Start ()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        psMain = particles.main;
        psShape = particles.shape;
        psEmission = particles.emission;
        isShooting = false;
        isReloading = false;
        currentAmmo = maxAmmo;
        SetMiniGun();
        CreateBlood();
	}

    public void Shot()
    {
        
        if (isShooting || isReloading || currentAmmo <= 0)
        {
            return;
        }
        particles.Play();

        isShooting = true;
        currentAmmo--;
        if (!shootgun)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Coje el punto de la posicion del mouse y lanza un rayo

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, maxDistance, mask))
            {
                //Debug.Log(hit.transform.name);
                //Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.red, 1.0f);

                if (hit.rigidbody != null && hit.rigidbody.tag == "Enemy")
                {
                    Debug.Log("No null");
                    hit.rigidbody.AddForce(ray.direction * hitForce, ForceMode.Impulse);

                    EnemyBehaviour target = hit.transform.gameObject.GetComponent<EnemyBehaviour>();

                    targetEnemy = target;
                    targetEnemy.LoseLife(hitDamage);
                    particleBlood[currentBlood].transform.position = hit.point;
                    particleBlood[currentBlood].time = 0;
                    particleBlood[currentBlood].Play();
                    currentBlood++;
                    if (currentBlood >= maxBlood) currentBlood = 0;
                }
                else if(hit.rigidbody != null && hit.rigidbody.tag == "Pilar")
                {
                    hit.rigidbody.AddForce(ray.direction * hitForce, ForceMode.Impulse);

                    PilarBehaviour target = hit.transform.gameObject.GetComponent<PilarBehaviour>();

                    targetPilar = target;
                    targetPilar.LoseLife(hitDamage);
                }
            }
        }
        else if (shootgun)
        {
            for (int i = 0; i < 20; i++)
            {
                xVar = Random.Range(-0.3f, 0.3f);
                yVar = Random.Range(-0.3f, 0.3f);
                ShotShotgun();
            }
        }
        StartCoroutine(WaitFireRate());
    }
    void ShotShotgun()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen

        // actual Ray
        Vector3 origin = Camera.main.ViewportToWorldPoint(rayOrigin);
        Vector3 direction = Camera.main.transform.forward + new Vector3(xVar, yVar, 0);
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        //RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, maxDistance, mask))
        {
            //Debug.Log(hit.transform.name);
            //Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.red, 1.0f);

            if (hit.rigidbody != null && hit.rigidbody.tag == "Enemy")
            {
                Debug.Log("No null");
                hit.rigidbody.AddForce(direction * hitForce, ForceMode.Impulse);

                EnemyBehaviour target = hit.transform.gameObject.GetComponent<EnemyBehaviour>();

                targetEnemy = target;
                targetEnemy.LoseLife(hitDamage);
            }
            else if (hit.rigidbody != null && hit.rigidbody.tag == "Pilar")
            {
                hit.rigidbody.AddForce(ray.direction * hitForce, ForceMode.Impulse);

                PilarBehaviour target = hit.transform.gameObject.GetComponent<PilarBehaviour>();

                targetPilar = target;
                targetPilar.LoseLife(hitDamage);
            }
        }
        Debug.DrawRay(origin, direction, Color.blue, 3);

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
        //ShootParticles();
        //animacion.SetTrigger("recharge");

        //reload.SetTrigger ("reload");
        StartCoroutine(WaitForReload());
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void CreateBlood()
    {
        particleBlood = new ParticleSystem[maxBlood];

        for (int i = 0; i < maxBlood; i++)
        {
            Vector3 spawnPos = bloodTransform.position;
            spawnPos.z -= i * 2;
            GameObject b = Instantiate(bloodPrefab, spawnPos, Quaternion.identity, bloodTransform);
            b.name = "Blood_" + i;
            particleBlood[i] = b.GetComponent<ParticleSystem>();
        }
    }
    #region Sets
    void SetMachineGun()
    {
        maxAmmo = 100;
        currentAmmo = maxAmmo;
        fireRate = 0.3f;
        maxDistance = Mathf.Infinity;
        hitForce = 0.5f;
        hitDamage = 0.5f;
        reloadTime = 2f;
        shootgun = false;
        //Particles
        psShape.angle = 5.5f;
        psMain.duration = 0.5f;
        psEmission.rateOverTime = 40;
    }
    void SetShootGun()
    {
        maxAmmo = 5;
        currentAmmo = maxAmmo;
        fireRate = 0.5f;
        maxDistance = 20;
        hitForce = 5f;
        hitDamage = 10;
        reloadTime = 4f;
        shootgun = true;
        //Particles
        psShape.angle = 45;
        psMain.duration = 0.7f;
        psEmission.rateOverTime = 80;
    }
    void SetRafagas()
    {
        maxAmmo = 20;
        currentAmmo = maxAmmo;
        fireRate = 0.1f;
        maxDistance = Mathf.Infinity;
        hitForce = 1f;
        hitDamage = 2;
        reloadTime = 1f;
        shootgun = false;
        //Particles
        psShape.angle = 5.5f;
        psMain.duration = 0.5f;
        psEmission.rateOverTime = 40;
    }
    void SetMiniGun()
    {
        maxAmmo = 300;
        currentAmmo = maxAmmo;
        fireRate = 0.3f;
        maxDistance = Mathf.Infinity;
        hitForce = 2f;
        hitDamage = 1;
        shootgun = false;
        reloadTime = 10f;
        //Particles
        psShape.angle = 5.5f;
        psMain.duration = 0.5f;
        psEmission.rateOverTime = 40;
    }
    #endregion
}
