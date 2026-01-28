using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _gravityScale = 1f;

    // Using a BoxCast is better than a Raycast because it has width
    // preventing the player from slipping off edges pixel-perfectly.
    [Header("Collision Checks")]
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.5f, 0.1f);
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.1f, 0.5f);
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Transform _frontWallCheckPoint;

    public bool IsGrounded { get; private set; }
    public bool IsTouchingWall { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public int FacingDirection { get; private set; } = 1; // 1 = Right, -1 = Left

    private Vector2 _currentVelocity;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.gravityScale = _gravityScale;
    }

    private void FixedUpdate()
    {
        CheckCollisions();

        // We apply velocity here to ensure it aligns with the Physics step
        RB.linearVelocity = _currentVelocity; // Updated to use linearVelocity
    }

    // --- The Public API (What the State Machine calls) ---

    public void SetVelocity(Vector2 velocity)
    {
        _currentVelocity = velocity;
        CheckFlip(velocity.x);
    }

    public void SetGravity(float scale)
    {
        RB.gravityScale = scale;
    }

    // A crucial helper for platformers to prevent "bouncing" down slopes
    public void SnapToGround()
    {
        if (IsGrounded && _currentVelocity.y <= 0)
        {
            RB.position += Vector2.down * 0.1f;
        }
    }

    // --- Internal Logic ---

    private void CheckCollisions()
    {
        // Ground Check
        IsGrounded = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0f, _groundLayer);

        // Wall Check
        IsTouchingWall = Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0f, _groundLayer);
    }

    private void CheckFlip(float xInput)
    {
        if (xInput != 0 && (int)Mathf.Sign(xInput) != FacingDirection)
        {
            Flip();
        }
    }
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // --- Debug Visualization (Essential for Jams!) ---
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_groundCheckPoint)
            Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);

        Gizmos.color = Color.blue;
        if (_frontWallCheckPoint)
            Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
    }
}
