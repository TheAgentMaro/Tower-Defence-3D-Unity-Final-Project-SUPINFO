using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startMoney= 400;

    public static int lives;
    public int startLives = 20;

    public static int rounds;
    public static int waves;
    public int startWaves = 0;


    public void Start()
    {
        rounds = 0;
        waves = startWaves;
        money = startMoney;
        lives = startLives;
    }

    public static void IncrementWaves()
    {
        waves++;
    }
    public static void DecrementWaves()
    {
        waves--;
    }
}
