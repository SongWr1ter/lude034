using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Able,
    Die,
}
public class PlayerControl : MonoBehaviour
{
    [SerializeField,Tooltip("速度")]
    private float moveSpeed;
    private Rigidbody2D rb2d;

    [SerializeField, Tooltip("血量")] 
    public float maxHp;

    private float currentHp;
    private CommonMessage HurtMessage;
    private PlayerState currentState;
    
    private float GetHurt_TimeInterval = 0.5f;
    [SerializeField]private float GetHurtTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        MessageCenter.AddListener(GetHurt,MESSAGE_TYPE.GET_HURT);
        maxHp = 10.0f;
        currentState = PlayerState.Able;
    }
    void GetHurt(CommonMessage mag)
    {
        if (GetHurtTimer > 0.0f)
        {
            return;
        }
        currentHp -= mag.intParam;
        GetHurtTimer = GetHurt_TimeInterval;
        if (currentHp <= 0)
        {
            currentHp = 0;
            PlayerDie();
            //todo:游戏输了
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rb2d.AddForce(h * moveSpeed * transform.right, ForceMode2D.Impulse);
        GetHurtTimer -= Time.deltaTime;
    }

    public void PlayerDie()
    {
        currentState = PlayerState.Die;
    }

}
