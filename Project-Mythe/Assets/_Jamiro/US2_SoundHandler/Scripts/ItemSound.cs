using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    #region Variables
    [SerializeField] [Tooltip("geluidje wat het object moet maken als hij iets raakt")] private AudioClip objectHitSomethingSound;
    [SerializeField] [Tooltip("geluidje wat het object moet maken wanneer je het object gebruikt")] private AudioClip objectSoundWhenUsed;
    [SerializeField] [Tooltip("de audiosource die op dit item moet")] private AudioSource thisObjectAudioSource;
    [SerializeField] [Tooltip("de magnitude van de velocity / 100  en dan nog eens delen door dit")]private float soundDevideBy;
    private AudioSource objectPoolingAudioSource;
    private float mag;
    #endregion

    #region Unity methods
    private void Start()
    {
        if(thisObjectAudioSource != null) thisObjectAudioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region Sound methods
    // checkt voor collision met de grond
    // set de enum zodat de juiste sound settings word gebruikt
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 3)
        {
            mag = collision.relativeVelocity.magnitude;
            PoolItemSound();
        }
    }

    //vraagt de objectpooler een audiohandler object uit de pool aan 
    //past de settings van het geluid aan waar dat nodig is (volume van het item wat de grond raakt word bepaald door hoe hard hij iets raakt)
    private void PoolItemSound()
    {
        GameObject sound = ObjectPooler.SharedInstance.GetPooledObject();
        objectPoolingAudioSource = sound.GetComponent<AudioSource>();
        if (sound != null)
        {
            SetHitSomethingSoundSettings(sound);
        }
    }

    // de enum word gecheckt en de volume word aangepast wanneer nodig is.
    // en de juiste audioclip word gebruikt
    private void SetHitSomethingSoundSettings(GameObject sound)
    {
            objectPoolingAudioSource.volume = (mag / 100) / soundDevideBy;
            objectPoolingAudioSource.clip = objectHitSomethingSound;
            sound.transform.position = transform.position;
            sound.SetActive(true);
    }
    // wanneer je een item gebruikt deze aanroepen
    // speelt het geluid af wanneer een item gebruikt word.
    private void ItemIsUsedSound()
    {
        thisObjectAudioSource.clip = objectSoundWhenUsed;
        thisObjectAudioSource.Play();
    }
    #endregion
}
