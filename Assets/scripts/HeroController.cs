using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    private int maxSpeed = 7;
    private int move;
    public int playerSpeed;
    private bool isGrounded = false;
    public Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool isFacingRight = true;
    private Animator anim;
    public Transform attack;
    public float attackRadius;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
            
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }
    }


    public void Jump()
    {
        if (anim.GetBool("IsGrounded") == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 650), ForceMode2D.Force);
            anim.SetBool("IsGrounded", false);
            Invoke("ToIdle", 0.05f);
        }
        
        
    }

    public void Move(int InputAxis)
    {
        move = InputAxis;
    }

    public void Attack()
    {
        if (anim.GetBool("IsAttack") == false )

        {
            anim.SetBool("IsAttack", true);
            Fight2D.Action(attack.position, attackRadius, 9, 5);
            anim.Play("Attack");
            Invoke("NoAttack", 0.07f);
        }
    }

    private void NoAttack()
    {
        anim.SetBool("IsAttack", false);
    }

    private void ToIdle()
    {
        anim.Play("Idle");
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}