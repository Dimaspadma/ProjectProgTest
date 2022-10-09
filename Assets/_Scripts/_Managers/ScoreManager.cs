using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    
    public int score;
    
    [SerializeField] private TextMeshProUGUI scoreView;
    [SerializeField] private Image plane;
    [SerializeField] private Button retry;

    public void Loose()
    {
        plane.gameObject.SetActive(true);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        retry.onClick.AddListener(RestartLevel);
    }

    private void Update()
    {
        scoreView.text = score.ToString();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("MainScene");
        score = 0;
    }
    
}
