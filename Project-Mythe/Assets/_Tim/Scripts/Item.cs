//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Item : MonoBehaviour
{
    #region Variables
    [SerializeField, HideInInspector] private Vector3 position;
    public Vector3 Position { get { return this.position; } }
    [SerializeField, HideInInspector] private Quaternion rotation;
    public Quaternion Rotation { get { return this.rotation; } }
    [SerializeField, HideInInspector] private AnimationClip handPose;
    public AnimationClip HandPose { get { return this.handPose; } }
    private Rigidbody rb;
    public Rigidbody rigidbody { get { return this.rb; } }
    #endregion

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    public void SetPosition()
    {
        position = this.transform.localPosition;
    }
    public void SetRotation()
    {
        rotation = this.transform.localRotation;
    }
}