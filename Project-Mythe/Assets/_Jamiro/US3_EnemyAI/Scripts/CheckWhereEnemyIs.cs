using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWhereEnemyIs : MonoBehaviour
{
    [SerializeField] private float angleEnemyNeedsToBeIn = 0;
    [SerializeField] private Transform enemyHead = null;
    [SerializeField] private PlayerDeath playerIsDeath = null;
    private float angle = 0;
    private Vector3 targetDir = new Vector3();
    private bool enemySeen = false;

    void Update() { if(!enemySeen) CheckIfEnemyIsInAngle(); }

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
        Debug.DrawRay(transform.position, enemyHead.position);
        if (!Physics.Linecast(transform.position, enemyHead.position))
        {
            enemySeen = true;
            playerIsDeath.SetDestenationToKill();
        }
    }

}
