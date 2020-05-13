using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Cinemachine;


public class MenuController : MonoBehaviour
{
    // public stuff is for testing purposes:
    public GameObject canvas;
    public static bool paused;
    public PlayerInput playerInput;
    public InputManager inputManager;
    public EventSystem eventSystem;
    public GameObject firstSelectedObject;

    private static bool invertCameraX;
    private static bool invertCameraY;

    public GameObject CMFreeLookCamera;
    public Toggle cameraX;
    public Toggle cameraY;

    private void Start()
    {
        Debug.Log(invertCameraX);
        Debug.Log(invertCameraY);
            // laita buttonit näyttään oikeeta:
            cameraX.isOn = invertCameraX;
            cameraY.isOn = invertCameraY;
            SetCameraInverse();
    }


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

    public void ToggleCameraX()
    {
        invertCameraX = !invertCameraX;
        SetCameraInverse();
    }

    public void ToggleCameraY()
    {
        invertCameraY = !invertCameraY;
        SetCameraInverse();
    }

    public void SetCameraInverse()
    {
        if(CMFreeLookCamera != null)
        {
            // since cinemachine doesn't know what is inverted Y:
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InvertInput = invertCameraY;
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InvertInput = !invertCameraX;
        }
      
    }
}
