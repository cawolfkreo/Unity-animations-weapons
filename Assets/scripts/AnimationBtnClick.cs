using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This manages the button click event. it will
/// send the specified animation to the Game
/// Manager when the button is clicked by a user.
/// script by: Camilo Zambrano
/// </summary>
public class AnimationBtnClick : MonoBehaviour
{
    [Header("Click Information")]

    /// <summary>
    /// This is the name of the animation state this button will transition to.
    /// </summary>
    [SerializeField]
    [Tooltip("This is the name of the animation state this button will transition to.")]
    private string animationName;

    // Start is called before the first frame update
    void Start()
    {
        var animButton = GetComponent<Button>();
        animButton.onClick.AddListener(ButtonClicked);
    }

    /// <summary>
    /// We will send the new animation state to the
    /// Game Manager when the user clicks the 
    /// button.
    /// </summary>
    private void ButtonClicked()
    {
        GameManagerSingleton.instance.ChangeAnimation(animationName);
    }
}
