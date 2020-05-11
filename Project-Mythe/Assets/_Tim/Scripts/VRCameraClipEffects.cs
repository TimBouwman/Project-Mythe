//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioReverbZone))]
[RequireComponent(typeof(Volume))]
public class VRCameraClipEffects : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform head;
    [SerializeField] private AudioReverbPreset reverbPreset;
    private AudioReverbZone reverbZone;
    private AudioReverbPreset oldReverbPreset;
    private Volume volume;
    #endregion

    #region Unity Methods
    private void Start()
    {
        reverbZone = this.GetComponent<AudioReverbZone>();
        oldReverbPreset = reverbZone.reverbPreset;
        volume = this.GetComponent<Volume>();
        volume.weight = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("t");
        reverbZone.reverbPreset = this.reverbPreset;
        volume.weight = 1;
    }
    private void OnCollisionExit(Collision collision)
    {
        reverbZone.reverbPreset = oldReverbPreset;
        volume.weight = 0;
    }
    #endregion
}