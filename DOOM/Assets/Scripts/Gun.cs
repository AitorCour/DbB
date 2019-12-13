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

    private float xVar;
    private float yVar;

    public bool isShooting;
    public bool isReloading;
    public bool isShootingParticles;
    private bool shootgun;
    private bool revolver;
    private bool miniGun;
    private bool machineGun;
    private bool uzi;

    public float reloadTime;

    private EnemyBehaviour targetEnemy;
    private PilarBehaviour targetPilar;
    private ParticleSystem particlesShootGun;
    private ParticleSystem particlesRevolver;
    private ParticleSystem particlesMiniGun;
    private ParticleSystem particlesUzi;
    private ParticleSystem particlesMachine;
    //private Animator animator;
    ParticleSystem.MainModule psMain;
    ParticleSystem.ShapeModule psShape;
    ParticleSystem.EmissionModule psEmission;

    public int maxBlood;
    public GameObject bloodPrefab;
    public Transform bloodTransform;
    public ParticleSystem[] particleBlood;
    private int currentBlood = 0;

    private GameObject shootGunOBJ;
    private GameObject revolverOBJ;
    private GameObject miniGunOBJ;
    private GameObject uziOBJ;
    private GameObject machineGunOBJ;
    //public Animator animacion;

    // Use this for initialization
    void Start ()
    {
        particlesShootGun = GameObject.FindGameObjectWithTag("ShootGun").GetComponentInChildren<ParticleSystem>();
        particlesRevolver = GameObject.FindGameObjectWithTag("Revolver").GetComponentInChildren<ParticleSystem>();
        //particlesMiniGun = GameObject.FindGameObjectWithTag("MiniGun").GetComponentInChildren<ParticleSystem>();
        particlesUzi = GameObject.FindGameObjectWithTag("Uzi").GetComponentInChildren<ParticleSystem>();
        particlesMachine = GameObject.FindGameObjectWithTag("MachineGun").GetComponentInChildren<ParticleSystem>();
        shootGunOBJ = GameObject.FindGameObjectWithTag("ShootGun");
        revolverOBJ = GameObject.FindGameObjectWithTag("Revolver");
        //miniGunOBJ = GameObject.FindGameObjectWithTag("MiniGun");
        uziOBJ = GameObject.FindGameObjectWithTag("Uzi");
        machineGunOBJ = GameObject.FindGameObjectWithTag("MachineGun");
        //animator = GetComponentInChildren<Animator>();
        /*psMain = particles.main;
        psShape = particles.shape;
        psEmission = particles.emission;*/
        isShooting = false;
        isReloading = false;
        currentAmmo = maxAmmo;
        SetRevolver();
        CreateBlood();
	}

    public void Shot()
    {
        if (isShooting || isReloading || currentAmmo <= 0)
        {
            return;
        }
        if(revolver)
        {
            particlesRevolver.Play();
        }
        else if(miniGun)
        {
            particlesMiniGun.Play();
        }
        else if(machineGun)
        {
            particlesMachine.Play();
        }
        else if(uzi)
        {
            particlesUzi.Play();
        }
        else
        {
            particlesShootGun.Play();
        }

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
                particleBlood[currentBlood].transform.position = hit.point;
                particleBlood[currentBlood].time = 0;
                particleBlood[currentBlood].Play();
                currentBlood++;
                if (currentBlood >= maxBlood) currentBlood = 0;
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
    public void SetMachineGun()
    {
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        fireRate = 0.2f;
        maxDistance = Mathf.Infinity;
        hitForce = 0.5f;
        hitDamage = 0.5f;
        reloadTime = 2f;
        //
        shootgun = false;
        revolver = false;
        miniGun = false;
        machineGun = true;
        uzi = false;
        SetWeaponObjects();
    }
    public void SetUzi()
    {
        maxAmmo = 50;
        currentAmmo = maxAmmo;
        fireRate = 0.1f;
        maxDistance = Mathf.Infinity;
        hitForce = 0.5f;
        hitDamage = 0.5f;
        reloadTime = 2f;
        //
        shootgun = false;
        revolver = false;
        miniGun = false;
        machineGun = false;
        uzi = true;
        SetWeaponObjects();
    }
    public void SetShootGun()
    {
        maxAmmo = 5;
        currentAmmo = maxAmmo;
        fireRate = 1f;
        maxDistance = 20;
        hitForce = 5f;
        hitDamage = 10;
        reloadTime = 4f;
        //Particles
        //particles.transform.parent = shootGunOBJ.transform;
        //particles.transform.position = new Vector3(-0.8f, 0, 0);
        /*psShape.angle = 45;
        psMain.duration = 0.7f;
        psEmission.rateOverTime = 80;*/
        //Bools
        shootgun = true;
        revolver = false;
        miniGun = false;
        machineGun = false;
        uzi = false;
        SetWeaponObjects();
    }
    public void SetRevolver()
    {
        maxAmmo = 9;
        currentAmmo = maxAmmo;
        fireRate = 0.2f;
        maxDistance = Mathf.Infinity;
        hitForce = 1f;
        hitDamage = 2;
        reloadTime = 1f;
        shootgun = false;
        //Particles
        //particles.transform.parent = revolverOBJ.transform;
        /*psShape.angle = 5.5f;
        psMain.duration = 0.5f;
        psEmission.rateOverTime = 40;*/
        //Bools
        shootgun = false;
        revolver = true;
        miniGun = false;
        machineGun = false;
        uzi = false;
        SetWeaponObjects();
    }
    public void SetMiniGun()
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
        //particles.transform.parent = miniGunOBJ.transform;
        /*psShape.angle = 5.5f;
        psMain.duration = 0.5f;
        psEmission.rateOverTime = 40;*/
        //Bools
        shootgun = false;
        revolver = false;
        miniGun = true;
        machineGun = false;
        uzi = false;
        SetWeaponObjects();
    }
    void SetWeaponObjects()
    {
        //animator.SetBool("Revolver", false);
        //animator.SetBool("ShootGun", false);
        //animator.SetBool("MiniGun", false);
        if (revolver && !shootgun && !miniGun)
        {
            revolverOBJ.SetActive(true);
            shootGunOBJ.SetActive(false);
            machineGunOBJ.SetActive(false);
            uziOBJ.SetActive(false);
            //miniGunOBJ.SetActive(false);
            //animator.SetBool("Revolver", true);
        }
        else if (!revolver && shootgun && !miniGun)
        {
            revolverOBJ.SetActive(false);
            shootGunOBJ.SetActive(true);
            machineGunOBJ.SetActive(false);
            uziOBJ.SetActive(false);
            //miniGunOBJ.SetActive(false);
            //animator.SetBool("ShootGun", true);
            Debug.Log("Escopeta");
        }
        else if (!revolver && !shootgun && miniGun)
        {
            revolverOBJ.SetActive(false);
            shootGunOBJ.SetActive(false);
            machineGunOBJ.SetActive(false);
            uziOBJ.SetActive(false);
            //miniGunOBJ.SetActive(true);
            //animator.SetBool("MiniGun", true);
        }
        else if (machineGun)
        {
            revolverOBJ.SetActive(false);
            shootGunOBJ.SetActive(false);
            machineGunOBJ.SetActive(true);
            uziOBJ.SetActive(false);
            //miniGunOBJ.SetActive(true);
            //animator.SetBool("MiniGun", true);
        }
        else if (uzi)
        {
            revolverOBJ.SetActive(false);
            shootGunOBJ.SetActive(false);
            machineGunOBJ.SetActive(false);
            uziOBJ.SetActive(true);
            //miniGunOBJ.SetActive(true);
            //animator.SetBool("MiniGun", true);
        }
        else if (!revolver && !shootgun && miniGun)
        {
            revolverOBJ.SetActive(false);
            shootGunOBJ.SetActive(false);
            machineGunOBJ.SetActive(false);
            uziOBJ.SetActive(false);
            //miniGunOBJ.SetActive(true);
            //animator.SetBool("MiniGun", true);
        }
        else
        {
            Debug.Log("Hacha LOL");
        }
    }
    #endregion
}
