using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] TextMeshProUGUI winText;
    bool playerWon = false;
    private void Start()
    {
        Resume();
        winMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale > 0)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        if (!playerWon)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Resume()
    {
        if (!playerWon)
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void winGame(Component sender, object data)
    {
        if (data is int)
        {
            playerWon = true;
            winMenu.SetActive(true);
            int playerNumber = (int)data;
            winText.text = "PLAYER " + playerNumber + " WINS!";
        }
    }
}
