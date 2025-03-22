using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField,Tooltip("速度")]
    private float moveSpeed;
    private Rigidbody2D rb2d;

    [SerializeField, Tooltip("血量")] 
    public float maxHp;

    [Header("特效Prefab")] 
    public GameObject HurtVFX;
    public GameObject DieVFX;

    private float currentHp;
    [SerializeField,Tooltip("每Hp对应的尺寸")]private float[] Hp2size = new[] { 0.5f, 0.5f, 0.667f,1.0f };
    private CommonMessage HurtMessage;
    
    private float GetHurt_TimeInterval = 0.5f;
    private float GetHurtTimer = 0.0f;

    public bool debug;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        MessageCenter.AddListener(GetHurt,MESSAGE_TYPE.GET_HURT);
        currentHp = maxHp;
    }
    void GetHurt(CommonMessage mag)
    {
        if (GetHurtTimer > 0.0f)
        {
            return;
        }
        currentHp -= mag.intParam;
        GetHurtTimer = GetHurt_TimeInterval;
        
        //整体变小
        float size = Hp2size[Mathf.Clamp(Mathf.RoundToInt(currentHp),0,Hp2size.Length-1)];
        transform.localScale = new Vector3(size, size, 1f);
        if (currentHp <= 0)
        {
            currentHp = 0;
            Instantiate(DieVFX, transform.position, Quaternion.identity);
            //todo:游戏输了
        }
        else
        {
            Instantiate(HurtVFX, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb2d.AddForce(h * moveSpeed * transform.right, ForceMode2D.Impulse);
        GetHurtTimer -= Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentHp += 1;
            currentHp = Mathf.Clamp(currentHp, 0, maxHp);
            float size = Hp2size[Mathf.RoundToInt(currentHp)];
            transform.localScale = new Vector3(size, size, 1f);
        }
    }

    private void OnDisable()
    {
        MessageCenter.RemoveListener(GetHurt,MESSAGE_TYPE.GET_HURT);
    }
}
