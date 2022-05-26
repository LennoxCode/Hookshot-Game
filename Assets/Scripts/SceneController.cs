using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneAsset[] _sceneAssets;
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
        SceneManager.LoadScene(_sceneAssets[index].name);
        OnLevelLoaded?.Invoke();
    }
}
