using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField] [Tooltip("geluidje wat het object moet maken als hij iets raakt")] private AudioClip objectHitSomethingSound;
    [SerializeField] [Tooltip("geluidje wat het object moet maken wanneer je het object gebruikt")]private AudioClip objectSoundWhenUsed;
    private AudioSource audioSource;
    private enum ObjectSound {HitSomethingSound, UseObjectSound}
    private ObjectSound myObjectSound;

    void Start()
    {
        
    }


    // checkt voor collision met de grond
    // set de enum zodat de juiste sound settings word gebruikt
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 3)
        {
            PlayItemSound(collision.relativeVelocity.magnitude, objectHitSomethingSound);
            myObjectSound = ObjectSound.HitSomethingSound;
        }
    }

    //vraagt de objectpooler een audiohandler object uit de pool aan 
    //past de settings van het geluid aan waar dat nodig is (volume van het item wat de grond raakt word bepaald door hoe hard hij iets raakt)
    private void PlayItemSound(float mag, AudioClip audioSound)
    {
        GameObject sound = ObjectPooler.SharedInstance.GetPooledObject();
        audioSource = sound.GetComponent<AudioSource>();
        if (sound != null)
        {
            setSoundSettings(mag);
            sound.transform.position = transform.position;
            sound.SetActive(true);
        }
    }

    // de enum word gecheckt en de volume word aangepast wanneer nodig is.
    // en de juiste audioclip word gebruikt
    private void setSoundSettings(float mag)
    {
        if (myObjectSound == ObjectSound.HitSomethingSound)
        {
            audioSource.volume = (mag / 100) / 1.5f;
            audioSource.clip = objectHitSomethingSound;
        }
        else if (myObjectSound == ObjectSound.UseObjectSound)
        {
            audioSource.volume = 1;
            audioSource.clip = objectSoundWhenUsed;
        }
    }
}
