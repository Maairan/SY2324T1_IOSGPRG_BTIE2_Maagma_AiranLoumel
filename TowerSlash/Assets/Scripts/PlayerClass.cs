using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerClass : MonoBehaviour
{
    public static PlayerClass instance;
    [SerializeField] TextMeshProUGUI[] characterSelectedText = new TextMeshProUGUI[3];
    public int _characterSelected;
    private int _prevSelected = 0;

    private void Start()
    {
        instance = this;
    }

    public void OnDefaultClicked()
    {
        CharacterSelected(0);
    }

    public void OnTankClicked()
    {
        CharacterSelected(1);
    }

    public void OnAssassinClicked()
    {
        CharacterSelected(2);
    }

    private void CharacterSelected(int value)
    {
        characterSelectedText[_prevSelected].gameObject.SetActive(false);
        _prevSelected = value;
        _characterSelected = value;
        characterSelectedText[value].gameObject.SetActive(true);
    }
}
