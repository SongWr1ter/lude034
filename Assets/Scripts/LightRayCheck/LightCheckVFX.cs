using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheckVFX : MonoBehaviour
{
    [SerializeField, Tooltip("伤害延迟时间")] private float delayTime;
    [SerializeField, Tooltip("回复时间")] private float recoverSpeed;
    private float timer;
    private SpriteRenderer spriteRenderer;
    private LightCheck lightCheck;
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        spriteRenderer.color = new Color(originalColor.r,originalColor.g,originalColor.b, 1f);
        lightCheck = GetComponent<LightCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!lightCheck.IsInLight())
        {
            timer += Time.deltaTime;
            //VFX
            float ratio = timer / delayTime;
            spriteRenderer.color = new Color(originalColor.r,originalColor.g,originalColor.b, 1f - ratio);
            //Event
            if (timer >= delayTime)
            {
                timer = 0;
                MessageCenter.SendMessage(new CommonMessage
                {
                    content = null,
                    intParam = 0,
                },MESSAGE_TYPE.GET_HURT);
            }
        }
        else
        {
            if(timer >= 0f)
                timer -= Time.deltaTime * recoverSpeed;
            float ratio = timer / delayTime;
            spriteRenderer.color = new Color(originalColor.r,originalColor.g,originalColor.b, 1 - ratio);
        }
    }
}
