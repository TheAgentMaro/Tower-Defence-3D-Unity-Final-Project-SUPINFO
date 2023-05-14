using UnityEngine;

public class GameTurret : MonoBehaviour
{
    public Transform target;

    public float range = 15f;
    public string enemyTag = "gameEnemie";
    private float turnSpeed = 7f;
    public float attackSpeed = 1f;
    private float fireCountdown = 0;
    public Transform tourellePlaceHolder;

    public GameObject bulletPrefab;
    public Transform canon;




    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    void UpdateTarget()
    {
        GameObject[] gameEnemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float selected = Mathf.Infinity;
        GameObject selectedE = null;

        foreach(GameObject gameEnemie in gameEnemies)
        {
            float distanceToEnemie = Vector3.Distance(transform.position, gameEnemie.transform.position);
            if(selected>distanceToEnemie)
            {
                selected = distanceToEnemie;
                selectedE = gameEnemie;
            }
        }
        if(selectedE!=null && selected<= range)
        {
            target = selectedE.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tourellePlaceHolder.rotation,lookRotation,Time.deltaTime*turnSpeed).eulerAngles;
        tourellePlaceHolder.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown<=0)
        {
            Shoot();
            fireCountdown = 1 / attackSpeed;
        }
        fireCountdown -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void Shoot()
    {
       GameObject balleGO= Instantiate(bulletPrefab, canon.position, canon.rotation);
        gameTurretBullet Bullet = balleGO.GetComponent<gameTurretBullet>();
        if(Bullet != null)
        {
            Bullet.Seek(target);
        }
    }
}
