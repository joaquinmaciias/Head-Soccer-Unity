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


    private ScoreManager scoreManager;
    private StateManager stateManager = StateManager.GetInstance();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player1 = GameObject.FindGameObjectWithTag("Player1");
        _player2 = GameObject.FindGameObjectWithTag("Player2");

        scoreManager = ScoreManager.Instance;

        // We will need to change score and currentState -> we call those functions

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
            // Goal for player 2
            scoreManager.AddScore(0);
            stateManager.ChangeState(new GoalState());
        }

        if (collision.gameObject.tag == "GoalRight")
        {
            scoreManager.AddScore(1);
            stateManager.ChangeState(new GoalState());
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
