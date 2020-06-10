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
    [SerializeField, HideInInspector] private int id;
    public int Id { get { return this.id; } }
    [SerializeField, HideInInspector] private Vector3 position;
    public Vector3 Position { get { return this.position; } }
    [SerializeField, HideInInspector] private Quaternion rotation;
    public Quaternion Rotation { get { return this.rotation; } }
    [SerializeField, HideInInspector] private string handPose;
    public string HandPose { get { return this.handPose; } }
    protected Rigidbody rb;
    public Rigidbody Rigidbody { get { return this.rb; } }
    [HideInInspector] public bool beingheld;
    [HideInInspector] public GameObject hand;
    #endregion
    
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    virtual protected void Grab()
    {
        rb.isKinematic = true;
        beingheld = true;
    }
    virtual protected void Release()
    {
        this.transform.parent = null;
        rb.isKinematic = false;
        beingheld = false;
        hand = null;
    }

    #region Editor Methods
    public void SetPosition()
    {
        position = this.transform.localPosition;
        Debug.Log("Position set to: " + position);
    }
    public void SetRotation()
    {
        rotation = this.transform.localRotation;
        Debug.Log("Rotation set to: " + rotation);
    }
    public void ResetPosRot()
    {
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        Debug.Log(transform.name + " is reset.");
    }
    #endregion
}