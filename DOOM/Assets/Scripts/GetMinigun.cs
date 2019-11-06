using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMinigun : MonoBehaviour
{
    private Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gun.SetMiniGun();
            Debug.Log("MiniGun");
        }
    }
}
