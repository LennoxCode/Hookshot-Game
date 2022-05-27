using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneAsset[] _sceneAssets;
    private int currLevelIndex = 0;
    public static SceneController instance;
    public Action OnLevelLoaded;
    private void Awake()
    {
        if(instance != null){Destroy(this.gameObject);}

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int index)
    {
        currLevelIndex = index;
        SceneManager.LoadScene(_sceneAssets[index].name);
        OnLevelLoaded?.Invoke();
    }

    public void LoadNextLevel()
    {

        if (currLevelIndex + 1 >= _sceneAssets.Length) return;
        LoadScene(currLevelIndex + 1);
    }

    public void ReloadCurrLevel()
    {
        LoadScene(currLevelIndex);
    }
}
