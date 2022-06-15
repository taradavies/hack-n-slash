using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void Begin()
    {
        StartCoroutine(BeginGame());
    }

    IEnumerator BeginGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            yield return null;
        }

        PlayerManager.Instance.SpawnPlayerCharacters();

        Debug.Log("Starting game");
    }
}
