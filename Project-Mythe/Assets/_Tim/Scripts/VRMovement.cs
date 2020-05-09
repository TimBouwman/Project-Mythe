//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using Valve.VR;

/// <summary>
/// This class is resposeble for moving and rotating the player object.
/// it also makes sure the collider gets smaller when the player ducks down and keeps the collider underneath the plays head.
/// 
/// This script is made to be use with the SteamVR CameraRig Prefab
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class VRMovement : MonoBehaviour
{
    #region Variables
    [Header("Value's")]
    [Tooltip("The speed with which the player moves")]
    [SerializeField] private float speed = 2f;
    [Tooltip("The amount the player rotates when using the snap rotation")]
    [SerializeField] private float snapIncrement = 45f;
    /// <summary> Velocity is the speed that gets build up while the plays is falling. if the player is grounded it get set to 0.0f, -2.0f, 0.0f. </summary>
    private Vector3 velocity;

    [Header("Actions")]
    /// <summary> The action that is intended to be used to move the player object. This can be set to any Vector2 action. <summary>
    [SerializeField] private SteamVR_Action_Vector2 moveInput;
    [Tooltip("The device the Move Input Action should be registered on")]
    [SerializeField] private SteamVR_Input_Sources moveInputSource = SteamVR_Input_Sources.LeftHand;

    [Space(10f)]
    /// <summary> The action that is intended to be used to turn the player object to the left. This can be set to any Boolean action. <summary>
    [SerializeField] private SteamVR_Action_Boolean turnLeftInput;
    [Tooltip("The device the Turn Left Input Actions should be registered on")]
    [SerializeField] private SteamVR_Input_Sources turnLeftInputSource = SteamVR_Input_Sources.RightHand;

    [Space(10f)]
    /// <summary> The action that is intended to be used to turn the player object to the right. This can be set to any Boolean action. <summary>
    [SerializeField] private SteamVR_Action_Boolean turnRightInput;
    [Tooltip("The device the Turn Right Input Actions should be registered on")]
    [SerializeField] private SteamVR_Input_Sources turnRightInputSource = SteamVR_Input_Sources.RightHand;

    [Header("Objects")]
    [SerializeField] private Transform head;
    private CharacterController cc;
    #endregion

    #region Unity Methods
    private void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }
    private void Update()
    {
        BodyHandler();
        MovementHandler();
        GravityHandler();
        SnapRotation();
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// This method is resposeble for keeping the collider underneath the players head
    /// and making it smaller when the player ducks down.
    /// </summary>
    private void BodyHandler()
    {
        //Sets the hight of the collider to the hight of the players head with a clamp for 1 to 2
        float headHight = Mathf.Clamp(head.localPosition.y, 1, 2);
        cc.height = headHight;

        //resets the center
        Vector3 center = Vector3.zero;
        center.y = cc.height / 2;
        center.y += cc.skinWidth;

        //keeps the collider underneath the players head
        center.x = head.localPosition.x;
        center.z = head.localPosition.z;

        cc.center = center;
    }
    /// <summary>
    /// This method handels the movement of the player it does this reletive to the rotation of the head.
    /// </summary>
    private void MovementHandler()
    {
        if(moveInput.GetAxis(moveInputSource) != Vector2.zero)
        {
            Vector2 input = moveInput.GetAxis(moveInputSource);
            Vector3 move = head.right * input.x + head.forward * input.y;
            cc.Move(move * speed * Time.deltaTime);
        }
    }
    /// <summary>
    /// Appleys gravity to the player object it uses this formula Δy = ½g·t² to calculate the increasing velocity when falling.
    /// </summary>
    private void GravityHandler()
    {
        //this if statment makes sure you stop exelorating when you on the ground
        if (cc.isGrounded && velocity.y < 0f)
            velocity.y = -2f;
        else //increasing velocity while faling
        {
            velocity += Physics.gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
        }
    }
    /// <summary>
    /// Turns the player a certain amount to the left or to the right.
    /// when the if statement is true the player object rotates around the head object so that the player collider
    /// stays in the same position and does not clip through another object
    /// </summary>
    private void SnapRotation()
    {
        if (turnLeftInput.GetStateDown(turnLeftInputSource))
            this.transform.RotateAround(head.position, Vector3.up, -Mathf.Abs(snapIncrement));
        if (turnRightInput.GetStateDown(turnRightInputSource))
            this.transform.RotateAround(head.position, Vector3.up, Mathf.Abs(snapIncrement));
    }
    #endregion
}