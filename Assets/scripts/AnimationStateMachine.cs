using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the state machine made to manage the
/// animation controller values and to sync the
/// internal state of the character to the
/// animation controller state, this way we can
/// programatically control the animation state.
/// script by: Camilo Zambrano
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimationStateMachine : MonoBehaviour
{
    /// <summary>
    /// This flag is set to indicate wether or not
    /// the machine has been initialized.
    /// </summary>
    private bool _initialized;

    /// <summary>
    /// This is the current state of the animation.
    /// it will change when we want to change the state
    /// of the animation.
    /// </summary>
    private IAnimationState currentState;

    /// <summary>
    /// This will start the machine with the
    /// initial state passed.
    /// </summary>
    /// <param name="animationState">The initial state for the machine</param>
    private void StartMachine(string animationState)
    {
        Animator animator = GetComponent<Animator>();
        currentState = new AnimationState();
        currentState.StartState(animationState, animator);
        currentState.StartAnimation();
        _initialized = true;
    }

    /// <summary>
    /// This method will update the animation state
    /// with the new one passed.
    /// </summary>
    /// <param name="newAnimationName">the new state for the animation.</param>
    public void ChangeState(string newAnimationName)
    {
        if (!_initialized)
        {
            StartMachine(newAnimationName);
        }
        else
        {
            currentState = currentState.ChangeAnimationState(newAnimationName);
        }
    }
}

/// <summary>
/// This is the interface for what an animation
/// state should implement.
/// </summary>
public interface IAnimationState
{
    /// <summary>
    /// This sets the state for the object. It
    /// should be used when an instance of the
    /// class is created.
    /// </summary>
    /// <param name="animationName">The current animation state.</param>
    public void StartState(string animationName, Animator animator);

    /// <summary>
    /// This will set the animation state in the
    /// animation controller to the one we want
    /// programatically.
    /// </summary>
    public void StartAnimation();

    /// <summary>
    /// This will stop the animation that is being
    /// played and will set the state to a new
    /// animation state. It will return the new
    /// animation state for the model.
    /// </summary>
    public IAnimationState ChangeAnimationState(string newAnimationName);
}

/// <summary>
/// This is an animation state for the state
/// machine. it represents the current animation
/// that is being played for the model.
/// </summary>
public class AnimationState : IAnimationState
{
    /// <summary>
    /// This is the current animation that should
    /// be played by the animator.
    /// </summary>
    private string _currentAnimation;

    /// <summary>
    /// This is the animator of the object. It will
    /// be used to set the state of the animator
    /// controller for the model.
    /// </summary>
    private Animator _animator;

    public void StartState(string animationName, Animator animator)
    {
        _currentAnimation = animationName;
        _animator = animator;
    }

    public void StartAnimation()
    {
        _animator.SetBool(_currentAnimation, true);
    }
    
    public IAnimationState ChangeAnimationState(string newAnimationName)
    {
        if (!ShouldWeChangeAnimation(newAnimationName))
        {
            return this;
        }

        StopAnimation();

        IAnimationState newState = new AnimationState();
        newState.StartState(newAnimationName, _animator);
        newState.StartAnimation();
        return newState;
    }

    /// <summary>
    /// This will stop the current animation before
    /// the animation state transitions to a new
    /// animation state.
    /// </summary>
    private void StopAnimation()
    {
        _animator.SetBool(_currentAnimation, false);
    }


    /// <summary>
    /// This decides if it is necessary to change 
    /// the animation state or not.
    /// </summary>
    /// <param name="animationName">The new animation state</param>
    /// <returns><code>true</code> if we need to change state, <code>false</code> otherwise</returns>
    private bool ShouldWeChangeAnimation(string animationName)
    {
        return _currentAnimation != animationName;
    }
}