//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;
using Valve.VR;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ActiveRagdollHand : MonoBehaviour
{
    #region Variables
    [Header("OverlapSphere")]
    [SerializeField] private Vector3 center;
    [Space(7f)]
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float environmentRadius = 0.2f;
    [Space(7f)]
    [SerializeField] private LayerMask itemLayer;
    [SerializeField] private float itemRadius = 0.1f;
    private LayerMask layer;

    [Header("Objects")]
    [SerializeField] private Transform controller;
    [Tooltip("This is the object that will rotate to the nearest collider but it is not the object the rotation will be read from")]
    [SerializeField] private Transform turnIndexHolder;
    [Tooltip("This is the object the rotation for the hand is read from")]
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position + center, environmentRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + center, itemRadius);
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// 
    /// </summary>
    private void UpdateHandPos()
    {
        this.transform.position = controller.position;
    }
    /// <summary>
    /// 
    /// </summary>
    private void UpdateHandRot()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + center, environmentRadius, environmentLayer);
        if (colliders.Length > 0)
        {
            //Look for the closest point inside OverlapSphere
            float minDistance = environmentRadius;
            Collider closestCollider = colliders[0];
            foreach (Collider collider in colliders)
            {
                //get the 2 points
                Vector3 a = this.transform.position;
                Vector3 b = collider.ClosestPoint(a);
                //compare the distance between point a and point b
                if ((a - b).sqrMagnitude < minDistance * minDistance)
                {
                    //if the distance is smaller than minDistance save that value as the new minDistance
                    //and same the collider as closestCollider
                    minDistance = Vector3.Distance(a, b);
                    closestCollider = collider;
                }
            }
            //turn object to the closest collider for a reference rotation 
            turnIndexHolder.LookAt(closestCollider.ClosestPoint(this.transform.position));
            //get current distance between the hand and the closest collider
            float currentDistance = Vector3.Distance(closestCollider.ClosestPoint(this.transform.position), this.transform.position);
            //transition from the controller rotation to the turn idex rotation depending on
            //the current distance between the hand and the collider
            float i = currentDistance / environmentRadius;
            Quaternion newRotation = Quaternion.Lerp(turnIndex.transform.rotation, controller.rotation, i);

            Vector3 eulerRotation = new Vector3(newRotation.eulerAngles.x, controller.rotation.eulerAngles.y, newRotation.eulerAngles.z);

            this.transform.rotation = Quaternion.Euler(eulerRotation);
        }
        else this.transform.rotation = controller.rotation;
    }
    /// <summary>
    /// 
    /// </summary>
    private void GrabHandler()
    {

    }
    #endregion
}