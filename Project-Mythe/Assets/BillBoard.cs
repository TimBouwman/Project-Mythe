//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BillBoard : MonoBehaviour
{
    #region Variables
    private Transform camera;
    #endregion

    #region Unity Methods
    private void Start()
    {
        this.camera = GameObject.FindWithTag("PlayerHead").transform;
    }

    private void Update()
    {
        this.transform.LookAt(camera);
    }
    #endregion
}