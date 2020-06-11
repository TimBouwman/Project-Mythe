using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPosition;
    [SerializeField] private int colliderCheckLayerInt;
    [SerializeField] private string ignoreCollisionTag;
    [SerializeField] private bool needsToBeChild;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == colliderCheckLayerInt && collision.gameObject.tag != ignoreCollisionTag)
        {
            if(needsToBeChild)
            {
                transform.SetParent(respawnPosition);
            }
            transform.position = respawnPosition.position;
            transform.Rotate(0, 0, 0);
        }
    }

}
