using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{

    public float rangeDefence, speed;
    public Transform defence;
    private GameObject _ball;
    private Rigidbody _rigidbodyAI;

    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        //_rigidbodyAI = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (Mathf.Abs(_ball.transform.position.x - transform.position.x) < rangeDefence)
        {
            if (_ball.transform.position.x > transform.position.x)
            {
                _rigidbodyAI.velocity = new Vector2(Time.deltaTime * speed, _rigidbodyAI.velocity.y);
            }
            else
            {
                _rigidbodyAI.velocity = new Vector2(-Time.deltaTime * speed, _rigidbodyAI.velocity.y);
            }
        }
        else
        {
            if (transform.position.x > defence.position.x)
            {
                _rigidbodyAI.velocity = new Vector2(-Time.deltaTime * speed, _rigidbodyAI.velocity.y);
            }
            else
            {
                _rigidbodyAI.velocity = new Vector2(0, _rigidbodyAI.velocity.y);
            }
        }
    }
}
