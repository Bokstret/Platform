using System.Collections;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    static Timer resetAnim;
    static Timer timer;
    private int maxSpeed = 7;
    private int move;
    public int playerSpeed;
    private bool isGrounded = false;
    public Transform groundCheck;
    public float damage;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool isFacingRight = true;
    public Transform attack;
    public float attackRadius;
    public static Animator anim;
    public static bool invulnerability = false;
    public static float blinkTime = 0.3f;
    static bool blink = false;
    static bool state = true;
    static SpriteRenderer renderer;
    static HeroController instance;

    void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        resetAnim = gameObject.AddComponent<Timer>();
        resetAnim.Duration = 0.5f;

        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2;

        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
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

        if (timer.Finished)
        {
            InvulnerabilityOff();
        }

        if(resetAnim.Finished)
        {
            anim.SetBool("IsHurt", false);
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
        if (anim.GetBool("IsAttack") == false & anim.GetBool("IsGrounded") == false)
        {
            anim.SetBool("IsAttack", true);
            Fight2D.Action(attack.position, attackRadius, 9, damage);
            anim.Play("Attack");
            Invoke("NoAttack", 5);
        }

        else
        {
            anim.SetBool("IsAttack", true);
            Fight2D.Action(attack.position, attackRadius, 9, damage);
            anim.Play("Attack");
            Invoke("NoAttack", 0.07f);
        }

        
    }
    public static void InvulnerabilityOn()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
        invulnerability = true;
        blink = true;
        anim.SetBool("IsHurt", true);
        resetAnim.Run();
        timer.Run();
        instance.StartCoroutine(Blink());
    }

    public static void InvulnerabilityOff()
    {
        Physics2D.IgnoreLayerCollision(10, 9, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerability = false;
        blink = false;
        renderer.enabled = true;
        state = true;
        instance.StopAllCoroutines();
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

    private static IEnumerator Blink()
    {
        while (blink)
        {
            state = !state;
            renderer.enabled = state;
            yield return new WaitForSeconds(blinkTime); 
        }
    }
}