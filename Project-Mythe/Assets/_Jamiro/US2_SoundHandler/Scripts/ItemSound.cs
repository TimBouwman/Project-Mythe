using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField] [Tooltip("geluidje wat het object moet maken als hij iets raakt")] private AudioClip objectHitSomethingSound;
    [SerializeField] [Tooltip("geluidje wat het object moet maken wanneer je het object gebruikt")]private AudioClip objectSoundWhenUsed;
    private AudioSource objectPoolingAudioSource;
    private AudioSource thisObjectAudioSource;
    private enum ObjectSound {HitSomethingSound, UseObjectSound}
    private ObjectSound myObjectSound;
    private float mag;
    [SerializeField] private float soundDevideBy;

    // checkt voor collision met de grond
    // set de enum zodat de juiste sound settings word gebruikt

    private void Start()
    {
        thisObjectAudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 3)
        {
            PoolItemSound();
            mag = collision.relativeVelocity.magnitude;
            myObjectSound = ObjectSound.HitSomethingSound;
        }
    }

    //vraagt de objectpooler een audiohandler object uit de pool aan 
    //past de settings van het geluid aan waar dat nodig is (volume van het item wat de grond raakt word bepaald door hoe hard hij iets raakt)
    private void PoolItemSound()
    {
        GameObject sound = ObjectPooler.SharedInstance.GetPooledObject();
        objectPoolingAudioSource = sound.GetComponent<AudioSource>();
        if (sound != null && myObjectSound == ObjectSound.HitSomethingSound)
        {
            setHitSomethingSoundSettings(sound);
        }
    }

    // de enum word gecheckt en de volume word aangepast wanneer nodig is.
    // en de juiste audioclip word gebruikt
    private void setHitSomethingSoundSettings(GameObject sound)
    {
            objectPoolingAudioSource.volume = (mag / 100) / soundDevideBy;
            objectPoolingAudioSource.clip = objectHitSomethingSound;
            sound.transform.position = transform.position;
            sound.SetActive(true);
    }
}
