using UnityEngine;

// This script requires an Animator component on the same GameObject.
// It provides functions to control animations and set variables in the Animator.
[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    // Reference to the Animator component
    private Animator _animator;

    private void Awake()
    {
        // Awake is called when the script instance is being loaded.
        // Here, we fetch the Animator component attached to the same GameObject.
        _animator = GetComponent<Animator>();
    }

    // Plays a specific animation by name.
    public void PlayAnimation(string animationName)
    {
        // Triggers the animation using the given animation name.
        // "Base Layer" is the default layer in Unity's Animator system.
        _animator.Play($"Base Layer.{animationName}");
    }

    // Sets a float parameter in the Animator.
    public void SetVariable(string variableName, float variableValue)
    {
        // Updates the float parameter with the provided name and value.
        _animator.SetFloat(variableName, variableValue);
    }

    // Sets an integer parameter in the Animator.
    public void SetVariable(string variableName, int variableValue)
    {
        // Updates the integer parameter with the provided name and value.
        _animator.SetInteger(variableName, variableValue);
    }

    // Sets a boolean parameter in the Animator.
    public void SetVariable(string variableName, bool variableValue)
    {
        // Updates the boolean parameter with the provided name and value.
        _animator.SetBool(variableName, variableValue);
    }
}
