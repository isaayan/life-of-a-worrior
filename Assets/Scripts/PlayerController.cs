using UnityEngine;

// This script handles the player's movement, jumping, flipping, and animations.
public class PlayerController : MonoBehaviour
{
    // Movement and control variables
    private float _horizontal; // Stores horizontal input
    private float _speed = 8f; // Speed of the player's movement
    private float _jumpPower = 16f; // Force applied when the player jumps
    private bool _isFacingRight = true; // Tracks whether the player is facing right

    // Serialized fields to link components and settings in the Inspector
    [SerializeField] private Rigidbody2D _rigidbody; // Player's Rigidbody2D for physics
    [SerializeField] private Transform _groundCheck; // Position to check if the player is on the ground
    [SerializeField] private LayerMask _groundLayer; // Layer that defines what is considered ground

    [Space(10)]
    [SerializeField] private float _fireRate; // Cooldown time between attacks

    private float _nextFire; // Tracks when the player can attack again

    // Reference to the custom AnimatorController script
    private AnimatorController _animatorController;

    private void Awake()
    {
        // Find and assign the AnimatorController component from the player's child objects
        _animatorController = GetComponentInChildren<AnimatorController>();
    }

    private void Update()
    {
        // Retrieve horizontal input (e.g., A/D keys or Left/Right arrow keys)
        _horizontal = Input.GetAxisRaw("Horizontal");

        // Handle jumping when the Jump button is pressed and the player is on the ground
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.linearVelocity = new(_rigidbody.linearVelocity.x, _jumpPower);
        }

        // If the Jump button is released mid-air, reduce the jump height for a short-hop effect
        if (Input.GetButtonUp("Jump") && _rigidbody.linearVelocity.y > 0f)
        {
            _rigidbody.linearVelocity = new(_rigidbody.linearVelocity.x, _rigidbody.linearVelocity.y * 0.5f);
        }

        // Handle attack when the F key is pressed, the player is on the ground, and enough time has passed since the last attack
        if (Input.GetKeyDown(KeyCode.F) && IsGrounded() && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate; // Update the next allowed attack time
            _animatorController.PlayAnimation("Attack"); // Trigger the attack animation
        }

        // Update animations based on the player's state
        HandleAnimations();

        // Flip the player sprite based on movement direction
        Flip();
    }

    private void FixedUpdate()
    {
        // Set the player's velocity based on horizontal input
        // This applies smooth movement through Unity's physics system
        _rigidbody.linearVelocity = new(_horizontal * _speed, _rigidbody.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        // Check if the player is touching the ground by detecting overlap with the specified ground layer
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        // Flip the player's sprite horizontally when changing direction
        if (_isFacingRight && _horizontal < 0f || !_isFacingRight && _horizontal > 0f)
        {
            _isFacingRight = !_isFacingRight; // Toggle the direction

            // Reverse the player's horizontal scale to flip the sprite
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void HandleAnimations()
    {
        // Update the Animator variables to match the player's state
        _animatorController.SetVariable("Speed", _horizontal); // Speed for running animation
        _animatorController.SetVariable("Jump", _rigidbody.linearVelocity.y); // Vertical velocity for jump animations
        _animatorController.SetVariable("IsGrounded", IsGrounded()); // Grounded status for grounded animations
    }
}
