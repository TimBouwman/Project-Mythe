//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

/// <summary>
/// 
/// </summary>
public class Calibrator : MonoBehaviour
{
    #region Variables
    [Header("Value's")]
    [Range(0,2)]
    [SerializeField] private float targetHight;

    [Header("actions")]
    [SerializeField] private SteamVR_Action_Boolean calibrate;

    [Header("Objects")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform head;
    private GameObject canvas;
    public static Action<Vector3> playerHight;
    #endregion

    #region Unity Methods
    private void Start()
    {
        canvas = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if(calibrate.GetState(SteamVR_Input_Sources.LeftHand) && calibrate.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            canvas.active = false;
            CalibrarePlayerHight();
        } 
    }
    #endregion

    private void CalibrarePlayerHight()
    {
        if(head.position.y > targetHight)
            while (head.position.y > targetHight)
            {
                UpdatePlayerHight(-0.05f);
                Debug.Log("down");
            }
        else
            while (head.position.y < targetHight)
            {
                UpdatePlayerHight(0.05f);
                Debug.Log("up");
            }
        playerHight(player.localScale);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void UpdatePlayerHight(float increment)
    {
        Vector3 scaleUpdate =  new Vector3(increment, increment, increment);
        player.localScale += scaleUpdate;
    }
}