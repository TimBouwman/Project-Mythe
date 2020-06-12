using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
public class restartGame : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean calibrate;
    void Update()
    {
        if (calibrate.GetState(SteamVR_Input_Sources.LeftHand) && calibrate.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            VRHandController.releaseItem = null;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
