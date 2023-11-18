using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class Health : MonoBehaviour
{
    public float _currentHealth { get; private set;}
    public float _maxHealth { get; private set;}
    public bool isAlive { get { return _currentHealth > 0;} }
    private int _healTime, _maxMedKit;
    public int _currentMedkit;
    public bool _isHealing;
    float _elapsedTime;

    private void Start()
    {
        _isHealing = false;
        _healTime = 3;
        _maxMedKit = 3;
    }
    
    private void Update()
    {
        if(_isHealing)
        {
            _elapsedTime += Time.deltaTime;
            UIHandler.instance._healingText.text = "Healing..." + Mathf.Round(3.0f - _elapsedTime);
        }
    }

    public void InitHealth(float health)
    {
        _maxHealth = health;
        _currentHealth = _maxHealth;
    }

    public void UpdateHealth(float value, Action<bool> onCallback = null)
    {
        _currentHealth -= value;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            onCallback?.Invoke(true);
        }

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void AddMedkit()
    {
        _currentMedkit++;
        if(_currentMedkit > _maxMedKit)
            _currentMedkit = _maxMedKit;
    }

    public void OnHealButtonClicked()
    {
        if(_currentHealth < _maxHealth && _currentMedkit > 0)
        {
            _isHealing = true;
            StartCoroutine(Heal());
            _elapsedTime = 0;
        }
    }

    public void StopHealing()
    {
        _isHealing = false;

        StopAllCoroutines();
        ChangePlayerSpeed(2.0f);
        UIHandler.instance._healingText.text = "";
    }

    private void ChangePlayerSpeed(float value)
    {
        this.GetComponent<PlayerMovement>()._moveSpeed *= value;
    }

    private IEnumerator Heal()
    {   
        ChangePlayerSpeed(0.5f);

        yield return new WaitForSeconds(_healTime);

        while(_isHealing)
        {
            ChangePlayerSpeed(2.0f);
            UIHandler.instance._healingText.text = "";

            _currentHealth += 30;
            _currentMedkit--;
            UIHandler.instance.HealthBarUpdate(this);
            UIHandler.instance.UpdateMedkitText();
            
            _isHealing = false;
        }        
    }
}