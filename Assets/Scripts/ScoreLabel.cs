using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class ScoreLabel : MonoBehaviour {
    private TMPro.TextMeshProUGUI label;
    private int highscore;
    private int score;

    private bool displayHighscore = true;

    public bool DisplayHighscore {
        set {
            label.text = value ? highscore.ToString() : score.ToString();

            displayHighscore = value;
        }
    }

    public int Score {
        get => score;
        set {
            score = value;
            if (score > highscore) {
                highscore = score;
                PlayerPrefs.SetInt("highscore", score);
            }

            if (!displayHighscore) {
                label.text = score.ToString();
            }
        }
    }

    private void Awake() {
        label = GetComponent<TMPro.TextMeshProUGUI>();
        highscore = PlayerPrefs.GetInt("highscore", 0);
        label.text = highscore.ToString();
    }
}