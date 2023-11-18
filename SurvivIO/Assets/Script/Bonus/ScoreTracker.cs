using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killfeedText, _enemiesLeft;
    public static ScoreTracker instance;

    private void Awake()
    {
        ScoreTracker.instance = this;
    }

    public IEnumerator UpdateKillFeed(string killer, string killed)
    {
        _killfeedText.text = killer + " killed " + killed;
        yield return new WaitForSeconds(1.0f);
        _killfeedText.text = "";
    }

    public void UpdateEnemiesLeft(int count)
    {
        _enemiesLeft.text = "Enemies left: " + count;
    }

}