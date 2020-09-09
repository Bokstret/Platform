using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    static Timer attackPause;
    static Timer timer;
    private int maxSpeed = 7;
    private int move;
    public int playerSpeed;
    private bool isGrounded = false;
    private bool inDanger = false;
    public static bool playing = true;
    private bool isEnd = false;
    public LayerMask whatIsDanger;
    public Transform groundCheck;
    public float damage;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public LayerMask whatIsExit;
    private bool isFacingRight = true;
    public Transform attack;
    public Transform attack2;
    public Transform attack3;
    public Transform attack4;
    public float attackRadius;
    public static Animator anim;
    public static bool invulnerability = false;
    static HeroController instance;
    Button dialogue;
    static GameObject[] gameObjects;
    static Image HP;
    static public Sprite[] HPSpriteArray;
    private Rigidbody2D rb2D;

    void Awake()
    {
        instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        dialogue = GameObject.Find("EndDialogue").GetComponent<Button>();
        HP = GameObject.Find("HP").GetComponent<Image>();
        HPSpriteArray = Resources.LoadAll<Sprite>("sprites/HP");
    }

    void Start()
    {
        playing = true;
        InvulnerabilityOff();
        instance.enabled = true;
        instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        attackPause = gameObject.AddComponent<Timer>();
        attackPause.Duration = 0.75f;

        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1;

        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        int getAxisHor = (int)Input.GetAxis("Horizontal");
        if (getAxisHor != 0)
        {
            Move(getAxisHor);
        }
        else
        {
            Move(getAxisHor);
        }



        isEnd = Physics2D.OverlapCircle(attack.position, attackRadius, whatIsExit);
        if (isEnd == true & playing == true)
        {
            dialogue.onClick.Invoke();
            Physics2D.IgnoreLayerCollision(10, 9, true);
            Physics2D.IgnoreLayerCollision(10, 11, true);
            ButtonsLevel.HUD.SetActive(false);
            playing = false;
            gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (GameObject obj in gameObjects)
            {
                if (obj.layer == 9)
                {
                    Destroy(obj);
                }
            }
            move = 0;
        }

        inDanger = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsDanger);
        if (inDanger & invulnerability == false)
        {
            GetComponent<HP>().health -= 10;
            InvulnerabilityOn();
            HPBarCheck();
            if (GetComponent<HP>().health == 0)
            {
                Death();
            }
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("vSpeed", rb2D.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(move));
        rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);

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

        if (attackPause.Finished)
        {
            ButtonsLevel.attack.GetComponent<Button>().enabled = true;
        }
    }

    public void Jump()
    {
        if (anim.GetBool("IsGrounded"))
        {
            rb2D.AddForce(new Vector2(0, 650), ForceMode2D.Force);
            anim.SetBool("IsGrounded", false);
        }   
    }

    public void Move(int InputAxis)
    {
        move = InputAxis;
    }

    public void Attack()
    {
        if (anim.GetBool("IsGrounded") == true)
        {
            int attackCheck = Random.Range(1, 3);
            if (attackCheck == 1)
            {
                anim.SetTrigger("IsAttack");
            }
            else
            {
                anim.SetTrigger("IsAttack2");
            }
            Fight.Hit(attack.position, attackRadius, 9, damage);
            Fight.Hit(attack2.position, attackRadius, 9, damage);
            Fight.Hit(attack3.position, attackRadius, 9, damage);
            Fight.Hit(attack4.position, attackRadius, 9, damage);
            ButtonsLevel.attack.GetComponent<Button>().enabled = false;
            attackPause.Run();
        }
    }

    public static void InvulnerabilityOn()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
        invulnerability = true;
        anim.SetTrigger("IsHurt");
        timer.Run();
    }

    public static void InvulnerabilityOff()
    {
        Physics2D.IgnoreLayerCollision(10, 9, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerability = false;
    }

    public static void HPBarCheck()
    {
        foreach (Sprite spr in HPSpriteArray)
        {
            if("HP" + instance.GetComponent<HP>().health.ToString() == spr.name)
            {
                HP.sprite = spr;
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    public static void Death()
    {
        anim.SetTrigger("Dead");
        gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in gameObjects)
        {
            if (obj.layer == 9)
            {
                Destroy(obj);
            }
        }
        ButtonsLevel.HUD.SetActive(false);
        ButtonsLevel.resume.SetActive(false);
        ButtonsLevel.pause.SetActive(false);
        ButtonsLevel.restart.SetActive(true);
        ButtonsLevel.menu.SetActive(true);
        instance.enabled = false;
        instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}