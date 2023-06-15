using UnityEngine;

public class gameTurretBullet : MonoBehaviour
{
    public GameObject impactEffect;
    private Transform enemyTarget;
    public float playerSpeed = 70;
    public float Radius = 0f;
    public int takeDamage = 20;
    public bool type = false; 
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
            Explode();
        }
        else
        {
            Damage(enemyTarget);
            
        }
        Destroy(gameObject);
    }
    void Damage(Transform enemy)
    {
        GameEnemy e = enemy.GetComponent<GameEnemy>();
        GameEnemyGTD enemyGTD = enemy.GetComponent<GameEnemyGTD>();
        GameEnemyGCTD enemyGCTD = enemy.GetComponent<GameEnemyGCTD>();
        if (enemyGTD != null)
        {
            enemyGTD.TakeDamage(takeDamage, type);
        }
        if (enemyGCTD != null)
        {
            enemyGCTD.TakeDamage(takeDamage, type);
        }
        if (e!= null )
        {
            e.TakeDammage(takeDamage, type);
        }
    }
    void Explode()
    {
      Collider[] colliders=  Physics.OverlapSphere(transform.position, Radius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag== "gameEnemie")
            {
                Damage(collider.transform);
            }
        }
    }
}
