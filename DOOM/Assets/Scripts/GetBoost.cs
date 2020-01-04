using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBoost : MonoBehaviour
{
    private PlayerController player;
    private GameManager gameManager;
    public int boost;
    public int maxBoosts;
    public GameObject[] mesh;
    private MeshRenderer child;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        child = GetComponentInChildren<MeshRenderer>();
        child.enabled = false;
        boost = Random.Range(0, 3);
        Debug.Log(boost);
        SetBoost();
    }
    private void SetBoost()
    {
        Quaternion rot = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        for (int i = 0; i < maxBoosts; i++)
        {
            if (i == boost)
            {
                GameObject b = Instantiate(mesh[i], transform.position, /*Quaternion.identity*/ rot, transform);
                b.name = "boost_" + i;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //iniSpeed = 6 // iniJump = 15;
            if (boost == 0)//Quick
            {
                player.SetSpeed(12, 30);
            }
            else if (boost == 1)//Slow
            {
                player.SetSpeed(3, 6);
            }
            else//Slow All
            {
                gameManager.SetTime(0.5f);
            }
        }
    }
}

