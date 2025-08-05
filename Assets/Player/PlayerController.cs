using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //필요한 변수 선언
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform attackPoint;
    public float attackRange = 2f;
    public int attackDamage = 10;
    [HideInInspector] public Animator animator;
    [SerializeField] private LayerMask groundLayer; // 땅(타일맵)만 땅으로 인식하기 위한 레이어 분리
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    //SateMachine 및 각 State 선언
    private PlayerStateMachine stateMachine;
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public RunState runState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public FallState fallState;
    [HideInInspector] public AttackState attackState;

    void Awake()
    {
        //컴포넌트 불러오기
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //StateMachine 및 State 인스턴스 생성
        stateMachine = new PlayerStateMachine();

        idleState = new IdleState(this, stateMachine);
        runState = new RunState(this, stateMachine);
        jumpState = new JumpState(this, stateMachine);
        fallState = new FallState(this, stateMachine);
        attackState = new AttackState(this, stateMachine);
    }

    void Start() //IdleState로 시작
    {
        stateMachine.Initialize(idleState);
    }

    void Update() //현재 State의 LogicUpdate를 계속 실행
    {
        stateMachine.currentState.LogicUpdate();
    }

    void FixedUpdate() //현재 State의 Physics를 계속 실행
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    //일부 State에서 사용할 플레이어 움직임 제어 함수
    public void SetVelocityX(float x) => rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
    public void SetVelocityY(float y) => rb.linearVelocity = new Vector2(rb.linearVelocity.x, y);

    public void FlipCheck(float move)//좌우 전환
    {
        if (move != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(move) * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    public bool IsGrounded()//땅에 닿아있는지 확인
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
    public void OnAttackAnimationEnd() // 애니메이션 이벤트
    {
        if (stateMachine.currentState == attackState)
        {
            attackState.OnAttackAnimationEnd();
        }
    }
    public void PerformAttack() // 애니메이션 이벤트
    {
        //공격 범위 내 오브젝트 탐지 -> Interactable 컴포넌트를 가진 오브젝트라면 interact 실행
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D Interactable in hitEnemies)
        {
            Interactable.GetComponent<Interactable>()?.Interact(attackDamage, transform.position);
        }
    }
    //Scene에서 공격 범위를 표시해주는 함수, 궁금하다면 주석 풀고 써보세요
    /*
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    */
}
