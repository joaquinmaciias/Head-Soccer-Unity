using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerLeft : MonoBehaviour
{
    public static MovementPlayerLeft Instance;

    private float _jumpForce = 6.5f;
    private float _velocityPlayer = 5f;
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
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.25f, groundLayer);
    }
    public void Jump()
    {
        if (grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    void MovePlayer()
    {
        float horizontalMomevent = Input.GetAxis("HorizontalPlayerLeft");
        Vector2 velocidad = new Vector2(horizontalMomevent * _velocityPlayer, _rigidbody.velocity.y);
        _rigidbody.velocity = velocidad;
    }

    void Shoot()
    {
        if (canShoot)
        {
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 250));

            // Rotacion Shoe
            Transform shoeTransform = transform.Find("Root/Shoe");

            // Realizar la animación de rotación del Shoe
            //float rotationAngle = 20f;
            //float rotationDuration = 0.5f;

            // Crear un objeto visual secundario
            GameObject visualShoe = new GameObject("VisualShoe");
            visualShoe.transform.parent = shoeTransform; // Establecer el "Shoe" como padre
            visualShoe.transform.localPosition = Vector3.zero;

            // Configurar el material y la geometría del objeto visual
            visualShoe.AddComponent<SpriteRenderer>().sprite = shoeTransform.GetComponent<SpriteRenderer>().sprite;
            visualShoe.GetComponent<SpriteRenderer>().material = shoeTransform.GetComponent<SpriteRenderer>().material;

            // Realizar la animación de rotación del objeto visual
            StartCoroutine(RotateVisualShoe(visualShoe.transform, 360f, 0.5f));
        }
    }

    IEnumerator RotateVisualShoe(Transform visualShoeTransform, float angle, float duration)
    {
        float elapsed = 0f;
        Quaternion startRotation = visualShoeTransform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, angle);

        while (elapsed < duration)
        {
            visualShoeTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        visualShoeTransform.rotation = targetRotation;

        // Limpiar el objeto visual después de la animación
        Destroy(visualShoeTransform.gameObject);
    }

}