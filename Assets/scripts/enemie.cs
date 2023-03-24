using UnityEngine;
using UnityEngine.UI;
public class enemie : MonoBehaviour
{

    private bool mort = false;

    public float armor = 10f;
    public float rm = 10f;
    public float speed = 10f;
    private float health;
    public float starthealth = 100;
    private Transform target;
    [SerializeField]
    private int waypointIndex = 0;
    [SerializeField]
    private int size = 0;
    [SerializeField]
    public int enemievalue = 50;

    public Image HB;
    void Start()
    {
        target = move.points[0];
        health = starthealth;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime,Space.World);
        
        if(Vector3.Distance(transform.position, target.position)<=0.2f)
        {
            GetNext();
        }
    }
    public void TakeDammage(int amount,bool type)
    {
        float dmg;
        if(type)
        {
            dmg = amount - rm;
        }
        else
        {
            dmg= amount - armor;
        }
        if(dmg<0)
        {
            dmg = 1;
        }
        health -= dmg;
        HB.fillAmount = (health / starthealth);
        if(health<=0 && !mort)
        {
            mort = true;
            dead();
        }
    }
    private void dead()
    {
        playerstats.money += enemievalue;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;
    }
    private void GetNext()
    {
        size = move.points.Length;
        waypointIndex++;
        
        if (waypointIndex >= size )
        {
            playerstats.lives--;
            WaveSpawner.enemiesAlive--;
            Destroy(gameObject);
            mort = true;
            return;
        }

        target = move.points[waypointIndex];
    }
}
