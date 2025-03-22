using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesign : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnRows;
    public ObstacleLoader obstacleLoader;
    public ObstacleWave obstacleWave;
    [Tooltip("是否开启生成"),SerializeField]
    public bool canSpawn = true;
    private List<ObstacleWave.WaveData> waveList;
    private Dictionary<ObstacleType,GameObject> obstaclePrefabDicts = new Dictionary<ObstacleType, GameObject>();
    private float timer;
    private int currentWaveIndex;
    private bool winFlag;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnRows.Add(transform.GetChild(i).transform);
        }

        if (obstacleWave is not null)
        {
            waveList = obstacleWave.waves;
            //排序
            waveList.Sort((a, b) => a.existTime.CompareTo(b.existTime));
        }

        if (obstacleLoader is not null)
        {
           var obstaclePrefab = obstacleLoader.obstacles;
           foreach (var obstacle in obstaclePrefab)
           {
               obstaclePrefabDicts.Add(obstacle.obstacleType,obstacle.obstaclePrefab);
           }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
           
            // 检查是否需要生成敌人
            while (currentWaveIndex < waveList.Count && timer >= waveList[currentWaveIndex].existTime)
            {
                // 获取当前波次
                ObstacleWave.WaveData currentWave = waveList[currentWaveIndex];

                // 生成敌人
                SpawnObstacle(currentWave.obstacleType,currentWave.rowIndex,currentWave.speed);

                // 移动到下一个波次
                currentWaveIndex++;
            }

            if (currentWaveIndex >= waveList.Count && winFlag == false)
            {
                MessageCenter.SendMessage(new CommonMessage
                {
                    intParam = 0,
                    content = null,
                },MESSAGE_TYPE.WIN);
                winFlag = true;
            }
        }
        
    }

    public void SpawnObstacle(ObstacleType type,ObstacleWave.RowIndex rowIndex,float speed = 1.0f)
    {
        if(type == ObstacleType.None) return;
        GameObject obj = Instantiate(obstaclePrefabDicts[type], spawnRows[(int)rowIndex].position, Quaternion.identity);
        obj.GetComponent<ObstacleMove>().SetMoveSpeed(speed);
    }
}
