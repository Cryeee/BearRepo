using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigateMenus : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject firstSelected;

    // Start is called before the first frame update
    void Start()
    {
        SetSelected(firstSelected);
    }
    public void SetSelected(GameObject next)
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(next);
    }

}
