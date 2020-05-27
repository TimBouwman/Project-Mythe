//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Animator))]
public class Match : Item
{
    #region Variables
    private Transform matchHead;
    private float oldPos;
    [SerializeField] private float matchHeadColliderRaduis;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float minSpeed;
    private Animator anim;
    private bool used = false;
    private Transform matchHeadIndex;
    [SerializeField] private Collider[] colliders; 
    #endregion

    #region Unity Methods
    private void Start()
    {
        matchHead = this.transform.GetChild(0);
        anim = this.GetComponent<Animator>();
        matchHeadIndex = new GameObject("Match Head Index").transform;
        matchHeadIndex.parent = this.transform;
    }

    private void Update()
    {
        MatchHeadHandler();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (matchHead != null)
            Gizmos.DrawSphere(matchHead.position, matchHeadColliderRaduis);
    }
    #endregion

    private void MatchHeadHandler()
    {
        colliders = Physics.OverlapSphere(matchHead.position, matchHeadColliderRaduis, layer);
        Debug.Log("1");
        if (colliders.Length > 0)
        {
            Debug.Log("2");
            MatchHeadIndexRelativeMovement(colliders[0].transform);
            if (Mathf.Abs(oldPos - matchHeadIndex.localPosition.x) > minSpeed && !used)
            {
                Debug.Log("3");
                anim.Play("Burning", -1);
                used = true;
            }
            oldPos = matchHeadIndex.localPosition.x;
        }
    }

    private void MatchHeadIndexRelativeMovement(Transform parent)
    {
        if (parent != matchHeadIndex.parent)
        {
            matchHeadIndex.parent = parent;
            matchHeadIndex.localRotation = Quaternion.identity;
        }
        matchHeadIndex.position = matchHead.position;
    }
}