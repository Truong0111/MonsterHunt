using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sequences;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Singleton(nameof(LoadSceneManager), true)]
public class LoadSceneManager : Singleton<LoadSceneManager>
{
    private AsyncOperation _loadSceneOperation;
    private AsyncOperation _unloadSceneOperation;

    [TableList] public List<SceneData> sceneDatas;

#if UNITY_EDITOR
    [Button]
    public void InitSceneData()
    {
        var scenesToAddToBuild = new List<EditorBuildSettingsScene>();

        var sceneIndex = 0;

        foreach (var sceneData in sceneDatas)
        {
            if (sceneData.sceneRef == null)
                continue;

            sceneData.path = sceneData.sceneRef.path;
            var sceneBuildSetting = new EditorBuildSettingsScene(sceneData.path, sceneData.active);
            scenesToAddToBuild.Add(sceneBuildSetting);

            string sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneData.path);

            sceneData.index = sceneIndex;
            sceneData.name = sceneName;
            if (!sceneData.active)
            {
                sceneData.index = -1;
                continue;
            }

            sceneIndex++;
        }

        EditorBuildSettings.scenes = scenesToAddToBuild.ToArray();
    }
#endif

    private SceneData GetSceneData(int id)
    {
        return sceneDatas.Find(x => x.index == id);
    }

    private SceneData GetSceneData(string sceneName)
    {
        return sceneDatas.Find(x => x.name == sceneName);
    }

    public void Load(int id)
    {
        // var sceneData = GetSceneData(id);
        StartCoroutine(LoadScene(false, id));
    }

    public void Load(string sceneName)
    {
        var sceneData = GetSceneData(sceneName);
    }

    public void Unload(int id)
    {
        StartCoroutine(UnloadScene(id));
    }
    
    public void Unload(string sceneName)
    {
        StartCoroutine(UnloadScene(sceneName: sceneName));
    }

    private IEnumerator LoadScene(bool isRemoveCurrentScene, int id = -1, string sceneName = null)
    {
        if (id == -1 && sceneName == null)
        {
            Debug.LogError("No scene find to load");
            yield break;
        }

        if (isRemoveCurrentScene)
        {
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var sceneToRemove = SceneManager.GetSceneAt(i);
                if (sceneToRemove.buildIndex == 1 || sceneToRemove.name == "MasterScene") continue;
                _unloadSceneOperation =
                    SceneManager.UnloadSceneAsync(sceneToRemove, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

                while (!_unloadSceneOperation.isDone) yield return null;
            }
        }

        if (id != -1)
        {
            _loadSceneOperation = SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);
        }
        else if (sceneName != null)
        {
            _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        while (!_loadSceneOperation.isDone) yield return null;
    }

    private IEnumerator UnloadScene(int id = -1, string sceneName = null)
    {
        if(SceneManager.sceneCount <= 1) yield break;
        
        if (id == -1 && sceneName == null)
        {
            Debug.LogError("No scene find to unload");
            yield break;
        }

        if (id != -1)
        {
            
            _unloadSceneOperation = SceneManager.UnloadSceneAsync(id, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
        else if (sceneName != null)
        {
            _unloadSceneOperation =
                SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

        while (!_loadSceneOperation.isDone) yield return null;
    }
}

[Serializable]
public class SceneData
{
    public SceneReference sceneRef;

    [DisableInEditorMode] [DisableInPlayMode]
    public int index;

    [HideInInspector] [DisableInEditorMode] [DisableInPlayMode] [HideInTables]
    public string path;

    [DisableInEditorMode] [DisableInPlayMode]
    public string name;

    [DisableIf(nameof(CantRemove))] public bool active;
    private bool CantRemove => index is 0 or 1;
}