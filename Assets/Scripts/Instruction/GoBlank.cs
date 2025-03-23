using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBlank : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeSpeed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,2 * fadeTime);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= fadeTime)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(fadeSpeed, 0);
        }
    }
}
