using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Stats")]
    public float MoveSpeed = 10f;
    public float Acceleration = 5f; // How fast we reach max speed
    public float Decceleration = 5f; // How fast we stop

    [Header("Jump Stats")]
    public float JumpForce = 16f;
    public float VariableJumpHeightMultiplier = 0.5f; // For "short hops"

    [Header("Mechanics & Abilities")]
    public int MaxJumpCount = 1; // Set to 2 for Double Jump
    public bool CanWallJump = false;
    public bool CanDash = false;

    [Header("Physics")]
    public float FallGravityMult = 1f;
    public float FastFallGravityMult = 2f; // Heavier gravity when falling feels better
}
