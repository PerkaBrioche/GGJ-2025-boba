using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public GameObject PausePanel;
    public void LoadSceneAction(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))&& PausePanel!=null)
        {
            Pause();
        }
    }
}
