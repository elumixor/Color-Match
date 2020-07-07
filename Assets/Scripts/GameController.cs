using System.Collections.Generic;
using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameController : SingletonBehaviour<GameController>, IUnityAdsListener {
    [SerializeField] private List<DisplayedAnimationToggle> objectToHide = new List<DisplayedAnimationToggle>();
    [SerializeField] private float secondsBeforeAdd = 15f;

    // Track time and conditionally show ads
    private float startTime;
    private float elapsed;

    protected override void Awake() {
        base.Awake();

        PlayerCollisionHandler.OnCollided += delegate { OnGameEnded(); };
        PlayerCollisionHandler.OnPassed += delegate { ScoreLabel.Score++; };

        instance = this;
    }

    private void Start() {
        GameComponentsEnabled = false;
        Advertisement.AddListener(this);
    }

    private void Update() {
        // Will react on taps and mouse buttons
        if (Input.GetMouseButtonDown(0)) OnGameStartedOrResumed();
    }

    private bool GameComponentsEnabled {
        set {
            EnemySpawner.Enabled = value;
            PlayerInputHandler.Enabled = value;
            SpeedIncreaser.Enabled = value;
        }
    }

    private bool MenuComponentsEnabled {
        set {
            enabled = value;
            foreach (var toggle in objectToHide) toggle.Displayed = value;
        }
    }

    private bool ComponentsEnabled {
        set {
            GameComponentsEnabled = value;
            MenuComponentsEnabled = !value;
        }
    }

    private void OnGameStartedOrResumed() {
        // Track time playing
        startTime = Time.time;

        ScoreLabel.Score = 0;
        ComponentsEnabled = true;

        // Restart game components components
        PlayerController.ResetPlayer();
        SpeedIncreaser.Restart();
    }

    private void OnGameEnded() {
        ComponentsEnabled = false;

        // Destroy all currently spawned enemies and stop spawning new ones 
        foreach (var enemy in FindObjectsOfType<Enemy>()) {
            enemy.SpawnParticles();
            Destroy(enemy.gameObject);
        }

        // If playing for long enough, then show advertisement
        elapsed += Time.time - startTime;
        if (elapsed > secondsBeforeAdd) {
            Advertisement.Show();
            Enabled = false;
            elapsed = 0f;
        }
    }

    public void OnUnityAdsReady(string placementId) { }

    public void OnUnityAdsDidError(string message) => Debug.LogError(message);

    public void OnUnityAdsDidStart(string placementId) { }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) => Enabled = true;
}