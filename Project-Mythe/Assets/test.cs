//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class test : MonoBehaviour
{
    #region Variables
    
    #endregion

    #region Unity Methods
    private void Start()
    {
        
    }

    private void Update()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.right;
    }
    #endregion
}