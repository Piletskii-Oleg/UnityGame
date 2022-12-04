using DataPersistence;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private DataPersistenceManager dataManager;
    [SerializeField] private OptionsManager optionsManager;

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
        optionsManager.LoadOptions();
        dataManager.LoadGame();
        SceneManager.SetActiveScene(scene);
    }
}