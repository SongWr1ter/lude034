using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacleArea : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ObstacleMove obstacleMove))
        {
            obstacleMove.Despawn();
        }
    }
}
