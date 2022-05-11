using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneAsset[] _sceneAssets; 
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(_sceneAssets[index].name);
    }
}
