using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelDesign/ObstacleWave")]
public class ObstacleWave : ScriptableObject
{
    public enum RowIndex
    {
        Row1,
        Row2,
        Row3,
    }
    [Serializable]
    public struct WaveData
    {
        public RowIndex rowIndex;
        public ObstacleType obstacleType;
        public float speed;
        public float existTime;
    }
    public List<WaveData> waves = new List<WaveData>();
}
