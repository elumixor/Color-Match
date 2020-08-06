using System.Collections;
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
    [SerializeField] private AudioSource mainTheme;
    [SerializeField] private AudioSource touchParticle;

    [SerializeField] private float fadeTime = .5f;

    private static float TransitionTime => Instance.transitionTime;

    private float enemyDestroyedVolume;
    private float playerDestroyedVolume;
    private float mainThemeVolume;
    private float touchParticleVolume;

    protected override void Awake() {
        base.Awake();

        enemyDestroyedVolume = enemyDestroyed.volume;
        playerDestroyedVolume = playerDestroyed.volume;
        mainThemeVolume = mainTheme.volume;
        touchParticleVolume = touchParticle.volume;

        if (PlayerPrefs.GetInt("disableSounds", 0) == 1)
            touchParticle.volume = enemyDestroyed.volume = playerDestroyed.volume = mainTheme.volume = 0;

        touchParticle.enabled = enemyDestroyed.enabled = playerDestroyed.enabled = mainTheme.enabled = true;
    }

    private void Start() {
        normal.TransitionTo(0.1f);
    }

    public static bool Lowpass {
        set {
            if (value)
                Instance.lowpass.TransitionTo(TransitionTime);
            else
                Instance.normal.TransitionTo(TransitionTime);
        }
    }

    public static void Mute() {
        Instance.muted.TransitionTo(TransitionTime);
    }

    public static void PlayEnemyCollision() {
        Instance.enemyDestroyed.pitch = Random.Range(0.5f, 1.2f);
        Instance.enemyDestroyed.Play();
    }

    public static void PlayPlayerDestroyed() {
        Instance.playerDestroyed.pitch = Random.Range(0.9f, 1.1f);
        Instance.playerDestroyed.Play();
    }

    public bool EnableSounds {
        set {
            var currentEnemyDestroyedVolume = enemyDestroyed.volume;
            var currentPlayerDestroyedVolume = playerDestroyed.volume;
            var currentTouchParticleVolume = touchParticle.volume;
            var currentMainThemeVolume = mainTheme.volume;

            var targetEnemyDestroyedVolume = value ? enemyDestroyedVolume : 0;
            var targetPlayerDestroyedVolume = value ? playerDestroyedVolume : 0;
            var targetTouchParticleVolume = value ? targetPlayerDestroyedVolume : 0;
            var targetMainThemeVolume = value ? mainThemeVolume : 0;

            void Set(float v) {
                enemyDestroyed.volume = Mathf.Lerp(currentEnemyDestroyedVolume, targetEnemyDestroyedVolume, v);
                playerDestroyed.volume = Mathf.Lerp(currentPlayerDestroyedVolume, targetPlayerDestroyedVolume, v);
                mainTheme.volume = Mathf.Lerp(currentTouchParticleVolume, targetTouchParticleVolume, v);
                touchParticle.volume = Mathf.Lerp(currentMainThemeVolume, targetMainThemeVolume, v);
            }

            IEnumerator Coroutine() {
                var elapsed = 0f;

                while (elapsed < fadeTime) {
                    Set(elapsed / fadeTime);
                    elapsed += Time.unscaledDeltaTime;
                    yield return null;
                }

                Set(1f);
            }

            StartCoroutine(Coroutine());
        }
    }
}