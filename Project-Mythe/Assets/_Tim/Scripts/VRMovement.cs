//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using Valve.VR;

/// <summary>
/// This class is resposeble for the movement of the player 
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class VRMovement : MonoBehaviour
{
    #region Variables
    [Header("Value's")]
    [Tooltip("The speed with which the player moves")]
    [SerializeField] private float speed = 2f;
    private Vector3 velocity;

    [Header("Actions")]
    /// <summary> The action that is intended to be used to move the player object. This can be set to any Vector2 action <summary>
    [SerializeField] private SteamVR_Action_Vector2 moveInput;
    [Tooltip("The device the Move Input Action should be registered on")]
    [SerializeField] private SteamVR_Input_Sources moveInputSource = SteamVR_Input_Sources.LeftHand;

    [Space(13f)]
    /// <summary> The action that is intended to be used to turn the player object to the left. This can be set to any Boolean action <summary>
    [SerializeField] private SteamVR_Action_Boolean turnLeft;
    /// <summary> The action that is intended to be used to turn the player object to the right. This can be set to any Boolean action <summary>
    [SerializeField] private SteamVR_Action_Boolean turnRight;
    [Tooltip("The device the Turn Input Actions should be registered on")]
    [SerializeField] private SteamVR_Input_Sources turnInputSource = SteamVR_Input_Sources.RightHand;

    [Header("Objects")]
    [SerializeField] private Transform head;
    [SerializeField] private Transform cameraRig;
    private CharacterController cc;
    #endregion

    #region Unity Methods
    private void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }
    private void Update()
    {
        RotationHandler();
        BodyHandler();
        MovementHandler();
        GravityHandler();
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// 
    /// </summary>
    private void RotationHandler()
    {
        Vector3 oldPos = cameraRig.position;
        Quaternion oldRot = cameraRig.rotation;

        this.transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        cameraRig.position = oldPos;
        cameraRig.rotation = oldRot;
    }

    private void MovementHandler()
    {
        if(moveInput.GetAxis(moveInputSource) != Vector2.zero)
        {
            Vector2 input = moveInput.GetAxis(moveInputSource);
            Vector3 move = this.transform.right * input.x + transform.forward * input.y;
            cc.Move(move * speed * Time.deltaTime);
        }
    }

    private void GravityHandler()
    {
        //this if statment makes sure you stop exelorating when you on the ground
        if (cc.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        //Δy = ½g·t²
        velocity += Physics.gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    private void BodyHandler()
    {
        float headHight = Mathf.Clamp(head.localPosition.y, 1, 2);
        cc.height = headHight;

        Vector3 center = Vector3.zero;
        center.y = cc.height / 2;
        center.y += cc.skinWidth;

        center.x = head.localPosition.x;
        center.z = head.localPosition.z;

        center = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * center;

        cc.center = center;
    }
    #endregion
}