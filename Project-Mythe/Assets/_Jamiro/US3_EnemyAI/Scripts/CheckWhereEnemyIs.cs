using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CheckWhereEnemyIs : MonoBehaviour
{
    [SerializeField] private float angleEnemyNeedsToBeIn = 0;
    [SerializeField] private float angleWithWarning = 0;
    [SerializeField] private PlayerDeath playerIsDeath = null;
    [SerializeField] private Volume volume = null;
    [SerializeField] private BirdHandler birdHandler;
    [SerializeField] private string TagOfSleepingBird = "SleepingBird";
    [SerializeField] private BirdGoingCrazy birdGoingCrazy;
    private float angle = 0;
    private Vector3 targetDir = new Vector3();
    private bool enemySeen = false;

    void Update()
    {

        if (!enemySeen)
        {     
          CheckIfEnemyIsInAngle(birdHandler.ActiveHead());  
        }

    }
    private void CheckIfEnemyIsInAngle(Transform enemyHead)
    {
        if (enemyHead.tag != TagOfSleepingBird)
        {
            targetDir = enemyHead.position - transform.position;
            angle = Vector3.Angle(targetDir, transform.forward);
            if (angle < angleEnemyNeedsToBeIn)
            {
                CheckIfYouLookAtEnemeyHead(enemyHead);
            }
            if (angle < angleWithWarning)
            {
                PostProccesCamera(enemyHead);
            }
            else if (volume.weight > 0)
            {
                volume.weight -= 0.05f;
            }
        }
    }
    private void CheckIfYouLookAtEnemeyHead(Transform enemyHead)
    {
        if (!Physics.Linecast(transform.position, enemyHead.position))
        {
            enemySeen = true;
            if (birdHandler.GetActiveBird().tag == "GoCrazy")
            {
                birdGoingCrazy.GoCrazy();
                enemySeen = false;
            }
            else
            {
                playerIsDeath.SetDestenationToKill();
            }
        }
    }
    private void PostProccesCamera(Transform enemyHead)
    {
        if (!Physics.Linecast(transform.position, enemyHead.position))
        {
            if (volume.weight < 1) volume.weight += 0.05f;
        }
        
    }

}
