using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerLeft : MonoBehaviour
{
    public static MovementPlayerLeft Instance;
    public Animator _animator;
    public Animator Anim { get { return _animator; } }

    private Vector3 _position;
    private float _jumpForce = 6.5f;
    private float _speed = 5f;
    private Rigidbody2D _rigidbody;

    public bool grounded, canShoot;

    private GameObject _ball;
    public LayerMask groundLayer;
    public Transform checkGround;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _animator = GetComponent<Animator>();
        canShoot = false;
        grounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // grounded = Physics2D.OverlapCircle(checkGround.position, 0.25f, groundLayer); 
        if (Input.GetKey(KeyCode.W) && grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Tab) && canShoot)
        {
            Shoot();
        }

        MovePlayer();

        // Evitar que empieze a rotar
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(checkGround.position, 1f, groundLayer);
    }
    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce); 
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _position.x = _speed;
            transform.position += _position * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _position.x = _speed;
            transform.position -= _position * Time.deltaTime;
        }   
    }

    void Shoot()
    {
        _animator.SetTrigger("Shoot");
        _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 150));
    }
}