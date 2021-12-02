using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This plays a secret music
/// script by: Camilo Zambrano
/// </summary>
public class SpecialMusic : MonoBehaviour
{
    /// <summary>
    /// This is the audio source where the sound
    /// will come from.
    /// </summary>
    private AudioSource _audioSource;

    private void Start()
    {
        GameManagerSingleton.instance.OnAnimationChange += ChangeAnimation;
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the secret animation on a special
    /// condition.
    /// </summary>
    /// <param name="animationName"> the animation name being played</param>
    private void ChangeAnimation(string animationName)
    {
        if (animationName == "Macarena")
        {
            _audioSource?.Play();
        }
    }
}
