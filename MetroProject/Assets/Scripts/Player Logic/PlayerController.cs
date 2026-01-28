using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    public InputReader Input;       // 
    public PhysicsObject Physics;   // 

    // State Management
    public PlayerState CurrentState { get; private set; }

    // We instantiate states here so we don't create garbage memory by using 'new' constantly
    public PlayerState IdleState;
    public PlayerState MoveState;
    public PlayerState JumpState;
    public PlayerState FallState;

    private void Awake()
    {
        // Initialize States
        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
    }

    private void Start()
    {
        // Start the machine
        ChangeState(IdleState);
    }

    private void Update()
    {
        // Pass the Update down to the current state
        CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        // Pass the Physics Update down
        CurrentState.PhysicsUpdate();
    }

    public void ChangeState(PlayerState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = newState;
        CurrentState.Enter();
    }
}