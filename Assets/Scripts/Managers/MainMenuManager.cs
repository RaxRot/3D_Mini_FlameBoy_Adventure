using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void SelectLevel()
    {
        SceneManager.LoadScene(TagManager.LEVEL_SELECT_NAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
