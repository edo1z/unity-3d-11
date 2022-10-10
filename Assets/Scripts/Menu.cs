using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject MenuObj;

    public void OpenMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        MenuObj.SetActive(true);
    }

    public void BackGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuObj.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        //Debug.Log("Quit!");
        //Application.Quit();
        SceneManager.LoadScene("StartScene");
    }
}
