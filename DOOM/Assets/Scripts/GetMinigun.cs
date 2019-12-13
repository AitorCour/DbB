using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMinigun : MonoBehaviour
{
    private Gun gun;
    public int weapon;
    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        weapon = Random.Range(0, 4);
        Debug.Log(weapon);
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
