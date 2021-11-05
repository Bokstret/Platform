using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    Transform[] attackList = new Transform[10];

    static Timer jumpPause;
    static Timer attackPause;
    static Timer timerInv;
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

        jumpPause = gameObject.AddComponent<Timer>();
        jumpPause.Duration = 0.5f;

        timerInv = gameObject.AddComponent<Timer>();
        timerInv.Duration = 1;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        
    }

    void FixedUpdate()
    {
        if (GetComponent<HP>().health > 0)
        {
            Move((int)Input.GetAxis("Horizontal"));
        }
        else
        {
            Move(0);
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
        if (!isGrounded)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsDanger);
        }
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

        if (timerInv.Finished)
        {
            InvulnerabilityOff();
        }

        if (attackPause.Finished)
        {
            ButtonsLevel.attack.GetComponent<Button>().interactable = true;
        }
        /*if (jumpPause.Finished)
        {
            ButtonsLevel.jump.GetComponent<Button>().interactable = true;
        }*/
    }

    public void Jump()
    {
        //if (ButtonsLevel.jump.GetComponent<Button>().interactable)
        //{
            if (anim.GetBool("IsGrounded"))
            {
                rb2D.AddForce(new Vector2(0, 650), ForceMode2D.Force);
                anim.SetBool("IsGrounded", false);
            }
            //ButtonsLevel.jump.GetComponent<Button>().interactable = false;
            jumpPause.Run();
        //}
    }

    public void Move(int InputAxis)
    {
        move = InputAxis;
    }

    public void Attack()
    {
        if (ButtonsLevel.attack.GetComponent<Button>().interactable)
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
                Invoke("DealDamage", 0.3f);
                ButtonsLevel.attack.GetComponent<Button>().interactable = false;
                attackPause.Run();
            }
        }
    }

    public void DealDamage()
    {
        foreach (Transform point in attackList)
        {
            Fight.Hit(point.position, attackRadius, 9, damage);
        }
    }

    public static void InvulnerabilityOn()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
        invulnerability = true;
        if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HeroAttack" & anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "HeroAttack2")
        {
            anim.SetTrigger("IsHurt");
        }
        timerInv.Run();
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
        //ButtonsLevel.resume.SetActive(false);
        //ButtonsLevel.pause.SetActive(false);
        ButtonsLevel.restart.SetActive(true);
        ButtonsLevel.menu.SetActive(true);
        instance.enabled = false;
        instance.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}