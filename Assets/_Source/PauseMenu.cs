using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private MoveController MC;

    // Start is called before the first frame update
    void Awake()
    {
        MC = FindObjectOfType<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
