using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController player) : base(player) { }
    public override void Enter()
    {
        base.Enter(); // not actually needed unless PlayerState has logic in Enter()

        // Use the value from the ScriptableObject
        _player.Physics.SetVelocity(new Vector2(_player.Physics.RB.linearVelocity.x, _player.Data.JumpForce));

        // Increment the counter so we know we used a jump
        _player.JumpCount++;

        //_player.Input.UseJumpInput(); // Consume the input so it doesn't fire twice
    }
    public override void LogicUpdate()
    {
        // TRANSITION: If we are falling, go to Fall state
        if (_player.Physics.RB.linearVelocity.y < 0)
        {
            _player.ChangeState(_player.FallState);
        }
    }
    public override void PhysicsUpdate()
    {
        // Maintain horizontal velocity (optional, can be modified for air control)
        _player.Physics.SetVelocity(new Vector2(_player.Physics.RB.linearVelocity.x, _player.Physics.RB.linearVelocity.y));
    }
}
