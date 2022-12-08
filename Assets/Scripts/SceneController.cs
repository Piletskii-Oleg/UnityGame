using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneController : MonoBehaviour
{
    [FormerlySerializedAs("dataManager")] [SerializeField] private GameDataManager gameDataManager;
    [FormerlySerializedAs("optionsManager")] [SerializeField] private OptionsDataManager optionsDataManager;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        optionsDataManager.Load();
        gameDataManager.Load();
        SceneManager.SetActiveScene(scene);
    }
}