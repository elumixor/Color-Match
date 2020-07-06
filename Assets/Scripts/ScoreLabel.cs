using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ScoreLabel : SingletonBehaviour<ScoreLabel> {
    [SerializeField] private TMPro.TextMeshProUGUI label;

    private static int highscore;
    private static int score;
    private static bool displayHighscore = true;

    public static bool DisplayHighscore {
        set {
            instance.label.text = value ? highscore.ToString() : score.ToString();

            displayHighscore = value;
        }
    }

    public static int Score {
        get => score;
        set {
            score = value;
            if (score > highscore) {
                highscore = score;
                PlayerPrefs.SetInt("highscore", score);
            }

            if (!displayHighscore) {
                instance.label.text = score.ToString();
            }
        }
    }

    public Color Color {
        set => label.faceColor = value;
    }

    protected override void Awake() {
        base.Awake();
        highscore = PlayerPrefs.GetInt("highscore", 0);
        label.text = highscore.ToString();
    }
}