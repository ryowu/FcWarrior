using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    private AudioSource stageClearSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        stageClearSoundEffect = GetComponent<AudioSource>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            stageClearSoundEffect.Play();
            CompleteStage();
        }
    }

    private void CompleteStage()
    {
        throw new NotImplementedException();
    }
}
