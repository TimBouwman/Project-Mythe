//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ActiveRagdollHand : MonoBehaviour
{
    #region Variables
    [Tooltip("Overlap Sphere")]
    [SerializeField] private Vector3 center;
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private LayerMask environmentLayer;
    private LayerMask layer;
    Collider[] colliders;
    [Tooltip("Objects")]
    [SerializeField] private Transform HandLocation;
    [SerializeField] private Transform turnIndex;
    private Rigidbody rb;
    #endregion

    #region Unity Methods
    private void Start()
    {
        layer = itemLayer + environmentLayer;
        rb = this.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        GrabHandler();
    }
    private void LateUpdate()
    {
        UpdateHandPos();
        UpdateHandRot();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position + center, radius);
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// 
    /// </summary>
    private void UpdateHandPos()
    {
        this.transform.position = HandLocation.position;
    }
    /// <summary>
    /// 
    /// </summary>
    private void UpdateHandRot()
    {
        //this.transform.rotation = HandLocation.rotation;
    }
    /// <summary>
    /// 
    /// </summary>
    private void GrabHandler()
    {
        colliders = Physics.OverlapSphere(this.transform.position + center, radius, layer);
        foreach (Collider collider in colliders)
        {
            turnIndex.LookAt(collider.ClosestPoint(this.transform.position));
            float x = Vector3.Distance(collider.ClosestPoint(this.transform.position), this.transform.position);
            Debug.Log(x);
            float t = x / radius;
            this.transform.rotation = Quaternion.Lerp(collider.transform.rotation, HandLocation.rotation, t);
        }
    }
    #endregion
}