using UnityEngine;
using UnityEngine.UI;

public class GameEnemyGTD : MonoBehaviour
{
    private bool playerDead = false;
    public float playerArmor = 10f;
    public float rm = 10f;
    public float playerSpeed = 10f;
    private float playerHealth;
    public float startplayerHealth = 100;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public int enemievalue = 50;

    public Image HB;

    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
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

    public void TakeDamage(int amount, bool type)
    {
        float damage;
        if (type)
        {
            damage = amount - rm;
        }
        else
        {
            damage = amount - playerArmor;
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
        PlayerStats.money += enemievalue;
        Destroy(gameObject);
        WaveSpawnerGTD.enemiesAlive--;
    }

    private void ReachEnd()
    {
        PlayerStats.lives--;
        WaveSpawnerGTD.enemiesAlive--;
        Destroy(gameObject);
        playerDead = true;
    }
}
