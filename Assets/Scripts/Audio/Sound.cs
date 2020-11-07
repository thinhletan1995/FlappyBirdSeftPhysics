﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0.1f, 3f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}