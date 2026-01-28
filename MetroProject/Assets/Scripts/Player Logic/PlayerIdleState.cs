using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private Vector2 _inputVector;
    public PlayerIdleState(PlayerController player) : base(player) { }
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
        // TRANSITION: If we start moving, go to Move
        if (_inputVector.x != 0)
        {
            _player.ChangeState(_player.MoveState);
            return;
        }
        // TRANSITION: If we walked off a ledge, fall // does this belong here?
        if (!_player.Physics.IsGrounded)
        {
            _player.ChangeState(_player.FallState);
        }
    }
    public override void PhysicsUpdate()
    {
        // Set velocity using the Physics Wrapper from Step 2
        _player.Physics.SetVelocity(new Vector2(0, _player.Physics.RB.linearVelocity.y));
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
