using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score;
    void Start()
    {
        ScoreTracker.instance = this;
        _score = 0;
    }

    public void AddScore(int value)
    {
        _score += value;
        _scoreText.text = "Score: " + _score;
    }
}
