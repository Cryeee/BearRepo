using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    // public stuff is for testing purposes:
    public GameObject canvas;
    public static bool paused;
    public PlayerInput playerInput;
    public InputManager inputManager;
    public EventSystem eventSystem;
    public GameObject firstSelectedObject;

    public void Pause()
    {
        print("pause was called");

        if (!paused)
        {
            canvas.SetActive(true);
            print("paused");
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(firstSelectedObject);

            playerInput.SwitchCurrentActionMap("UI");
            Time.timeScale = 0;
        } else if (paused)
        {
            print("un paused");
            canvas.SetActive(false);
            playerInput.SwitchCurrentActionMap("Player");
            Time.timeScale = 1;
        }

        paused = !paused;
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneIE(index));
    }
    
    private IEnumerator LoadSceneIE(int index)
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }


}
