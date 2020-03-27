using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private SpaceForRoom targetSpace;
    public GameObject[] rooms; //0 = LR, 1 = LRB, 2 = LRT, 3 = LRBT
    public int roomType;
    public LayerMask layer;
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
        Vector3 directionF = transform.TransformDirection(Vector3.forward) * 10;
        Gizmos.DrawRay(transform.position, directionF);
        //Backward
        Gizmos.color = Color.green;
        Vector3 directionB = transform.TransformDirection(Vector3.back) * 10;
        Gizmos.DrawRay(transform.position, directionB);
        //Right
        Gizmos.color = Color.blue;
        Vector3 directionR = transform.TransformDirection(Vector3.right) * 10;
        Gizmos.DrawRay(transform.position, directionR);
        //Left
        Gizmos.color = Color.red;
        Vector3 directionL = transform.TransformDirection(Vector3.left) * 10;
        Gizmos.DrawRay(transform.position, directionL);
    }
    private void SetPointTop()
    {
        //Forward
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(2, rooms.Length);//si va hacia arriba: podria ser ahora LRT o LRBT
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.ocupped = true;
                //StartCoroutine(WaitRoom());
            }
            else
            {
                //StopGeneration
                setRoom = false;
            }
        }
        else return;
    }
    private void SetPointRight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 10, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(0, rooms.Length);//puede ser cualquiera
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.ocupped = true;
            }
            else
            {
                //StopGeneration
                setRoom = false;
            }
        }
        else return;
    }
    private void SetPointLeft()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 10, layer))
        {
            SpaceForRoom target = hit.transform.gameObject.GetComponent<SpaceForRoom>();
            targetSpace = target;
            if (targetSpace.ocupped == false)
            {
                //SetRoom
                int rand = Random.Range(0, rooms.Length);//puede ser cualquiera
                Instantiate(rooms[rand], targetSpace.transform.position, Quaternion.identity);

                setRoom = false;
                targetSpace.ocupped = true;
            }
            else
            {
                //StopGeneration
                setRoom = false;
            }
        }
        else return;
    }
    private void SetPointBottom()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 10, layer))
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
                targetSpace.ocupped = true;
            }
            else
            {
                //StopGeneration
                setRoom = false;
            }
        }
        else return;
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

}
