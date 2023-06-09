using UnityEngine;
using UnityEngine.UI;

public class GameEnemyGCTD : MonoBehaviour
{
    private bool playerDead = false;
    public float playerArmor = 10f;
    public float playerSpeed = 10f;
    private float playerHealth;
    public float startplayerHealth = 100;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public int enemyValue = 50;

    public Image HB;

    public WaveSpawnerGCTD spawner;

    public void SetSpawner(WaveSpawnerGCTD spawner)
    {
        this.spawner = spawner;
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
        transform.position = waypoints[currentWaypointIndex].position;
    }

    public void SetSpawnIndex(int index)
    {
        currentWaypointIndex = index;
        transform.position = waypoints[currentWaypointIndex].position;
    }

    void Start()
    {
        playerHealth = startplayerHealth;
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned for enemy movement.");
            return;
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 dir = targetWaypoint.position - transform.position;
        transform.Translate(dir.normalized * playerSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.2f)
        {
            currentWaypointIndex++;
        }
    }

    public void TakeDamage(float amount, bool type)
    {
        float damage = amount;

        if (!type) // Only subtract armor if type is false
        {
            damage -= playerArmor;
        }

        if (damage < 0)
        {
            damage = 1;
        }

        playerHealth -= damage;
        HB.fillAmount = (playerHealth / startplayerHealth);

        if (playerHealth <= 0 && !playerDead)
        {
            playerDead = true;
            Dead();
        }
    }
    private void Dead()
    {
        PlayerStats.money += enemyValue;
        Destroy(gameObject);
        WaveSpawnerGCTD.enemiesAlive--;
    }

    private void ReachEnd()
    {
        if (!playerDead)
        {
            playerDead = true; 
        }
        WaveSpawnerGCTD.enemiesAlive--;
        Destroy(gameObject);
    }
}