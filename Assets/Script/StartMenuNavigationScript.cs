using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuControlsScript : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject controlsScreen;
    
    void Start()
    {
        controlsScreen.SetActive(false);
    }

    public void ShowControls()
    {
        controlsScreen.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        controlsScreen.SetActive(false);
    }
}
