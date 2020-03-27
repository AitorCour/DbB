using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceForRoom : MonoBehaviour
{
    public bool ocupped;
    // Start is called before the first frame update
    void Start()
    {
        ocupped = false;
    }
    public void SetOcupped()
    {
        ocupped = true;
    }
}
