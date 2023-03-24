using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstats : MonoBehaviour
{
    public static int money;
    public int startmoney=400;

    public static int lives;
    public int startlives = 20;

    public static int rounds;

    public void Start()
    {
        rounds = 0;
        money = startmoney;
        lives = startlives;
    }
}
