using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class BirdAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator thisAnimtor;
    [SerializeField] private BirdHandler birdHandler;
    [SerializeField] private Transform playerTransform;
    private string animKillBool = "killwalk";
    private string walkAnimBool = "normalwalk";
    private float timeleft = 1.9f;
    private bool timer = false;
    void Start()
    {
        if(thisAnimtor == null)
        {
            thisAnimtor = GetComponent<Animator>();
        }


    }
    void Update()
    {
        if(timer)
        {
            if (timeleft > 0)
            {
                timeleft -= Time.deltaTime;
            }
            else
            {
                timer = false;
                birdHandler.GetAgent().destination = playerTransform.position;
            }
        }
    }
    public void SetAttackAnimation()
    {
        thisAnimtor.SetBool(animKillBool, true);
     //   timeleft = thisAnimtor.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        timer = true;

    }

    public void SetWalkAnimation(bool goingToWalk)
    {
        thisAnimtor.SetBool(walkAnimBool, goingToWalk);
    }

   


}
