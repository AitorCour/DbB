using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    private bool ocupped;
    public int rotation;
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        ocupped = false;
        GenerateObjects();
    }
    private void GenerateObjects()
    {
        if(!ocupped)
        {
            int rand = Random.Range(0, objects.Length);
            Instantiate(objects[rand], transform.position, Quaternion.Euler(0, rotation, 0));

            ocupped = true;
        }
    }
}
