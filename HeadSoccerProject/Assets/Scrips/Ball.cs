using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject _player1;
    private GameObject _player2;

    void Start()
    {
        _player1 = GameObject.FindGameObjectWithTag("Player1");
        _player2 = GameObject.FindGameObjectWithTag("Player2");
    
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
