using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

/**
 * Score label displays current score in white color. When it displays highscore, it starts showing stars.
 * Displays highscore on startup.
 * If highscore is zero, does not display it.
 */
public class ScoreLabel : SingletonBehaviour<ScoreLabel> {
    [SerializeField] private TMPro.TextMeshProUGUI label;
    [SerializeField] private List<DisplayedAnimationToggle> stars = new List<DisplayedAnimationToggle>();

    private static int highscore;
    private static int score;
    private static bool displayHighscore = true;

    public static int Score {
        get => score;
        set {
            score = value;
            if (score > highscore) {
                highscore = score;
                PlayerPrefs.SetInt("highscore", score);
            }

            instance.Display(score);
        }
    }

    protected override void Awake() {
        base.Awake();
        score = highscore = PlayerPrefs.GetInt("highscore", 0);
        Display(highscore);
    }

    private void Display(int score) {
        if (score == 0) {
            label.text = "";
            foreach (var star in stars) star.Displayed = false;
            return;
        }

        label.text = score.ToString();

        var isHighscore = score == highscore;

        foreach (var star in stars) star.Displayed = isHighscore;
    }
}