//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Container : MonoBehaviour
{
    #region Variables
    [Tooltip("The id of the item the container can hold if this is empty the container can take every item")]
    [SerializeField] private string itemIdFilter = "";
    [Tooltip("If this is use the container will only be active if the parent item is being held")]
    [SerializeField] private Item parentItem;
    [Header("Overlap Sphere")]
    [Tooltip("The center of the overlap Sphere. If this is null the script will use this.transform in its place.")]
    [SerializeField] private Transform centerObject;
    [Tooltip("The raduis of the overlap sphere")]
    [SerializeField] private float raduis;
    [Tooltip("The layer the hand object is on")]
    [SerializeField] private LayerMask handLayer;
    [Tooltip("The layer the item object is on")]
    [SerializeField] private LayerMask itemLayer;
    [Header("array/list")]
    [SerializeField] private Transform[] itemPositions;
    [SerializeField] private List<Item> items = new List<Item>();
    /// <summary> The amount of items left in the container. </summary>
    private int currentItemAmount;
    private Item currentItem;
    private Transform currentItemIndex;
    private VRHandController hand;
    private bool empty = false;
    public static Action<Collider> releaseItem;
    #endregion

    #region Unity Methods
    private void Start()
    {
        //sets the center for the overlapsphere to this object
        //if it is not set in the inspector
        if (!centerObject)
            centerObject = this.transform;

        if (items.Count == 0)
            empty = true;

        //setting and creating everything for the current item
        currentItemAmount = items.Count;
        currentItemIndex = new GameObject("Item Index").transform;
        currentItemIndex.parent = this.transform;

        //disables all the colliders of the items in the container
        foreach (Item item in items)
        {
            item.GetComponent<Collider>().enabled = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }

        releaseItem = null;
        releaseItem += item => AddItem(item);
    }
    virtual protected void Update()
    {
        if ((parentItem == null || parentItem.beingheld) && !empty)
            RemoveItem();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (centerObject)
            Gizmos.DrawWireSphere(centerObject.position, raduis);
        else 
            Gizmos.DrawWireSphere(this.transform.position, raduis);
    }
    #endregion

    private void RemoveItem()
    {
        //set current item if current item equals null
        if (currentItem == null)
            currentItem = items[currentItemAmount - 1];

        //check if current item is picked up
        if (currentItem.beingheld)
        {
            hand = null;
            items.RemoveAt(currentItemAmount - 1);
            currentItem = null;
            currentItemAmount -= 1;
            if (!items.Any())
                empty = true;
            else currentItem = items[currentItemAmount - 1];
            return;
        }

        if (currentItem != null && currentItem.GetComponent<Collider>().enabled != false)
            currentItem.GetComponent<Collider>().enabled = false;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, raduis, handLayer);
        if (colliders.Length > 0)
        {
            if (hand == null)
                hand = colliders[0].GetComponent<VRHandController>();

            //set current item index in the correct position and rotation
            if (currentItemIndex.parent != hand)
            {
                currentItemIndex.parent = hand.transform;
                currentItemIndex.localPosition = hand.GetItemPos(currentItem);
                currentItemIndex.localRotation = hand.GetItemRot(currentItem);
            }

            //lerp the current item to the item index tranform
            items[currentItemAmount - 1].transform.Lerp(currentItemIndex, Time.deltaTime * 3);
            currentItem.GetComponent<Collider>().enabled = true;
        }
        else if (currentItem.transform.position != itemPositions[currentItemAmount - 1].position && currentItem.transform.rotation != itemPositions[currentItemAmount - 1].rotation)
            currentItem.transform.Lerp(itemPositions[currentItemAmount - 1], Time.deltaTime * 3);
    }

    private void AddItem(Collider item)
    {
        if (items.Count < itemPositions.Length)
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, raduis, itemLayer);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider == item)
                    {
                        Item newItem = item.GetComponent<Item>();
                        if (itemIdFilter == "" || itemIdFilter == newItem.Id.ToString())
                        {
                            currentItemAmount += 1;
                            items.Add(newItem);
                            currentItem = newItem;
                            item.GetComponent<Rigidbody>().isKinematic = true;
                            item.enabled = false;
                            item.transform.parent = this.transform;
                            if (empty)
                                empty = false;
                            return;
                        }
                    }
                }
            }
        }
    }
}