using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWhenAnimationIsDone : MonoBehaviour
{
    [SerializeField] private Animator animatorOnThisObject;
    [SerializeField] private string animationName;
    [SerializeField] private AudioSource objectAudioSource;
    // Start is called before the first frame update
    public void playThisAnimation()
    {
        animatorOnThisObject.Play(animationName, 0, 0);
        objectAudioSource.Play();
    }
}
