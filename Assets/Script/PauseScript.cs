using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    
    public GameObject pauseMenu;
    public static bool IsPaused;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                IsPaused = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                IsPaused = false;
            }
        }
    }
}
