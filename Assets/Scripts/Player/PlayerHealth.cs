using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    private int _currentHealth;
    [SerializeField] private int maxHealth;

    [SerializeField] private float invincibilityLength = 1f;
    private float _invincCounter;

    [SerializeField] private GameObject player;
    private bool _isPlayerActive;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        FillHealth();
    }

    private void Update()
    {
        if (_invincCounter>0)
        {
            _invincCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer()
    {
        if (_invincCounter<=0)
        {
            MakePlayerFlash();
            
            _invincCounter = invincibilityLength;
        
            _currentHealth--;

            if (_currentHealth<=0)
            {
                _currentHealth = 0;
            
                LevelManager.Instance.RespawnPlayer();
            }
            
            UIManager.Instance.UpdateHealth(_currentHealth,maxHealth);
        }
    }

    public void FillHealth()
    {
        _currentHealth = maxHealth;
        
        UIManager.Instance.UpdateHealth(_currentHealth,maxHealth);
    }

    private void MakePlayerFlash()
    {
        StartCoroutine(_MakePlayerFlashCo());
    }

    private IEnumerator _MakePlayerFlashCo()
    {
        for (int i = 0; i < 10; i++)
        {
            player.SetActive(_isPlayerActive);
            _isPlayerActive = !_isPlayerActive;
            yield return new WaitForSeconds(0.1f);
        }

        _isPlayerActive = true;
        player.SetActive(_isPlayerActive);
    }
}
