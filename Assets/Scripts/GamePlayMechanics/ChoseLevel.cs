using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoTxt;
    private bool _canChoseLevel;

    [SerializeField] private string levelToLoadName;

    [SerializeField] private float timeForLoadLevel = 2f;

    private void Start()
    {
        infoTxt.enabled = _canChoseLevel;
    }

    private void Update()
    {
        if (_canChoseLevel && Input.GetKeyDown(KeyCode.Return))
        {
            _canChoseLevel = false;
            
            LoadLevel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            _canChoseLevel = true;
            infoTxt.enabled = _canChoseLevel;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            _canChoseLevel = false;
            infoTxt.enabled = _canChoseLevel;
        }
    }

    private void LoadLevel()
    {
        StartCoroutine(_LoadLevelCo());
    }

    private IEnumerator _LoadLevelCo()
    {
        UIManager.Instance.FadeToBlack();

        yield return new WaitForSeconds(timeForLoadLevel);
        
        SceneManager.LoadScene(levelToLoadName);
    }
}
