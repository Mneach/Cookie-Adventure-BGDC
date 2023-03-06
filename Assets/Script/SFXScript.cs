using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip hurtSFX;
    public AudioClip attack1;
    public AudioClip attackmagic;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Damaged()
    {
        aud.PlayOneShot(hurtSFX);
    }
    void PlayAttack()
    {
        aud.PlayOneShot(attack1);
    }
    void PlayAttackMagic()
    {
        aud.PlayOneShot(attackmagic);
    }
}
