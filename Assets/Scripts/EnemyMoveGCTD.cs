﻿using UnityEngine;

public class EnemyMoveGCTD : MonoBehaviour
{
    public Transform[] waypoints; 

    // Start is called before the first frame update
    void Awake()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}