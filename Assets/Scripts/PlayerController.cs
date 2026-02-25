using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public float jumpForce;
    public Transform visual;

    bool facingRight;

    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    Animator anim;

    public float groundCheckRadius = 0.2f;
    public float coyoteTime = 0.1f;
    float coyoteTimeCounter;

    public int coin = 0;
    public static int deathCount = 0;

    // hareket giriþi burada saklanacak
    float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = false;
    }

    void Update()
    {
        // Yatay input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Coyote time sayacý
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;     // yerdeyken her frame resetleniyor
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // havadayken geri sayýyor
        }

        // Jump (basýþ anýnda, coyote süresi içinde)
        if (Input.GetKey(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            coyoteTimeCounter = 0f;   // ayný coyote süresi içinde tekrar tekrar zýplamasýn
        }

        // Koþma animasyonu
        if (Mathf.Abs(moveInput) < 0.01f)
        {
            anim.SetBool("PlayerRunning", false);
        }
        else
        {
            anim.SetBool("PlayerRunning", true);
        }
    }

    void FixedUpdate()
    {
        // Fiziksel hareket burada
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = visual.localScale;
        scaler.x *= -1f;
        visual.localScale = scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Enemy")
        {
            deathCount++;

            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }
    }
}