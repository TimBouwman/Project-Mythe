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
    [SerializeField] private Item parentItem;
    [Header("Overlap Sphere")]
    [SerializeField] private Transform centerObject;
    [SerializeField] private float raduis;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform[] itemPositions;
    [SerializeField] private List<Item> items = new List<Item>();
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

        currentItemIndex = new GameObject("Item Index").transform;
        currentItem = items[currentItemAmount - 1];

        foreach (Item item in items)
        {
            item.GetComponent<Collider>().enabled = false;
        }
    }

    private void Update()
    {
        if (parentItem.beingheld)
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
            if (!hand.IsHolding)
            {
                Debug.Log("Here");
                if (currentItem == null)
                    currentItem = items[currentItemAmount - 1];

                //set current item index in the correct pos and rot
                if (currentItemIndex.parent == null)
                {
                    Debug.Log("Here");
                    currentItemIndex.parent = colliders[0].transform;
                    currentItemIndex.localPosition = hand.GetItemPos(currentItem);
                    currentItemIndex.localRotation = hand.GetItemRot(currentItem);
                }

                //lerp the current item to the item index tranform
                items[currentItemAmount - 1].transform.Lerp(currentItemIndex, Time.deltaTime * 2);
                currentItem.GetComponent<Collider>().enabled = false;

                //check if current item is grabbed
                if (currentItem.beingheld)
                {
                    Debug.Log("Here");
                    hand = null;
                    items.RemoveAt(currentItemAmount - 1);
                    currentItem.GetComponent<Rigidbody>().isKinematic = false;
                    currentItemAmount--;
                    currentItem = items[currentItemAmount - 1].GetComponent<Item>();
                }
            }
        }
        else if (currentItem.transform.position != itemPositions[currentItemAmount - 1].position && currentItem.transform.rotation != itemPositions[currentItemAmount - 1].rotation)
            currentItem.transform.Lerp(itemPositions[currentItemAmount - 1], Time.deltaTime * 2);
    }
}