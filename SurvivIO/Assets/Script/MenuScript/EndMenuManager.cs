using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resultText;

    void Update()
    {
        if(GameManager.instance.player._isWinner)
            _resultText.text = "Winner winner, chimken dinner!";
        else
            _resultText.text = "You lost. Better luck next time!";       
    }
}
