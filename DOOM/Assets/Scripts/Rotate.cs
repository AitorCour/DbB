using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedRot = Time.deltaTime * speed;
        transform.Rotate(Vector3.up * speedRot, Space.World);
    }
}
