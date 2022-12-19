using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip backGroundMusic;
    private AudioSource audioClip;


    private void Awake()
    {
        audioClip = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioClip.PlayOneShot(backGroundMusic);
    }
    
}
