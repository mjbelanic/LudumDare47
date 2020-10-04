using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ClickMainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ClickHowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void ClickContinueButton()
    {
        player.pauseUp = false;
        player.pausePanel.alpha = 0;
        player.pausePanel.interactable = false;
        player.pausePanel.blocksRaycasts = false;
        Time.timeScale = 1;
    }
}
