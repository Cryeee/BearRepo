﻿using System.Collections;
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


    public static bool invertCameraX;
    public static bool invertCameraY;

    public GameObject CMFreeLookCamera;

    public GameObject checkmarkX;
    public GameObject checkmarkY;

    private void Start()
    {
        // laita buttonit näyttään oikeeta:
        SwitchButtonGraphics();
        SetCameraInverse();
    }


    public void Pause()
    {


        if (!paused)
        {
            canvas.SetActive(true);
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(firstSelectedObject);

            playerInput.SwitchCurrentActionMap("UI");
            Time.timeScale = 0;
        } else if (paused)
        {
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

    // button click:
    public void ToggleCameraX()
    {
        invertCameraX = !invertCameraX;
        SwitchButtonGraphics();
        SetCameraInverse();
    }

    // button click:
    public void ToggleCameraY()
    {
        invertCameraY = !invertCameraY;
        SwitchButtonGraphics();
        SetCameraInverse();
    }

    public void SetCameraInverse()
    {
        if(CMFreeLookCamera != null)
        {
            // since cinemachine doesn't know what is inverted Y:
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_YAxis.m_InvertInput = !invertCameraY;
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InvertInput = invertCameraX;
        }
      
    }

    private void SwitchButtonGraphics()
    {
        Debug.Log(invertCameraX + " is inverted X");
        Debug.Log(invertCameraY + " is inverted Y");

        if(invertCameraX)
        {
            checkmarkX.SetActive(true);
        } else
        {
            checkmarkX.SetActive(false);
        }

        if(invertCameraY)
        {
            checkmarkY.SetActive(true);
        } else
        {
            checkmarkY.SetActive(false);
        }
    }
}
