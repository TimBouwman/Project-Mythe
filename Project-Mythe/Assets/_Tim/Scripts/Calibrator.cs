//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

/// <summary>
/// this class Calibrates the height of the player to a certain height in the game so that all players are the same hight
/// </summary>
public class Calibrator : MonoBehaviour
{
    #region Variables
    [Header("Value's")]
    [Range(0,2)]
    [Tooltip("The height that you want your player to be")]
    [SerializeField] private float targetheight = 1.8f;

    [Header("actions")]
    /// <summary> The action that needs to be called on both hands inorder to calibrate the player </summary>
    [SerializeField] private SteamVR_Action_Boolean calibrate;

    [Header("Objects")]
    [Tooltip("The object that should be resized")]
    [SerializeField] private Transform player;
    [Tooltip("The head of the player or another indicator of there height (this object must be a child of the player object)")]
    [SerializeField] private Transform head;
    /// <summary> The object that holds the ui elements with the player instructions </summary>
    private GameObject canvas;
    public static Action<Vector3> playerheight;
    #endregion

    #region Unity Methods
    private void Start()
    {
        canvas = this.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        //waits for both actions the removes the intructions from the screen and starts the calibration
        if(calibrate.GetState(SteamVR_Input_Sources.LeftHand) && calibrate.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            canvas.active = false;
            CalibrarePlayerheight();
        } 
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Checks if the player object needs to grow or shrink. Then sends the new scale with an event.
    /// After that it loads the next scene.
    /// </summary>
    private void CalibrarePlayerheight()
    {
        if(head.position.y > targetheight)
        {
            while (head.position.y > targetheight)
            {
                UpdatePlayerheight(-0.05f);
            }
        }  
        else
        {
            while (head.position.y < targetheight)
            {
                UpdatePlayerheight(0.05f);
            }
        }
            
        playerheight(player.localScale);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Scales the player object up or down depening on the parameter.
    /// </summary>
    /// <param name="increment">This amount is added to the player objects localscale.</param>
    private void UpdatePlayerheight(float increment)
    {
        Vector3 scaleUpdate =  new Vector3(increment, increment, increment);
        player.localScale += scaleUpdate;
    }
    #endregion
}