using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will manage the character dance using the
/// animation state machine.
/// script by: Camilo Zambrano
/// </summary>
[RequireComponent(typeof(AnimationStateMachine))]
public class CharacterDance : MonoBehaviour
{
    [Header("Animation")]

    /// <summary>
    /// This is the list of animation states names.
    /// This list should include all animation
    /// state variables from the animator
    /// controller.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the list of animation states names. This list should include all animation states variables from the animator controller.")]
    private string[] animationNames;

    /// <summary>
    /// This is the animation state machine for
    /// this model. It will be used to change the
    /// animation states programatically.
    /// </summary>
    private AnimationStateMachine _animationState;

    private void Awake()
    {
        _animationState = GetComponent<AnimationStateMachine>();
    }

    /// <summary>
    /// Start is called before the first frame
    /// update. Here we subscribe to the animation
    /// Action Event and setup the initial state.
    /// </summary>
    void Start()
    {
        GameManagerSingleton.instance.OnAnimationChange += ChangeAnimation;
    }

    /// <summary>
    /// When the object is destroyed we need to
    /// unsubscribe from the animation change event.
    /// </summary>
    private void OnDestroy()
    {
        GameManagerSingleton.instance.OnAnimationChange -= ChangeAnimation;
    }

    /// <summary>
    /// This should change the internal state for
    /// the animation if the animation name is part
    /// of the list of animations.
    /// </summary>
    /// <param name="animationName">The new animation state</param>
    private void ChangeAnimation(string animationName)
    {
        bool isAValidName = false;
        foreach (string animName in animationNames)
        {
            if (animName.Equals(animationName))
            {
                isAValidName = true;
                break;
            }
        }
        if (!isAValidName)
            return;

        Debug.Log("changing the anim state...");
        _animationState.ChangeState(animationName);
    }
}
