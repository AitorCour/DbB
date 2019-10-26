using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TweenAnims : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ChangeColor();
    }

    // Update is called once per frame
    void ChangeColor()
    {
        mainCamera.backgroundColor = new Color(214/255f, 255/255f, 155/255f, 1);
        mainCamera.DOColor(new Color(123 / 255f, 16 / 255f, 0 / 255f, 1), 10f).SetLoops(-1, LoopType.Yoyo);
        //Hacer un ramdom range maybe.
    }
}
