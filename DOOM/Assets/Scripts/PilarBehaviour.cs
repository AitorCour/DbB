using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PilarBehaviour : MonoBehaviour
{
    private PlayerController player;
    private Renderer render;
    public float iniLife;
    private float life;

    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        render = GetComponent<Renderer>();
        life = iniLife;
    }

    public void LoseLife(float damage)
    {
        if (isDead) return;
        life -= damage;
        if (life <= 0 && !isDead)
        {
            Dead();
            isDead = true;
        }
        // Create random rgb color
        //Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);

        // Change material color
        //MaterialPropertyBlock mat = new MaterialPropertyBlock();
        //render.GetPropertyBlock(mat);

        //mat.SetColor("_Color", Color.red);

        //render.SetPropertyBlock(mat);
        render.material.DOColor(Color.red, 0.2f);
        StartCoroutine(WaitForWhiteColor());
    }
    private IEnumerator WaitForWhiteColor()
    {
        yield return new WaitForSeconds(0.1f);
        /*MaterialPropertyBlock mat = new MaterialPropertyBlock();
        render.GetPropertyBlock(mat);

        mat.SetColor("_Color", Color.white);

        render.SetPropertyBlock(mat);*/
        render.material.DOColor(Color.white, 0.2f);
    }
    private void Dead()
    {
        this.enabled = false;
    }
}
