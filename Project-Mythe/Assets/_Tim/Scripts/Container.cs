//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Container : MonoBehaviour
{
    #region Variables
    [Header("Overlap Sphere")]
    [SerializeField] private Transform centerObject;
    [SerializeField] private float raduis;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform[] itemPositions;
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    private int currentItemAmount;
    private Item currentItem;
    private Transform  currentItemIndex;
    private VRHandController hand;
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (!centerObject)
            centerObject = this.transform;
        currentItemAmount = itemPositions.Length;
        items.RemoveAt(currentItemAmount - 1);

        currentItemIndex = new GameObject("Item Index").transform;
    }

    private void Update()
    {
        HandDetector();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (centerObject)
            Gizmos.DrawWireSphere(centerObject.position, raduis);
        else Gizmos.DrawWireSphere(this.transform.position, raduis);
    }
    #endregion

    private void HandDetector()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, raduis, layer);
        if (colliders.Length > 0)
        {
            if (hand == null)
                hand = colliders[0].GetComponent<VRHandController>();
            if (currentItem == null)
                currentItem = items[currentItemAmount - 1].GetComponent<Item>();
            if (currentItemIndex.parent == null)
            {
                currentItemIndex.parent = colliders[0].transform;
                currentItemIndex.localPosition = hand.GetItemPos(currentItem);
                currentItemIndex.localRotation = hand.GetItemRot(currentItem);
            }

        }
    }
}