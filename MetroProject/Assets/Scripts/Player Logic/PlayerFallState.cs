using UnityEngine;

public class PlayerFallState : PlayerState
{
    private Vector2 _inputVector;
    private bool _jumpPressed;
    public PlayerFallState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        // Hook into the events we made in Step 1
        _player.Input.MoveEvent += OnMove;
        _player.Input.JumpEvent += OnJump;
    }

    public override void Exit()
    {
        // Always unsubscribe!
        _player.Input.MoveEvent -= OnMove;
        _player.Input.JumpEvent -= OnJump;
    }

    public override void LogicUpdate()
    {
        // If the player presses Jump...
        if (_jumpPressed) //_player.Input.JumpTriggered
        {
            // CHECK: Do we have jumps left? (The "Designer Rule")
            if (_player.JumpCount < _player.Data.MaxJumpCount)
            {
                _player.ChangeState(_player.JumpState);
                return;
            }
        }

        // ... other logic (landing, etc)
    }

    public override void PhysicsUpdate()
    {
        // Set horizontal velocity using the Physics Wrapper from Step 2
        _player.Physics.SetVelocity(new Vector2(_inputVector.x * _player.Data.MoveSpeed, _player.Physics.RB.linearVelocity.y));
    }

    // --- Event Handlers ---

    private void OnMove(Vector2 input)
    {
        _inputVector = input;
    }

    private void OnJump()
    {
        _jumpPressed = true;
        // Handled in LogicUpdate to check jump count
    }
}
