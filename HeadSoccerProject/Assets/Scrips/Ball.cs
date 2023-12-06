using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public static Ball instance;

    private GameObject _player1;
    private GameObject _player2;
    private GameObject controller;
    private Rigidbody2D rBody;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player1 = GameObject.FindGameObjectWithTag("Player1");
        _player2 = GameObject.FindGameObjectWithTag("Player2");
        controller = GameObject.FindGameObjectWithTag("GameController");

        rBody = this.GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            MovementPlayerLeft.Instance.canShoot = true;
        }

        if (collision.gameObject.tag == "Player2")
        {
            MovementPlayerRight.Instance.canShoot = true;
        }

        if (collision.gameObject.tag == "GoalLeft")
        {
            // Goal graphic
            // Instantiate(goal, new vector3(0, -2, 0), Quaternion.identity);

            if (!matchManager.instance.isScore[0] && !matchManager.instance.isScore[1])
            {
                matchManager.instance.isScore[0] = true;
                Console.WriteLine("Goal");
            }
        }

        if (collision.gameObject.tag == "GoalRight")
        {
            // Goal graphic
            // Instantiate(goal, new vector3(0, -2, 0), Quaternion.identity);
            if (!matchManager.instance.isScore[1] && !matchManager.instance.isScore[0])
            {
                matchManager.instance.isScore[1] = true;
                Console.WriteLine("Goal");
            }
        }
    }
     
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            MovementPlayerLeft.Instance.canShoot = false;
        }

        if (collision.gameObject.tag == "Player2")
        {
            MovementPlayerRight.Instance.canShoot = false;
        }
    }
}
