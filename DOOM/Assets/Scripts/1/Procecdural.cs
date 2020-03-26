using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procecdural : MonoBehaviour
{
    public Transform[] startPos;
    public GameObject[] rooms; //0 = LR, 1 = LRB, 2 = LRT, 3 = LRBT

    private int direction;
    public float moveAmount;

    private bool setRoom;
    private bool stopGeneration;

    public float minX;
    public float maxX;
    public float minZ;

    public LayerMask room;
    // Start is called before the first frame update
    void Start()
    {
        setRoom = true;
        stopGeneration = false;
        int randStarPos = Random.Range(0, startPos.Length); //punto inicio random
        transform.position = startPos[randStarPos].position; //pone la primera room en ese punto
        Instantiate(rooms[0], transform.position, Quaternion.identity); //instancia la habitacion. el[0] deberá ser un random, ahora seria la primera del array

        direction = Random.Range(1, 7);
        StartCoroutine(WaitRoom());
        setRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        if (!setRoom || stopGeneration) return;
        if(direction == 1 || direction == 2) //Right
        {
            if(transform.position.x < maxX)
            {
                Vector3 newPos = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 7);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 3 || direction == 4) //Left
        {
            if(transform.position.x > minX)
            {
                Vector3 newPos = new Vector3(transform.position.x - moveAmount, transform.position.y, transform.position.z);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 7);
            }
            else
            {
                direction = 5;
            }
            
        }
        else if (direction == 5 || direction == 6) //Down
        {
            if(transform.position.z < minZ)
            {
                Collider[] roomDetection = Physics.OverlapSphere(transform.position, 10, room);

                int i = 0;
                if(roomDetection[i].GetComponent<RoomType>().type != 1 && roomDetection[i].GetComponent<RoomType>().type != 3)
                {
                    roomDetection[i].GetComponent<RoomType>().RoomDestruction();

                    i += 1;

                    int randBottomRoom = Random.Range(1, 4);
                    if(randBottomRoom == 2)
                    {
                        randBottomRoom = 1;
                    }
                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                }

                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 7);
            }
            else //Stop
            {
                stopGeneration = true;
            }
        }
        //Instantiate(rooms[0], transform.position, Quaternion.identity);
        //direction = Random.Range(1, 7);
        StartCoroutine(WaitRoom());
        setRoom = false;
    }
    private IEnumerator WaitRoom() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(0.2f);
        setRoom = true;
        Move();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}

