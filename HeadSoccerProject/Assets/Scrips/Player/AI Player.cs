using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementPlayerAI : MonoBehaviour
{

    public static MovementPlayerAI Instance;
    public Animator _animator;
    public Animator Anim { get { return _animator; } }

    private Vector3 _position;
    private float rangeDefence = 6;
    public Transform defence;
    private Rigidbody2D _rigidbodyAI;
    private float _jumpForce = 6.5f;
    private float _speed = 5f;

    public bool grounded, canShoot;

    public GameObject _ball;
    public LayerMask groundLayer;
    public Transform checkGround;
    private GameController gameController;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        //_ball = GameObject.FindGameObjectWithTag("Ball");
        _rigidbodyAI = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        canShoot = false;
        grounded = false;
        gameController = FindObjectOfType<GameController>();

        if (_ball == null)
        {
            Debug.LogError("No se encuentra el objeto Ball");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (canShoot)
        {
            Shoot();
        }

        if (!gameController.isGamePaused)
        { MoveAI(); }

        // Evitar que empieze a rotar
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(checkGround.position, 1f, groundLayer);
    }
    public void MoveAI()
    {
        if (_ball == null)
        {
            Debug.Log("No se encuentra el objeto Ball");
        }
        if (Mathf.Abs(_ball.transform.position.x - transform.position.x) < rangeDefence)
        {
            if (_ball.transform.position.x > (transform.position.x - 0.2f))
            {
                _position.x = _speed;
                transform.position += _position * Time.deltaTime;
            }
            else
            {
                _position.x = _speed;
                transform.position -= _position * Time.deltaTime;
            }
        }
        else
        {
            if (transform.position.x > defence.position.x)
            {
                _position.x = _speed;
                transform.position -= _position * Time.deltaTime;
            }
        }

        if ((_ball.transform.position.y > (transform.position.y + 0.9f)) && grounded)
        {
            Jump();
        }
    }

    private void Shoot()
    {
        _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 150));
    }

    private void Jump()
    {
        _rigidbodyAI.velocity = new Vector2(_rigidbodyAI.velocity.x, _jumpForce);
    }
}