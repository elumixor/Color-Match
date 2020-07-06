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

        PlayerCollisionHandler.OnCollided += OnCollided;
        PlayerCollisionHandler.OnPassed += OnPassed;

        instance = this;
    }

    private void Start() {
        PlayerInputHandler.Enabled = false;
        // instance.simulation = true;
        // instance.inputHandler.enabled = false;
        // instance.autoController.enabled = true;
        ScoreLabel.DisplayHighscore = true;
        // instance.playerController.ResetRotation();
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
        ComponentsEnabled = true;

        // Reset components
        EnemySpawner.Restart();
        PlayerController.ResetRotation();
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

    private void OnPassed(Enemy enemy) {
        if (!simulation) ScoreLabel.Score++;
    }

    private void OnCollided(Enemy enemy) {
        OnGameEnded();
    }

    public void OnUnityAdsReady(string placementId) { }

    public void OnUnityAdsDidError(string message) => Debug.LogError(message);

    public void OnUnityAdsDidStart(string placementId) { }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        lastAdd = Time.time;
    }
}