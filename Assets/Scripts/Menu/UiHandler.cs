using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour
{
    public GameObject areYouSure;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void areYouSure_Enable()
    {
        areYouSure.SetActive(true);
    }

    public void areYouSure_Disable()
    {
        areYouSure.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
