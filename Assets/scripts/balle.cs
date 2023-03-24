using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balle : MonoBehaviour
{
    public GameObject impactEffect;
    private Transform target;
    public float speed = 70;
    public float Radius = 0f;
    public int dgts = 20;
    public bool type = false; //false = phys true = mag
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target ==null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distancePFrame = speed * Time.deltaTime;
        if(dir.magnitude<= distancePFrame)
        {
            Hit();
            return;
        }
        transform.Translate(dir.normalized * distancePFrame, Space.World);
        transform.LookAt(target);

    }
    void Hit()
    {
       GameObject effectins= Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectins, 2f);
        if(Radius!=0)
        {
            explode();
        }
        else
        {
            damage(target);
            
        }
        Destroy(gameObject);
    }
    void damage(Transform enemy)
    {
        enemie e = enemy.GetComponent<enemie>();
        if(e!= null)
        {
            e.TakeDammage(dgts,type);
        }
        else
        {
            Debug.LogError("ERREUR, pas de script enemie!");
        }
    }
    void explode()
    {
      Collider[] colliders=  Physics.OverlapSphere(transform.position, Radius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag=="enemie")
            {
                damage(collider.transform);
            }
        }
    }
}
