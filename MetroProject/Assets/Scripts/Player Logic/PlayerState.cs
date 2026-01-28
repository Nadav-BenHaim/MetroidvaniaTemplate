public abstract class PlayerState
{
    // Reference to the main controller (The Context)
    protected PlayerController _player;

    // Constructor to pass dependencies
    public PlayerState(PlayerController player)
    {
        _player = player;
    }

    // Called once when the state starts
    public virtual void Enter() { }

    // Called every frame (Unity Update) - Logic, Inputs, Timers
    public virtual void LogicUpdate() { }

    // Called every physics step (Unity FixedUpdate) - Velocity, Forces
    public virtual void PhysicsUpdate() { }

    // Called once when leaving the state
    public virtual void Exit() { }
}