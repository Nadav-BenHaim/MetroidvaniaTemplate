using UnityEngine;
public class PlayerMoveState : PlayerState
{
    private Vector2 _inputVector;

    public PlayerMoveState(PlayerController player) : base(player) { }

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
        // TRANSITION: If we stopped moving, go to Idle
        if (_inputVector.x == 0)
        {
            _player.ChangeState(_player.IdleState);
            return;
        }

        // TRANSITION: If we walked off a ledge, fall
        if (!_player.Physics.IsGrounded)
        {
            _player.ChangeState(_player.FallState);
        }
    }

    public override void PhysicsUpdate()
    {
        // Set velocity using the Physics Wrapper from Step 2
        _player.Physics.SetVelocity(new Vector2(_inputVector.x * 10f, _player.Physics.RB.linearVelocity.y));
    }

    // --- Event Handlers ---

    private void OnMove(Vector2 input)
    {
        _inputVector = input;
    }

    private void OnJump()
    {
        _player.ChangeState(_player.JumpState);
    }
}