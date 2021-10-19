using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
	[SerializeField] private SceneTheme[] sceneThemes;

	private string sceneName;
	[SerializeField] private AudioClip clipHolder;
	float fadeHolder = 0f;
	float pitchHolder = 0f;
	private bool restart = false;
	bool playingMusic = false;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		// Shows a Unity warning, but doesn't cause any real error.
		SceneManager.sceneLoaded += ((Scene scene, LoadSceneMode mode) => {
			string newSceneName = scene.name;

			if (newSceneName != sceneName) {
				sceneName = newSceneName;

				Debug.Log("Restarted Here");

				Invoke(nameof(PlayMusic), .2f);
			}
			else {
				Debug.LogWarning($"Theme: { newSceneName } not found!");
			}
		});
	}

    private void Update()
    {
        
    }

    void PlayMusic()
	{
		AudioClip clipToPlay = null;
		float fadeDuration = 0f;
		float pitch = 0f;

		for (int i = 0; i < sceneThemes.Length; i++) {
			if (sceneName == sceneThemes[i].name) {
				clipToPlay = sceneThemes[i].theme;
				fadeDuration = sceneThemes[i].fadeDuration;
				pitch = sceneThemes[i].pitch;
			}
		}

		if (clipToPlay != null) {
			clipHolder = clipToPlay;
			fadeHolder = fadeDuration;
			pitchHolder = pitch;
			playingMusic = true;

			Debug.Log($"Length { clipHolder.length }");

			AudioManager._I.PlayMusic(clipToPlay, fadeDuration, pitch);

			StartCoroutine(RestartMusic(clipToPlay.length, sceneName));
        }
	}

	public void Restart()
    {
		if (restart) {
			AudioManager._I.PlayMusic(clipHolder, fadeHolder, pitchHolder);

			Invoke(nameof(PlayMusic), clipHolder.length);

			restart = false;
		}
	}

	public IEnumerator RestartMusic(float time, string _sceneName)
    {
		yield return new WaitForSeconds(time);

		if (_sceneName == sceneName) {
			Invoke(nameof(PlayMusic), .2f);
		}
    }

	[System.Serializable]
	public class SceneTheme
	{
		public string name;
		public AudioClip theme;
		public float fadeDuration;
		public float pitch;
	}
}
