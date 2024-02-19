using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharControl : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 5;
    public static float healthPoint = 3;
    public static int points = 0;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public Animator anim;

    private bool isGround;
    public Transform groundcheck;
    private int ExtraJump;
    public int extraJumpsValue;
    public float checkRadius;
    public LayerMask whatIsGround;

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if(healthPoint <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("LevelOne");
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!isGround)
        {
            anim.SetBool("jump", true);
        }
        if(isGround)
        {
            anim.SetBool("jump", false);
            ExtraJump = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && ExtraJump > 0)
        {
            anim.SetBool("jump", true);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            ExtraJump--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && ExtraJump == 0 && isGround == true)
        {
            anim.SetBool("jump", true);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, whatIsGround);

        float movement = Input.GetAxis("Horizontal");
        if(Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
            anim.SetBool("walk", true);
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
    }
}
