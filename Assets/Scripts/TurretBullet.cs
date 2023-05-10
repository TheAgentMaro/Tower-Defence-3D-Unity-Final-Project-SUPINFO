using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTurretBullet : MonoBehaviour
{
    public GameObject impactEffect;
    private Transform enemyTarget;
    public float playerSpeed = 70;
    public float Radius = 0f;
    public int takeDamage = 20;
    public bool type = false; //false = phys true = mag
    public void Seek(Transform _target)
    {
        enemyTarget = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = enemyTarget.position - transform.position;
        float distancePFrame = playerSpeed * Time.deltaTime;
        if(dir.magnitude<= distancePFrame)
        {
            Hit();
            return;
        }
        transform.Translate(dir.normalized * distancePFrame, Space.World);
        transform.LookAt(enemyTarget);

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
            damage(enemyTarget);
            
        }
        Destroy(gameObject);
    }
    void damage(Transform enemy)
    {
        GameEnemy e = enemy.GetComponent<GameEnemy>();
        if(e!= null)
        {
            e.TakeDammage(takeDamage, type);
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
            if(collider.tag== "gameEnemie")
            {
                damage(collider.transform);
            }
        }
    }
}
