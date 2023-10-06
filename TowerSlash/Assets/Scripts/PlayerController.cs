using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    EnemyController _target;
    Renderer _ren;
    private int _characterChoice = PlayerClass.instance._characterSelected;
    //Characters[0] default, Characters[1] tank 5 max health, Characters[2] assassin gaugefillmultiplier 2x
    [SerializeField] private TextMeshProUGUI _healthText;
    
    //Dash Stats
    [SerializeField] SpecialBar _specialBar;
    private float _maxGaugeFill = 100.0f;
    private float _currentGaugeFill = 0.0f;
    private float _gaugeFillMultiplier;
    private int _specialDuration = 3;
    bool _inSpecial = false; 
    public bool _canSpecial = false;

    //Player Stats
    private int _maxHealth;
    private int _currentHealth;
    private float _moveSpeed = 2.0f;
    bool _isDead = false;
    public bool _canAttack = false;

    #region Unity Functions
    private void Start()
    {
        switch(_characterChoice)
        {
            case 0: //Default
            _maxHealth = 3;
            _gaugeFillMultiplier = 1.0f;
            break;

            case 1: //Tank
            _maxHealth = 5;
            _gaugeFillMultiplier = 0.5f;
            break;

            case 2: //Assassin
            _maxHealth = 1;
            _gaugeFillMultiplier = 3.0f;
            break;
        }
        _specialBar.SetMaxSpecial(_maxGaugeFill);
        _specialBar.SetSpecial(_currentGaugeFill);

        _ren = GetComponent<Renderer>();
        _currentHealth = _maxHealth;
        _healthText.text = "Health: " + _currentHealth;
        
    }

    private void Update()
    {
        transform.position += new Vector3(0f, _moveSpeed, 0f) * Time.deltaTime;
    }

    #endregion
    //Space//
    #region Collisions
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            _canAttack = true;
            _target = collider.GetComponent<EnemyController>();
            StartCoroutine(SetPlayerColor(Color.green, Color.white));
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
            _canAttack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == _target.gameObject)
        {
            if (_inSpecial)
                ScoreTracker.instance.AddScore(10);
            
            else
                TakeDamage(); 
        
            _target.Die();
        }
    }

    #endregion
    //Space//
    #region Private Functions
    public void TakeDamage()
    {
        StartCoroutine(SetPlayerColor(Color.red, Color.white));
        if (_currentHealth > 0)
            _currentHealth--;

        if (_currentHealth == 0)
            Die();
        
        _healthText.text = "Health: " + _currentHealth;
    }

    private void Heal(int chance)
    {
        int healChance = Random.Range(0, 101);
        if (healChance <= chance) // % chance of healing
            if (_currentHealth < _maxHealth)
                _currentHealth++;
        _healthText.text = "Health: " + _currentHealth;
    }

    private void Die()
    {
        GameManager.instance.GoToScene("GameOverScene");
    }

    private void OnKill()
    {
        switch(_target.GetEnemyType())
        {
            case EnemyType.Yellow:
            ScoreTracker.instance.AddScore(4);
            break;

            case EnemyType.Red:
            ScoreTracker.instance.AddScore(3);
            break;

            case EnemyType.Green:
            ScoreTracker.instance.AddScore(2);
            break;
        }
        Heal(3); 
        AddDashGaugeFill(5);
        _target.Die();
    }

    private void AddDashGaugeFill(float value)
    {
        if(_currentGaugeFill < _maxGaugeFill)
            _currentGaugeFill += value * _gaugeFillMultiplier;

        if (_currentGaugeFill == _maxGaugeFill)
            _canSpecial = true;

        _specialBar.SetSpecial(_currentGaugeFill);
    }

    #endregion
    //Space//
    #region Public Functions
    public void Attack(SwipeDirection direction)
    {
        if (_target.GetArrowDirection() == (int)direction && (_target.GetEnemyType() == EnemyType.Green || _target.GetEnemyType() == EnemyType.Yellow)) //Same Direction
            OnKill();

        else if (_target.GetArrowDirection() == (int)SwipeDetection.instance.GetOppositeDirection(direction) && _target.GetEnemyType() == EnemyType.Red) //Opposite direction
            OnKill();
            
        else //Wrong swipe direction
        {
            TakeDamage();   
            _target.Die();
        }
    }

    public void OnDashButtonClicked()
    {
        if(_currentGaugeFill == _maxGaugeFill)
            StartCoroutine(DashAttack());
        
    }

    #endregion
    //Space//
    #region Coroutines
    private IEnumerator SetPlayerColor(Color color1, Color color2)
    {
        _ren.material.SetColor("_Color", color1);
        yield return new WaitForSeconds(1.0f);
        _ren.material.SetColor("_Color", color2);
    }

    private IEnumerator DashAttack()
    {
        _inSpecial = true;
        _moveSpeed = 10.0f; //Increase speed

        yield return new WaitForSeconds(_specialDuration); //Special time

        _moveSpeed = 2.0f; //Back to normal speed
        _inSpecial = false;
        _canSpecial = false;
        _currentGaugeFill = 0;
        _specialBar.SetSpecial(_currentGaugeFill);
    }
    
    #endregion

}
