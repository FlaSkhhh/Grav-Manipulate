using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManger : MonoBehaviour
{
    [SerializeField] Text timer;
    [SerializeField] Text result;

    float timeLeft;

    void Start()
    {
        result.gameObject.SetActive(false);
        timeLeft = 120f;
    }

    void FixedUpdate()
    {
        ManualRestart();
        ManualQuit();
        Timer();
        if (UIManager.instance.ScoreGetter() == 8)
        {
            Victory();
        }
    }

    void Victory()
    {
        result.gameObject.SetActive(true);
        result.text = "You Win";
        Invoke("Restart", 2f);
    }

    void Timer()
    {
        timeLeft -= Time.deltaTime;
        timer.text = timeLeft.ToString("F2");
        if (timeLeft < 0)
        {
            TimeOver();
        }
    }

    public void TimeOver()
    {
        result.gameObject.SetActive(true);
        result.text = "You Lost";
        Invoke("Restart", 2f);
    }

    void ManualRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Restart", 1f);
        }
    }

    void ManualQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
