using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Pause()
    {
        
     
         pauseMenu.SetActive(true);
         Time.timeScale = 0;
       

    }


    public void Resume()
    {
        Debug.Log("game is back on");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}