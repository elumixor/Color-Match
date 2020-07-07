using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : SingletonBehaviour<SoundManager> {
    [SerializeField] private AudioMixerSnapshot lowpass;
    [SerializeField] private AudioMixerSnapshot normal;
    [SerializeField] private AudioMixerSnapshot muted;
    [SerializeField] private float transitionTime = 0.5f;

    [SerializeField] private AudioSource enemyDestroyed;
    [SerializeField] private AudioSource playerDestroyed;
    

    private static float TransitionTime => instance.transitionTime;
    private static Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

    private void Start() {
        normal.TransitionTo(0.1f);
    }

    public static bool Lowpass {
        set {
            if (value)
                instance.lowpass.TransitionTo(TransitionTime);
            else
                instance.normal.TransitionTo(TransitionTime);
        }
    }

    public static void Mute() {
        instance.muted.TransitionTo(TransitionTime);
    }

    public static void PlayEnemyCollision() {
        instance.enemyDestroyed.pitch = Random.Range(0.5f, 1.2f);
        instance.enemyDestroyed.Play();
    }

    public static void PlayPlayerDestroyed() {
        instance.playerDestroyed.pitch = Random.Range(0.9f, 1.1f);
        instance.playerDestroyed.Play();
    }
}