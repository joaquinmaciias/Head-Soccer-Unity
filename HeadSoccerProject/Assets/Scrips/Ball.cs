using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject _playerLeft, _playerRight, _playerAI;
    public GameObject goal;

    void Start()
    {
        _playerLeft = GameObject.FindGameObjectWithTag("PlayerLeft");
        _playerRight = GameObject.FindGameObjectWithTag("PlayerRight");
        _playerAI = GameObject.FindGameObjectWithTag("PlayerAI");


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerLeft")
        {
            MovementPlayerLeft.Instance.canShoot = true;
        }

        if (collision.gameObject.tag == "PlayerRight")
        {
            MovementPlayerRight.Instance.canShoot = true;
        }

        if (collision.gameObject.tag == "PlayerAI")
        {
            MovementPlayerAI.Instance.canShoot = true;
        }

        if (collision.gameObject.tag == "GoalRight")
        {
            Instantiate(goal, new Vector3(0,-2,0), Quaternion.identity);
        }

        if (collision.gameObject.tag == "GoalLeft")
        {
            Instantiate(goal, new Vector3(0, -2, 0), Quaternion.identity);
        }
    }
     
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerLeft")
        {
            MovementPlayerLeft.Instance.canShoot = false;
        }

        if (collision.gameObject.tag == "PlayerRight")
        {
            MovementPlayerRight.Instance.canShoot = false;
        }

        if (collision.gameObject.tag == "PlayerAI")
        {
            MovementPlayerAI.Instance.canShoot = false;
        }
    }
}
