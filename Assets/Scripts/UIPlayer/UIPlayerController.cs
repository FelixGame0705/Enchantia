using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerController : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _expBar;
    [SerializeField] private Text _currentGoldTxt;
    [SerializeField] private Text _currentHealthTxt;
    [SerializeField] private ButtonSkillController _buttonSkillController;

    private float _currentHealth;
    private int _currentGold;
    private float _maxHealth;
    private int _exp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaxHealthValue(float health)
    {
        _maxHealth = health;
        _healthBar.maxValue = health;
        _currentHealthTxt.text = health.ToString();
    }

    public void SetCurrentHealthValue(float health)
    {
        _currentHealth = health;
        _healthBar.value = _currentHealth;
        _currentHealthTxt.text = _currentHealth.ToString();
    }

    public void SetCurrentExpValue(int exp)
    {
        _expBar.value = exp;
    }

    public void AddCurrentGoldValue(int gold)
    {
        _currentGold += gold;
        _currentGoldTxt.text = _currentGold.ToString();
    }

    public void ResetCurrentGold()
    {
        _currentGold = 0;
    }

    public ButtonSkillController GetButtonSkillController()
    {
        return _buttonSkillController;
    }
}
