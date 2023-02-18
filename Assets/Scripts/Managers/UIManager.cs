using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Image fadeImage;
    private bool _isFadingToBlack, _isFadingFromBlack;
    [SerializeField] private float fadeSpeed = 2f;

    [SerializeField] private Slider healthSlider;

    [SerializeField] private TextMeshProUGUI textMeshPro;
    private int _coinCount;

    [SerializeField] private GameObject pausePanel;
    private bool _isPausePanelActive;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        textMeshPro.text = _coinCount.ToString();
        pausePanel.SetActive(_isPausePanelActive);
    }

    private void Update()
    {
        ControlFade();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPausePanel();
        }
    }

    private void ControlFade()
    {
        if (_isFadingToBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 1f ,fadeSpeed * Time.deltaTime));
        }
        
        if (_isFadingFromBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 0f ,fadeSpeed * Time.deltaTime));
        }
    }

    public void FadeToBlack()
    {
        _isFadingToBlack = true;
        _isFadingFromBlack = false;
    }

    public void FadeFromBlack()
    {
        _isFadingToBlack = false;
        _isFadingFromBlack = true;
    }

    public void UpdateHealth(int currentHealth,int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void CollectCoin()
    {
        _coinCount++;
        textMeshPro.text = _coinCount.ToString();
    }

    private void ShowPausePanel()
    {
        _isPausePanelActive = true;
        pausePanel.SetActive(_isPausePanelActive);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
       Unpause();
       
       Cursor.lockState = CursorLockMode.Locked;
    }

    public void LevelSelect()
    {
        Unpause();
        
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(TagManager.LEVEL_SELECT_NAME);
    }

    public void GoToMainMenu()
    {
        Unpause();
        
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(TagManager.MEIN_MENU_SCENE_NAME);
    }

    private void Unpause()
    {
        _isPausePanelActive = false;
        pausePanel.SetActive(_isPausePanelActive);
        Time.timeScale = 1f;
    }
}
