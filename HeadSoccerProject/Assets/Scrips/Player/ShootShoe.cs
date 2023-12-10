using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShoe : MonoBehaviour
{
    private GameObject ball;    
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Ball")
    //    {
    //        if (Character2DController.instance.isMove == true && Character2DController.instance.isShoot == false)
    //        {
    //            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(_ball.GetComponent<Rigidbody2D>().velocity.x / 2f,
    //                                                                     _ball.GetComponent<Rigidbody2D>().velocity.y / 2f);
    //        }

    //    }
    //}
}
