using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private MapGenerator _generator;
    [SerializeField] private Snake _snake;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _exit;

    private void OnEnable()
    {
        _snake.Died += GameOver;
        _restart.onClick.AddListener(Restart);
        _exit.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _snake.Died -= GameOver;
        _restart.onClick.RemoveListener(Restart);
        _exit.onClick.RemoveListener(Exit);
    }

    private void Start()
    {
        Map map = _generator.Create(38, 18);
        _snake.Init(map);
    }

    private void GameOver()
    {
        _gameOver.SetActive(true);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
