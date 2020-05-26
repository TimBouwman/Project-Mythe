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
    private int frams;
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
        Collider[] colliders = Physics.OverlapSphere(matchHead.position, matchHeadColliderRaduis, layer);

        if (colliders.Length > 0)
        {
            MatchHeadIndexRelativeMovement(colliders[0].transform);
            if (Mathf.Abs(oldPos - matchHeadIndex.position.x) > minSpeed && !used)
            {
                frams++;
                //if(frams > 5)
                {
                    anim.Play("MatchLight", -1);
                    used = true;
                }   
            }
            oldPos = matchHeadIndex.position.x;
        }
        else if (frams != 0) frams = 0;
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