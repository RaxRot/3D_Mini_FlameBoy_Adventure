using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField] private float waitBeforeRespawning = 1.5f;
    [SerializeField] private bool isRespawning;

    private PlayerController _playerController;
    
    private Vector3 _respawnPoint;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _respawnPoint = _playerController.transform.position+Vector3.up;
    }

    public void RespawnPlayer()
    {
        if (!isRespawning)
        {
            isRespawning = true;

            StartCoroutine(_RespawnPlayerCo());
        }
    }

    private IEnumerator _RespawnPlayerCo()
    {
        UIManager.Instance.FadeToBlack();
        
        _playerController.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(waitBeforeRespawning);
        
        _playerController.transform.position = _respawnPoint;
        _playerController.gameObject.SetActive(true);
        
        UIManager.Instance.FadeFromBlack();

        isRespawning = false;
        
        PlayerHealth.Instance.FillHealth();
    }

    public void SetNewRespawnPoint(Vector3 newPoint)
    {
        _respawnPoint = newPoint + Vector3.up;
    }
    
}
