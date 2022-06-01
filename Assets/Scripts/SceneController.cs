using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// this class is responsible for loading scenes it holds an array of scenes which can be loaded with
/// the corresponding index. it also provides simpler functions to reload the current level or jump to the
/// next level. In addition it also fires an event upon loading a scene so every other class which persists
/// through loading calls certain function i.E scoremanger loading ScoreBoard
/// </summary>
public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneAsset[] _sceneAssets;
    public int currLevelIndex { private set; get; } 
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
