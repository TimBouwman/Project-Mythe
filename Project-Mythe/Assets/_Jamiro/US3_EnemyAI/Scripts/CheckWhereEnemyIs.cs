using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CheckWhereEnemyIs : MonoBehaviour
{
    [SerializeField] private float angleEnemyNeedsToBeIn = 0;
    [SerializeField] private Transform enemyHead = null;
    [SerializeField] private PlayerDeath playerIsDeath = null;
    private float angle = 0;
    private Vector3 targetDir = new Vector3();
    private int layerMask = 1 << 8;
    private bool enemySeen = false;
    void Start()
    {
        layerMask = ~layerMask;
    }
    void Update()
    {
        if(!enemySeen) CheckIfEnemyIsInAngle();
    }
    private void CheckIfEnemyIsInAngle()
    {
        targetDir = enemyHead.position - transform.position;
        angle = Vector3.Angle(targetDir, transform.forward);
        if (angle < angleEnemyNeedsToBeIn)
        {
            CheckIfYouLookAtEnemeyHead();
        }  
    }
    private void CheckIfYouLookAtEnemeyHead()
    {
        if (!Physics.Linecast(transform.position, enemyHead.position))
        {
            enemySeen = true;
            playerIsDeath.SetDestenationToKill();
        }
    }

}
