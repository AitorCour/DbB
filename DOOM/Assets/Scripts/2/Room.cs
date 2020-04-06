using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private SpaceForRoom targetSpace;
    private Transform endRoom;
    public GameObject[] rooms; //0 = LR, 1 = LRB, 2 = LRT, 3 = LRBT
    public GameObject[] deadEnd;
    public int roomType;
    private int lenght = 15;
    public LayerMask layer;
    public LayerMask layer2;
    private bool setRoom;
    // Start is called before the first frame update
    void Start()
    {
        //SetPoints();
        if(roomType == 0)//LR
        {
            //SetPointRight();
            //SetPointLeft();
            StartCoroutine(WaitRight());
            StartCoroutine(WaitLeft());
        }
        if(roomType == 1)//LRB
        {
            //SetPointRight();
            //SetPointLeft();
            //SetPointBottom();
            StartCoroutine(WaitRight());
            StartCoroutine(WaitLeft());
            StartCoroutine(WaitTop());
        }
        if(roomType == 2)//LRT
        {
            //SetPointRight();
            //SetPointLeft();
            //SetPointTop();
            StartCoroutine(WaitRight());
            StartCoroutine(WaitLeft());
            StartCoroutine(WaitBottom());
        }
        if(roomType == 3)//LRBT
        {
            //SetPointRight();
            //SetPointLeft();
            //SetPointBottom();
            //SetPointTop();
            StartCoroutine(WaitRight());
            StartCoroutine(WaitLeft());
            StartCoroutine(WaitBottom());
            StartCoroutine(WaitTop());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        //Forward
        Vector3 directionF = transform.TransformDirection(Vector3.forward) * lenght;
        Gizmos.DrawRay(transform.position, directionF);
        //Backward
        Gizmos.color = Color.green;
        Vector3 directionB = transform.TransformDirection(Vector3.back) * lenght;
        Gizmos.DrawRay(transform.position, directionB);
        //Right
        Gizmos.color = Color.blue;
        Vector3 directionR = transform.TransformDirection(Vector3.right) * lenght;
        Gizmos.DrawRay(transform.position, directionR);
        //Left
        Gizmos.color = Color.red;
        Vector3 directionL = transform.TransformDirection(Vector3.left) * lenght;
        Gizmos.DrawRay(transform.position, directionL);
    }
    private void SetPointTop()
    {
        //Forward
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, lenght, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(2, rooms.Length);//si va hacia arriba: podria ser ahora LRT o LRBT
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.SetOcupped(rand);
                //StartCoroutine(WaitRoom());
            }
            else
            {
                //StopGeneration
                
                if (target.roomType == 0 || target.roomType == 1)
                {
                    SetEnd(Vector3.forward, 180f);
                }
                else return;
                setRoom = false;
            }
        }
        else
        {
            SetDeadEnd(Vector3.forward, 180f);
        }
    }
    private void SetPointRight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, lenght, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(0, rooms.Length);//puede ser cualquiera
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.SetOcupped(rand);
            }
            else
            {
                //StopGeneration
                //SetEnd(Vector3.right, 270f);
                setRoom = false;
            }
        }
        else
        {
            SetDeadEnd(Vector3.right, 270f);
        }
    }
    private void SetPointLeft()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, lenght, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(0, rooms.Length);//puede ser cualquiera
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.SetOcupped(rand);
            }
            else
            {
                //StopGeneration
                //SetEnd(Vector3.left, 90f);
                setRoom = false;
            }
        }
        else
        {
            SetDeadEnd(Vector3.left, 90f);
        }
    }
    private void SetPointBottom()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, lenght, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(1, rooms.Length);//si va hacia abajo: podria ser ahora LBT o LRBT
                if(rand == 2)
                {
                    rand = 3;
                }
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.SetOcupped(rand);
            }
            else if(targetSpace.ocupped == true)
            {
                //StopGeneration
                if (target.roomType == 0 || target.roomType == 2)
                {
                    SetEnd(Vector3.back, 0f);
                }
                else return;
                setRoom = false;
            }
        }
        else
        {
            SetDeadEnd(Vector3.back, 0f);
        }
    }
    private IEnumerator WaitRight() //Usar corutinas para contar tiempo
    {
        float randTime = Random.Range(0.2f, 3.0f);
        yield return new WaitForSeconds(randTime);
        SetPointRight();
    }
    private IEnumerator WaitLeft() //Usar corutinas para contar tiempo
    {
        float randTime = Random.Range(0.2f, 3.0f);
        yield return new WaitForSeconds(randTime);
        SetPointLeft();
    }
    private IEnumerator WaitBottom() //Usar corutinas para contar tiempo
    {
        float randTime = Random.Range(0.2f, 3.0f);
        yield return new WaitForSeconds(randTime);
        SetPointBottom();
    }
    private IEnumerator WaitTop() //Usar corutinas para contar tiempo
    {
        float randTime = Random.Range(0.2f, 3.0f);
        yield return new WaitForSeconds(randTime);
        SetPointTop();
    }
    private void SetDeadEnd(Vector3 vector, float rotation)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(vector), out hit, lenght, layer2))
        {
            Transform target = hit.transform.gameObject.GetComponent<Transform>();
            endRoom = target;
            Instantiate(deadEnd[0], endRoom.position, Quaternion.Euler(0, rotation, 0));
        }
    }
    private void SetEnd(Vector3 vector, float rotation)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(vector), out hit, lenght, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            Instantiate(deadEnd[0], targetSpace.transform.position, Quaternion.Euler(0, rotation, 0));
        }
    }

}
