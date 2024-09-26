using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int score;
    [SerializeField] Text scoreText;

    #region Singleton
    public static UIManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ScoreUpdate()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public int ScoreGetter()
    {
        return score;
    }
}
