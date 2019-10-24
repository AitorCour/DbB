using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    // Start is called before the first frame update
    private float xVar;
    private float yVar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 20; i++)
        {
            xVar = Random.Range(-0.2f, 0.2f);
            yVar = Random.Range(-0.2f, 0.2f);
            Shot();
        }
    }

    void Shot()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen

        // actual Ray
        Vector3 ray = Camera.main.ViewportToWorldPoint(rayOrigin);
        Vector3 direction = Camera.main.transform.forward + new Vector3(xVar, yVar, 0);
        //RaycastHit hit = new RaycastHit();

            Debug.DrawRay(ray, direction, Color.blue, 3);
        
    }
}
