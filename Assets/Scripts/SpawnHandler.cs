using System;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject Ball;
    public Transform SpawnPos;
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    public void SpawnBall()
    {
        var go = Instantiate(Ball, SpawnPos.position, Quaternion.identity);
        Destroy(go, 1f);
    }
}
