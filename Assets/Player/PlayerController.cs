using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    [HideInInspector]public Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody2D rb;
    private PlayerStateMachine stateMachine;

    [HideInInspector] public IdleState idleState;
    [HideInInspector] public RunState runState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public FallState fallState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine);
        runState = new RunState(this, stateMachine);
        jumpState = new JumpState(this, stateMachine);
        fallState = new FallState(this, stateMachine);
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void SetVelocityX(float x) => rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
    public void SetVelocityY(float y) => rb.linearVelocity = new Vector2(rb.linearVelocity.x, y);

    public void FlipCheck(float move)
    {
        if (move != 0)
            spriteRenderer.flipX = move < 0;
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();

        RaycastHit2D hit = Physics2D.BoxCast(
            col.bounds.center,
            col.bounds.size,
            0f,
            Vector2.down,
            extraHeight,
            LayerMask.GetMask("Ground")
        );
        return hit.collider != null;
    }
}
