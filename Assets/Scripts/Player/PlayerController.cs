using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft;} }

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float dashSpeed = 4f;
    [SerializeField] TrailRenderer myTrailRenderer;

    PlayerControls playerControls;
    Vector2 movement;
    Rigidbody2D rb;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;

    private bool facingLeft = false;
    private bool isDashing = false;
    private float startingMoveSpeed;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (Time.fixedDeltaTime * moveSpeed));
    }
    void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        
        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

}
