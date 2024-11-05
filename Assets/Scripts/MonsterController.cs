using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rigidbody2d;
    
    public float speed = 0.01f;
    float timer = 10;
    public bool vertical;
    
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
    }

 
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        timer-=(Time.deltaTime);
        if (GameManager.Instance.battlescene.active == true)
        {
            return;
        }
        if (vertical)
        {
            if (timer > 0 && timer < 5)
            {
                position += speed * new Vector2(0, 1) * Time.fixedDeltaTime;
            }
            else if (timer > 5 && timer < 10)
            {
                position += speed * new Vector2(0, -1) * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (timer > 0 && timer < 5)
            {
                position += speed * new Vector2(1, 0) * Time.fixedDeltaTime;
            }
            else if (timer > 5 && timer < 10)
            {
                position += speed * new Vector2(-1, 0) * Time.fixedDeltaTime;
            }
        }
        if (timer < 0) { timer = 10; }
        rigidbody2d.MovePosition(position);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Luna"))
        {
            GameManager.Instance.SetEnemyObject(gameObject);
            GameManager.Instance.EnterorExitBattle();
        }
    }
}
