using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Singleton Reference
    public static PlayerController Instance;
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    
    [Header("StateMachine")]
    [SerializeField] private States startState;
    private States currentState;
    private float stateUptime;

    [Header("Ground Movement")] 
    [SerializeField] private float groundSpeed;
    [SerializeField] private float groundDrag, groundCheckLength, groundCheckOffset;
    private float timeSinceLastGroundCheck;
    private bool grounded;

    [SerializeField] private LayerMask groundLayer;
    
    [Header("Air Movement")]
    [SerializeField] private float airSpeed, jumpAmount;
    [SerializeField] private float jumpBuffer;

    [Header("Input")] 
    private float moveX;
    private float timeOfLastJumpInput = -10f;

    [Header("Stats")] 
    public int numberOfGems;
    
    public enum States
    {
        move,
        idle,
        air,
        hurt
    }


    private void Awake()
    {
        // if there is already a player in our scene, delete this one
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = startState;
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckGrounded();
        GatherInput();
        UpdateState(Time.deltaTime);
        CheckTransitions();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    
    public void ChangeState(States state)
    {
        if (state == currentState) return;
        ExitState();
        currentState = state;
        EnterState();
    }

    private void GatherInput()
    {
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            timeOfLastJumpInput = Time.time;
        }
    }

    private void Jump()
    {
        timeOfLastJumpInput = -1f;
        rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
    }

    public void CheckGrounded()
    {
        timeSinceLastGroundCheck += Time.deltaTime;
        if (timeSinceLastGroundCheck > .05f)
        {
            timeSinceLastGroundCheck = 0;
            Debug.DrawRay(transform.position + Vector3.right * groundCheckOffset, Vector3.down * groundCheckLength, Color.red);
            Debug.DrawRay(transform.position - Vector3.right * groundCheckOffset, Vector3.down * groundCheckLength, Color.red);
            grounded = Physics2D.Raycast(transform.position + Vector3.right * groundCheckOffset, Vector2.down, groundCheckLength, groundLayer) 
                       || Physics2D.Raycast(transform.position - Vector3.right * groundCheckOffset, Vector2.down, groundCheckLength, groundLayer);
        }
        
    }
    
    #region State Logic
    private void EnterState()
    {
        stateUptime = 0f;
        switch (currentState)
        {
            case States.move:
                anim.Play("Move");
                rb.drag = groundDrag;
                break;
            case States.idle:
                anim.Play("Idle");
                rb.drag = groundDrag;
                break;
            case States.air:
                rb.drag = 0;
                break;
            case States.hurt:
                rb.simulated = false;
                anim.Play("Hurt");
                break;
        }
    }
    
    private void UpdateState(float dt)
    {
        stateUptime += dt;
        switch (currentState)
        {
            case States.move:
                rb.velocity = new Vector2(moveX * groundSpeed, rb.velocity.y);
                if (moveX < -0.01f)
                {
                    anim.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (moveX > 0.01f)
                {
                    anim.transform.localScale = new Vector3(1, 1, 1);
                }
                break;
            
            case States.idle:
                break;
            
            case States.air:
                rb.velocity = new Vector2(moveX * airSpeed, rb.velocity.y);
                
                if (moveX < -0.01f)
                {
                    anim.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (moveX > 0.01f)
                {
                    anim.transform.localScale = new Vector3(1, 1, 1);
                }

                if (rb.velocity.y > 0f)
                {
                    anim.Play("Jump");
                }
                else
                {
                    anim.Play("Fall");
                }
                break;
            case States.hurt:
                
                break;
        }
    }

    private void ExitState()
    {
        switch (currentState)
        {
            case States.move:
                
                break;
            case States.idle:
                
                break;
            case States.air:
                
                break;
            case States.hurt:
                
                break;
        }
    }

    public void CheckTransitions()
    {
        if ((currentState == States.move || currentState == States.idle) && Time.time - timeOfLastJumpInput < jumpBuffer)
        {
            Jump();
            ChangeState(States.air);
            return;
        }
        
        if ((currentState == States.move || currentState == States.idle) && !grounded)
        {
            ChangeState(States.air);
            return;
        }

        if (currentState == States.air && grounded && rb.velocity.y < 0.1f)
        {
            ChangeState(States.idle);
            return;
        }
        if (currentState == States.move && Mathf.Abs(rb.velocity.x) < 0.5f)
        {
            ChangeState(States.idle);
            return;
        }
        if (currentState == States.idle && Mathf.Abs(moveX) >= 0.1f)
        {
            ChangeState(States.move);
            return;
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Hit Hazard");
            ChangeState(States.hurt);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Hit Hazard");
            ChangeState(States.hurt);
        }
    }
}
