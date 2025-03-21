using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour
{
    public List<Transform> lightCheckPoints;

    public Transform lightSource;
    [Tooltip("障碍物所在的Mask")]
    public LayerMask layerMask;
    [Tooltip("是否开启检测")]
    public bool checkLight;
    [Tooltip("是否显示射线")]
    public bool debug;
    [SerializeField]
    private bool _inLight;//是否在光源下面

    private void Start()
    {
        _inLight = true;
    }

    private void FixedUpdate()
    {
        if (checkLight)
        {
            _inLight = CheckInLight();
        }
        else
        {
            _inLight = true;
        }
        
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Vector3 dir = transform.position - lightSource.position;
            Color color;
            if (_inLight)
            {
                color = Color.yellow;
            }
            else
            {
                color = Color.red;
            }
            Debug.DrawRay(lightSource.position, dir,color);
            
            foreach (var point in lightCheckPoints)
            {
                dir = point.position - lightSource.position;
                RaycastHit2D hit = Physics2D.Raycast(lightSource.position, dir, float.MaxValue, layerMask);
                //一但有一点被挡住
                if (hit.collider is not null)
                {
                    Debug.DrawRay(lightSource.position, dir,Color.red);
                }
                else
                {
                    Debug.DrawRay(lightSource.position, dir,Color.yellow);
                }
            }
        }
    }

    private bool CheckInLight()
    {
        bool val = true;
        foreach (var point in lightCheckPoints)
        {
            Vector3 dir = point.position - lightSource.position;
            dir = dir.normalized;
            RaycastHit2D hit = Physics2D.Raycast(lightSource.position, dir, float.MaxValue, layerMask);
            //一但有一点被挡住
            if (hit.collider is not null)
            {
                val = false;
                break;
            }
        }
        return val;
    }

    public bool IsInLight()
    {
        return _inLight;
    }
}
