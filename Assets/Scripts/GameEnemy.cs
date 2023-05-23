using UnityEngine;
using UnityEngine.UI;
public class GameEnemy : MonoBehaviour
{

    private bool playerDead = false;
    public float playerArmor = 10f;
    public float rm = 10f;
    public float playerSpeed = 10f;
    private float playerHealth;
    public float startplayerHealth = 100;
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
        target = EnemyMove.waypoints[0];
        playerHealth = startplayerHealth;
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * playerSpeed * Time.deltaTime,Space.World);
        
        if(Vector3.Distance(transform.position, target.position)<=0.2f)
        {
            GetNext();
        }
    }
    public void TakeDammage(int amount,bool type)
    {
        float damage;
        if(type)
        {
            damage = amount - rm;
        }
        else
        {
            damage = amount - playerArmor;
        }
        if(damage <0)
        {
            damage = 1;
        }
        playerHealth -= damage;
        HB.fillAmount = (playerHealth / startplayerHealth);
        if(playerHealth<=0 && !playerDead)
        {
            playerDead = true;
            Dead();
        }
    }
    private void Dead()
    {
        PlayerStats.money += enemievalue;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;
    }
    private void GetNext()
    {
        size = EnemyMove.waypoints.Length;
        waypointIndex++;
        
        if (waypointIndex >= size )
        {
            PlayerStats.lives--;
            WaveSpawner.enemiesAlive--;
            Destroy(gameObject);
            playerDead = true;
            return;
        }

        target = EnemyMove.waypoints[waypointIndex];
    }
}
