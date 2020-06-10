//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BackPack : Item
{
    [SerializeField] private GameObject model;
    [SerializeField] private Transform back;

    protected override void Grab()
    {   
        model.SetActive(true);
        rb.isKinematic = true;
        beingheld = true;
    }
    protected override void Release()
    {
        model.SetActive(false);
        this.transform.parent = back;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        beingheld = false;
        hand = null;
    }
}