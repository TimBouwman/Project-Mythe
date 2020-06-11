//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
public class PressurePlate : MonoBehaviour
{
    #region Variables
    [SerializeField] private Door door;
    private Collider standingObject;
    private Animator anim;
    #endregion

    #region Unity Methods
    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 || other.gameObject.layer == 15)
        {
            print("test");
            door.CurrentInputAmount = 1;
            standingObject = other;
            anim.SetFloat("Speed", 1f);
            anim.Play("PressurePlate", -1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == standingObject)
        {
            standingObject = null;
            door.CurrentInputAmount = -1;
            anim.SetFloat("Speed", -1f);
            anim.Play("PressurePlate", -1);
        }
    }
    #endregion
}