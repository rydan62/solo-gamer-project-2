using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    Image player_health2;
    PlayerController player;
    GameObject pauseMenu;
    public bool isPaused = false;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("ui_pause");
            player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerController>();
            player_health2 = GameObject.FindGameObjectWithTag("ui_health").GetComponent<Image>();
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            player_health2.fillAmount = (float)player.health / (float)player.maxHealth;
        }
    }
    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = .5f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
    public void MainMenu()
    {
        LoadLevel(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
