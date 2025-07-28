using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoujo : MonoBehaviour
{
    public int direction = 1;
    private float speed = 3.0f;
    public Rigidbody2D rb;
    public float jumpForce = 5.5f;
    public float leftLimit = -2.6f;
    public float rightLimit = 2.6f;


    private bool isGrounded = false;
    private bool isBoosting = false;

    SpriteRenderer rederer;


    void Start()
    {
        Application.targetFrameRate = 60;
        rederer = GetComponent<SpriteRenderer>();
    }

   void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            direction *= -1;
            rederer.flipX = !rederer.flipX;
        }
        if (Input.GetMouseButton(1) && isGrounded)
        {
            Vector2 currentVelocity = rb.velocity;

            rb.velocity = new Vector2(currentVelocity.x, jumpForce);
            isGrounded = false;
        }
        if (transform.position.x > rightLimit)
        {
            direction = -1;
            rederer.flipX = true;
        }

        else if (transform.position.x < leftLimit)
        {
            direction = 1;
            rederer.flipX = false;
        }

        if (isGrounded)
        {
            transform.position = (Vector2)transform.position + new Vector2(direction * speed * Time.deltaTime, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isBoosting == false && GameManager.Instance.isSkill == true)
        {
            StartCoroutine(SpeedBoost());
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
    IEnumerator SpeedBoost()
    {
        isBoosting = true;

        speed += 3.0f;

        yield return new WaitForSeconds(2f);

        speed -= 3.0f;

        isBoosting = false;
        GameManager.Instance.UseSkill();
    }

}
