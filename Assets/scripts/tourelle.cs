using UnityEngine;

public class tourelle : MonoBehaviour
{
    public Transform target;

    public float range = 15f;
    public string enemyTag = "enemie";
    private float turnspd = 7f;
    public float atkspd = 1f;
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
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float selected = Mathf.Infinity;
        GameObject selectedE = null;

        foreach(GameObject enemie in ennemies)
        {
            float distanceToEnemie = Vector3.Distance(transform.position, enemie.transform.position);
            if(selected>distanceToEnemie)
            {
                selected = distanceToEnemie;
                selectedE = enemie;
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
        Vector3 rotation = Quaternion.Lerp(tourellePlaceHolder.rotation,lookRotation,Time.deltaTime*turnspd).eulerAngles;
        tourellePlaceHolder.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown<=0)
        {
            shoot();
            fireCountdown = 1 / atkspd;
        }
        fireCountdown -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    void shoot()
    {
       GameObject balleGO= Instantiate(bulletPrefab, canon.position, canon.rotation);
        balle Bullet = balleGO.GetComponent<balle>();
        if(Bullet != null)
        {
            Bullet.Seek(target);
        }
    }
}
