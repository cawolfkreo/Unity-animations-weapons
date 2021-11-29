using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This game manager is used by the UI, the player
/// and model as a bridge to control the different
/// objects in the scene. Since this object will
/// not be destroyed on load, it is important that
/// it doesn't duplicate when the scene changes and 
/// this is why this has to be a singleton across
/// the lifetime of the game.
/// script by: Camilo Zambrano
/// </summary>
public class GameManagerSingleton : MonoBehaviour
{
    [Header("Animation")]
    /// <summary>
    /// This is the animation name that the game
    /// manager told for the last time to the game
    /// object to begin playing.
    /// </summary>
    [Tooltip("This is the animation the game manager told to the model the last time.")]
    [SerializeField]
    private string animationToPlay;

    /// <summary>
    /// This is the action event used when we want
    /// to tell the model to change the animation.
    /// The model should subscribe for any updates
    /// and when the action is invoked, it will
    /// tell the object what animation to play.
    /// </summary>
    public Action<string> OnAnimationChange;

    /// <summary>
    /// This is the reference for the only allowed
    /// instance that this class could have during
    /// the lifecycle of the game.
    /// </summary>
    public static GameManagerSingleton instance;

    /// <summary>
    /// This is update is called when the script is
    /// loaded for the first time, it is used to
    /// set the singleton object necessary to move
    /// information between scenes as well as work
    /// as a "bridge" for the model's animation and
    /// the UI of the scenes.
    /// </summary>
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Oh I found somebody else! I guess I'll die");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            Debug.Log("Hey I'm the only one, sweet!");
        }
    }

    /// <summary>
    /// This method is called once before the first
    /// frame. It is used to set the initial animation
    /// for the model.
    /// </summary>
    void Start()
    {
        StartTheAnimation();
        SubscribeToSceneTransition();
    }

    /// <summary>
    /// This method should be used at the beginning
    /// of a scene to set the animation to play on
    /// the model.
    /// </summary>
    private void StartTheAnimation()
    {
        ChangeAnimation(animationToPlay);
    }

    /// <summary>
    /// This method is called With the name of the
    /// animation to change to. Thus, the manager
    /// should store the animation name and invoke
    /// the animation change action event.
    /// </summary>
    /// <param name="animationName">The name of the animation to play, it should have the same name as it's corresponded parameter.</param>
    public void ChangeAnimation(string animationName)
    {
        animationToPlay = animationName;
        OnAnimationChange?.Invoke(animationToPlay);
    }

    public void ChangeToSecondScene()
    {
        if (SceneManager.GetActiveScene().name != "Scene2")
        {
            _ = SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Single);
            Debug.Log("Loading next scene...");
        }
    }

    /// <summary>
    /// This method will subscribe the manager to
    /// the scene transition event, in order to
    /// detect when the scene has changed.
    /// </summary>
    private void SubscribeToSceneTransition()
    {
        SceneManager.sceneLoaded += SceneChanged;
    }

    /// <summary>
    /// This is the delegate used to listen to the
    /// scene changes in order to fire events and
    /// other functions.
    /// </summary>
    /// <param name="scene">The scene that loaded</param>
    /// <param name="mode">The mode used to load the scene</param>
    private void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        _ = StartCoroutine(InvokeAfterSceneChange());
    }

    /// <summary>
    /// Since the scene loaded event occurs before
    /// the Awake event, it is necessary to wait a
    /// single frame before firing the event.
    /// </summary>
    /// <returns></returns>
    IEnumerator InvokeAfterSceneChange()
    {
        yield return 0;

        OnAnimationChange?.Invoke(animationToPlay);
    }
}
