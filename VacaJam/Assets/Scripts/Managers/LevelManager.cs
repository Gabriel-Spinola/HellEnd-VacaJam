using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int CurrentLevel = 1;

    [SerializeField] private Animator anim = null;

    [SerializeField] private float transitionTime = 1f;

    private PlayerController _playerController = null;

    private void Awake()
    {
        if (FindObjectOfType<PlayerController>()) {
            _playerController = FindObjectOfType<PlayerController>();
        }
    }

    private void Update()
    {
        if (NextLevel.GoToNextLevel) {
            NextLevel.GoToNextLevel = false;
            GoToNextLevel();
        }
    }

    public static void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void GoToLevel(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    public void GoToLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        if (_playerController != null)
            StartCoroutine(_playerController.DisablePlayer(transitionTime));
    }

    public IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);

        if (_playerController != null)
            StartCoroutine(_playerController.DisablePlayer(transitionTime));
    }
}
