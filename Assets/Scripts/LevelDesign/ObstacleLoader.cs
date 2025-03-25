using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObstacleType
{
    Square,
    Circle,
    Triangle,
    Rectangle,
    None,
}
[CreateAssetMenu( menuName = "LevelDesign/ObstacleLoader")]
public class ObstacleLoader : ScriptableObject
{
    [Serializable]
    public class Obstacle
    {
        public GameObject obstaclePrefab;
        public ObstacleType obstacleType;
    }
    public List<Obstacle> obstacles = new List<Obstacle>();
}
