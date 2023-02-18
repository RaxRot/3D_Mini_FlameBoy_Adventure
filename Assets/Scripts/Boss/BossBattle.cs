using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int _currentHealth;

    [SerializeField] private Slider bossSlider;

    private void Start()
    {
        _currentHealth = maxHealth;
        bossSlider.maxValue = maxHealth;
        bossSlider.value = _currentHealth;
    }

    public void DamageBoss()
    {
        _currentHealth--;

        if (_currentHealth<=0)
        {
            _currentHealth = 0;
            gameObject.SetActive(false);
        }
        
        bossSlider.value = _currentHealth;
    }
}
