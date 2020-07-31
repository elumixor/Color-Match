using System.Collections.Generic;
using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameController : SingletonBehaviour<GameController>, IUnityAdsListener {
    [SerializeField] private List<DisplayedAnimationToggle> objectToHide = new List<DisplayedAnimationToggle>();
    [SerializeField] private float adSecondsMultiplier = 1;
    [SerializeField] private float adTimesLostMultiplier = 20;

    [SerializeField] private ScoreLabel scoreLabel;
    [SerializeField] private ScoreLabel highscoreLabel;

    [SerializeField] private DisplayedAnimationToggle settingsAnimationToggle;
    
    private int score;
    private int highscore;

    // Track time and conditionally show ads
    private float startTime;

    private float currentAdPoints;
    private static readonly int FocusedID = Animator.StringToHash("Focused");

    protected override void Awake() {
        base.Awake();

        PlayerCollisionHandler.OnCollided += delegate { OnGameEnded(); };
        PlayerCollisionHandler.OnPassed += delegate { OnPassed(); };
    }

    private void Start() {
        GameComponentsEnabled = false;
        highscoreLabel.Score = highscore = PlayerPrefs.GetInt("highscore", 0);
        if (highscore == 0) highscoreLabel.DisplayStars = highscoreLabel.Displayed = false;

        scoreLabel.DisplayStars = scoreLabel.Displayed = false;
        
        Advertisement.AddListener(this);
    }

    private void Update() {
        // Will react on taps and mouse buttons
        if (Input.GetMouseButtonDown(0)) OnGameStartedOrResumed();
    }

    private static bool GameComponentsEnabled {
        set {
            EnemySpawner.Enabled = value;
            PlayerInputHandler.Enabled = value;
            TouchParticlesSpawner.Enabled = value;
            SpeedIncreaser.Enabled = value;
        }
    }

    private bool MenuComponentsEnabled {
        set {
            enabled = value;
            SoundManager.Lowpass = !value;
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

        scoreLabel.Score = score = 0;
        ComponentsEnabled = true;
        scoreLabel.Displayed = true;
        scoreLabel.DisplayStars = false;
        scoreLabel.GetComponent<Animator>().SetBool(FocusedID, false);

        // Restart game components components
        PlayerController.ResetPlayer();
        SpeedIncreaser.Restart();
        PlayerCollisionHandler.Restart();
    }

    private void OnPassed() {
        score++;

        scoreLabel.Score = score;

        if (score > highscore) {
            highscore = score;
            highscoreLabel.Score = score;
            scoreLabel.DisplayStars = highscoreLabel.DisplayStars = highscoreLabel.Displayed = true;
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public static float Elapsed => Mathf.Max(0, Time.time - Instance.startTime);

    private void OnGameEnded() {
        ComponentsEnabled = false;
        scoreLabel.GetComponent<Animator>().SetBool(FocusedID, true);


        // Destroy all currently spawned enemies and stop spawning new ones 
        foreach (var enemy in FindObjectsOfType<Enemy>()) {
            enemy.SpawnParticles();
            Destroy(enemy.gameObject);
        }

        // If playing for long enough, then show advertisement
        currentAdPoints += Elapsed * adSecondsMultiplier + adTimesLostMultiplier;


        if (currentAdPoints > 1f) {
            Advertisement.Show();
            Enabled = false;
            currentAdPoints = 0f;
        }
    }

    public void ShowSettings() {
        settingsAnimationToggle.Displayed = true;
        GameComponentsEnabled = false;
        Time.timeScale = 0f;
    }

    public void HideSettings() {
        settingsAnimationToggle.Displayed = false;
        GameComponentsEnabled = true;
        Time.timeScale = 1f;
    }
    
    public void OnUnityAdsReady(string placementId) { }

    public void OnUnityAdsDidError(string message) => Debug.LogError(message);

    public void OnUnityAdsDidStart(string placementId) => SoundManager.Mute();

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
        Enabled = true;
        SoundManager.Lowpass = false; // Unmute 
    }
}