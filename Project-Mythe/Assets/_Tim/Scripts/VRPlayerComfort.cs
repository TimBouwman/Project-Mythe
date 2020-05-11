//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using UnityEngine.Rendering;
using Valve.VR;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Volume))]
public class VRPlayerComfort : MonoBehaviour
{
    #region Variables
    public enum TurnModes
    {
        SNAP45,
        SNAP90,
        SMOOTH
    }
    private static TurnModes TURN_MODES;
    private static bool USE_VIGNETTE;
    private Volume volume;
    /// <summary> The action that is intended to be used to move the player object. This can be set to any Vector2 action. <summary>
    [SerializeField] private SteamVR_Action_Vector2 moveInput;
    #endregion

    #region Unity Methods
    private void Start()
    {
        volume = this.GetComponent<Volume>();
    }

    private void Update()
    {
        VignetteHandler();
    }
    #endregion

    private void VignetteHandler()
    {
        Vector2 input = moveInput.GetAxis(SteamVR_Input_Sources.Any);
        if (input.x > input.y)
            volume.weight = Mathf.Abs(input.x);
        else
            volume.weight = Mathf.Abs(input.y);
    }
}