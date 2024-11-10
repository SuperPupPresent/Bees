using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, controlMenu, creditMenu;

    // Start is called before the first frame update
    void Start()
    {
        Back();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Controls()
    {
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    public void Credits()
    {
        creditMenu.SetActive(true);
        controlMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        controlMenu.SetActive(false);
        creditMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
