using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _weaponName, _reserved9mm, _reserved556mm, _reserved12gauge, _medkitCount;
    [SerializeField] private Slider slider;
    public TextMeshProUGUI _healingText, _magazineAmmo, _reloadingText;
    private PlayerShoot player;
    public static UIHandler instance;
    public Action<PlayerMovement> _OnDeath;

    private void Awake()
    {
        UIHandler.instance = this;
    }

    private void Start()
    {
        GameManager.instance.player.OnHit += HealthBarUpdate;
        player = GameManager.instance.player.GetComponent<PlayerShoot>();
        _medkitCount.text = player.GetComponent<Health>()._currentMedkit.ToString();
    }

    public void HealthBarUpdate(Health health)
    {
        slider.value = health._currentHealth / health._maxHealth;
        if (slider.value <= 0)
        {
            OnDeath(health.GetComponent<PlayerMovement>());
            GameManager.instance.player._isWinner = false;
            StartCoroutine(GoToEndMenu());
        }
    }

    public void UpdateBagText()
    {
        _reserved9mm.text = player._weapons[0]._reservedAmmo.ToString();
        _reserved12gauge.text = player._weapons[1]._reservedAmmo.ToString();
        _reserved556mm.text = player._weapons[2]._reservedAmmo.ToString();
    }

    public void UpdateWeaponText()
    {
        _weaponName.text = player._equippedWeapon.name;
        _magazineAmmo.text = player._equippedWeapon._currentAmmo.ToString();
    }

    public void UpdateMedkitText()
    {
        _medkitCount.text = player.GetComponent<Health>()._currentMedkit.ToString();
    }

    public void UpdateHealingText()
    {
        _healingText.text += Time.time.ToString();
    }
    private void OnDeath(PlayerMovement player)
    {
        Destroy(player.gameObject);
    }

    public IEnumerator GoToEndMenu()
    {
        yield return new WaitForSeconds(2);
        MenuManager.instance.LoadScene("EndMenuScene");
    }
}