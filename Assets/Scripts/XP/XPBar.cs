using TMPro;
using UI;
using UnityEngine;

[RequireComponent(typeof(ProgressBar)), RequireComponent(typeof(DisplayedAnimationToggle))]
public class XPBar : SingletonBehaviour<XPBar> {
    [SerializeField] private float baseLevelXP = 100f;
    [SerializeField] private float levelIncreaseCoefficient = .15f;

    private ProgressBar progressBar;
    private DisplayedAnimationToggle displayedAnimationToggle;

    [SerializeField] private TextMeshProUGUI levelLabel;
    [SerializeField] private TextMeshProUGUI xpLabel;

    private int xp;
    private int level = -1;

    protected override void Awake() {
        base.Awake();
        displayedAnimationToggle = GetComponent<DisplayedAnimationToggle>();
        progressBar = GetComponent<ProgressBar>();
        xp = PlayerPrefs.GetInt("xp", 0);

        UpdateValues();
    }

    public int UpdateValues() {
        var newLevel = Mathf.Max(0, Mathf.FloorToInt(1 - Mathf.Log(Mathf.Max(1, xp) / baseLevelXP, levelIncreaseCoefficient))) + 1;
        var levelXP = Mathf.Pow(levelIncreaseCoefficient, 1 - newLevel) * baseLevelXP;
        var progress = xp / levelXP;

        progressBar.progress = progress;
        progressBar.UpdateButton();

        if (newLevel != level) {
            if (level != -1)
                LevelUp.Show(newLevel);

            level = newLevel;
            levelLabel.text = $"Level {newLevel}";
        }

        xpLabel.text = $"{xp} / {levelXP}";

        displayedAnimationToggle.Displayed = newLevel > 1;
        return newLevel;
    }

    public static void AddXP(int value) {
        Instance.xp += value;
        Instance.UpdateValues();
        PlayerPrefs.SetInt("xp", Instance.xp);
    }
}