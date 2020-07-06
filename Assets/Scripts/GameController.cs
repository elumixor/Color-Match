using System.Collections.Generic;
using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameController : SingletonBehaviour<GameController>, IUnityAdsListener {
    [SerializeField] private List<DisplayedAnimationToggle> objectToHide = new List<DisplayedAnimationToggle>();

    [SerializeField] private float secondsBeforeAdd = 15f;

    private bool simulation = true;
    private float lastAdd = -1f;

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
        ScoreLabel.Score = 0;
        ComponentsEnabled = true;

        // Reset components
        EnemySpawner.Restart();
        PlayerController.ResetPlayer();
    }

    private void OnGameEnded() {
        ComponentsEnabled = false;

        // Destroy all currently spawned enemies and stop spawning new ones 
        foreach (var enemy in FindObjectsOfType<Enemy>()) {
            enemy.SpawnParticles();
            Destroy(enemy.gameObject);
        }

        // Show ad, if time elapsed since last ad
        if (lastAdd < 0) lastAdd = Time.time;
        else if (Time.time > lastAdd + secondsBeforeAdd) Advertisement.Show();
    }

    public void OnUnityAdsReady(string placementId) { }

    public void OnUnityAdsDidError(string message) => Debug.LogError(message);

    public void OnUnityAdsDidStart(string placementId) => Enabled = false;

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        lastAdd = Time.time;
        Enabled = true;
    }
}