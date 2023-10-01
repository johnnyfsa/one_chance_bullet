using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    Player player;
    Points points;

    public int Score { get => points.PointsValue; }

    public Action OnGameOver = delegate { };
    public Action OnGameRestart = delegate { };

    private void Awake() {
        Instance = this;
        player = FindObjectOfType<Player>();
        points = player.GetComponent<Points>();
        player.GetComponent<Health>().OnDeath += GameOver;
    }

    public void RestartGame()
    {
        player.gameObject.SetActive(true);
        player.Health.SetFullHealth();
        player.transform.position = Vector3.zero;
        points.Reset();
        
        FindAnyObjectByType<LevelManager>().Init();
        
        var enemies = FindObjectsOfType<Enemy>();
        Array.ForEach(enemies, e => e.gameObject.SetActive(false));
        
        OnGameRestart();
    }

    public void GameOver()
    {
        player.gameObject.SetActive(false);
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
        OnGameOver();
    }

    private void OnDestroy() {
        Instance = null;
        player.GetComponent<Health>().OnDeath -= GameOver;
    }
}
