using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private MoveController MC;

    void Awake()
    {
        MC = FindObjectOfType<MoveController>();
    }
    
    public void PauseOpenClose()
    {
        if (gameObject.activeSelf == false)
        {
            MC.CanMove = false;
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        else
        {
            MC.CanMove = true;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        
    }
    public void Continue()
    {
        PauseOpenClose();
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
