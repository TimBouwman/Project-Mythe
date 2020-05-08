//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using Valve.VR;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class VRMovement : MonoBehaviour
{
    #region Variables
    [Header("Value's")]
    [SerializeField] private float speed;

    [Header("Actions")]
    [SerializeField] private SteamVR_Action_Vector2 moveInput;
    #endregion

    #region Unity Methods
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    #endregion
}