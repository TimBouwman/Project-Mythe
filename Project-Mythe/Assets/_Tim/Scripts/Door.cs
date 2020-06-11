//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    #region Variables
    [SerializeField] private int inputAmount = 1;
    private int currentInputAmount;
    public int CurrentInputAmount 
    { 
        set
        {
            currentInputAmount += value;
            if (currentInputAmount >= inputAmount)
                Open();
        } 
    }
    private Animator anim;
    #endregion

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Open()
    {
        anim.Play("Open", -1);
    }
}