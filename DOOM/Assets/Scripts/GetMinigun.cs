using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMinigun : MonoBehaviour
{
    private Gun gun;
    public int weapon;
    public int maxWeapons;
    public GameObject[] mesh;
    private MeshRenderer child;
    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        child = GetComponentInChildren<MeshRenderer>();
        child.enabled = false;
        weapon = Random.Range(0, 4);
        Debug.Log(weapon);
        SetWeapon();
    }
    private void SetWeapon()
    {
        Quaternion rot = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        for (int i = 0; i < maxWeapons; i++)
        {
            if(i == weapon)
            {
                GameObject b = Instantiate(mesh[i], transform.position, /*Quaternion.identity*/ rot, transform);
                b.name = "gun_" + i;
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(weapon == 0)
            {
                gun.SetMachineGun();
            }
            else if(weapon == 1)
            {
                gun.SetShootGun();
            }
            else if(weapon == 2)
            {
                gun.SetUzi();
            }
            else
            {
                gun.SetRevolver();
            }
        }
    }
}
