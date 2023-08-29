using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    Door Door;
    Animator anim;
    [Header("Move")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpSpeed;
    private float horizontalMovement;
    private bool isGrounded=true;
    public bool isAttack=false;
    private bool isFacingRight = true;
    private Rigidbody2D rb2d;
    private bool isSword = true;
    [Header("Coyoto")]
    [SerializeField] private float coyoteTime = 0.2f;
    private bool isDashing = false;
    [Header("Dash")] 
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private TrailRenderer tr;
   
    void Start()
    {
        GameObject gameManager = GameObject.Find("Door");
        Door = gameManager.GetComponent<Door>();
        anim =GetComponent<Animator>();  
        rb2d=GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
       
    
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
        Jump();
        if (horizontalMovement > 0 && !isFacingRight)
        {
            FlipSprite();
        }
        else if (horizontalMovement<0 && isFacingRight)
        {
            FlipSprite();
        }
        if (Input.GetMouseButtonDown(0) && isSword == true)
        {
            StartCoroutine(Sword());
        }
     
    }
    private void FixedUpdate()
    {
        Movement();
       


    }
    private void Movement()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        transform.position+=new Vector3(horizontalMovement*movementSpeed*Time.deltaTime,0,0);
        anim.SetFloat("Runn",Mathf.Abs(horizontalMovement));
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            isGrounded=false;
            rb2d.velocity = Vector2.up * jumpSpeed;
        }
        if (!isGrounded)
        {
            anim.SetBool("jump", true);
        }
        else if (isGrounded)
        {
            anim.SetBool("jump", false);
        }
    }
    private void FlipSprite()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale=currentScale;
        isFacingRight=!isFacingRight;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
        if (other.gameObject.tag == "Knife")
        {
            
            if (Door.door == false)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                transform.position = new Vector3(-12.98f, -25.78f, 0);
            }

        }
        if (other.gameObject.tag == "ResPawmn")
        {
            if (Door.door == false)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                transform.position = new Vector3(-12.98f, -26.78f, 0);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Knife")
        {
            SceneManager.LoadScene(0);

        }
    }
    //private void OnCollisionExit2D(Collision2D other)
    //{

    //    if (other.gameObject.tag == "Platform")
    //    {

    //        StartCoroutine(WaitCoyoto());
    //    }
    //}
    IEnumerator Sword()
    {
        isSword= false;
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        isAttack = true;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        anim.SetBool("Attack", false);


        yield return new WaitForSeconds(5f);
        isSword = true;
    }
    IEnumerator WaitCoyoto()
    {
        
        yield return new WaitForSeconds(coyoteTime);
        
        isGrounded = false;
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        float initialHorizontalMovement = horizontalMovement;

        rb2d.velocity = new Vector2(initialHorizontalMovement * dashDistance / dashDuration, 0);
        //tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        //tr.emitting = false;

        rb2d.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        isDashing = false;
    }

}
