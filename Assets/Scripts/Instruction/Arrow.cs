using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("动画设置")]
    [SerializeField,Tooltip("移动距离")]
    private float distance;
    [SerializeField,Tooltip("移动速度")]
    private float speed;
    [SerializeField, Tooltip("是不是右箭头")] private bool right;
    private float startDistance;
    
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    [SerializeField,Tooltip("多久消失")]
    private float fadeTime;
    private float timer;
    [SerializeField,Tooltip("消失速度")]
    private float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startDistance = Vector3.Distance(transform.position, transform.parent.position);
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (right == true)
        {
            float h = Input.GetAxis("Horizontal");
            Vector3 startPos = transform.parent.position + Vector3.right * startDistance;
            Vector3 targetPos = startPos +  Vector3.right * distance;
            if (h > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            }
        }
        else
        {
            float h = Input.GetAxis("Horizontal");
            Vector3 startPos = transform.parent.position + Vector3.left * startDistance;
            Vector3 targetPos = startPos + Vector3.left * distance;
            if (h < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
            }
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= fadeTime)
        {
            spriteRenderer.color = Color.Lerp(startColor, Color.clear, fadeSpeed* (timer - fadeTime) / fadeTime);
        }
    }
}
