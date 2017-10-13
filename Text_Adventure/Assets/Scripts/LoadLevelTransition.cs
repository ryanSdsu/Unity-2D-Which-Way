using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTransition : MonoBehaviour {

	public Animator LoadLevelAnimator;

	private void Start() {

		LoadLevelAnimator = GetComponent<Animator>();
	}

	public void LoadLevel(string levelName) {

		SceneManager.LoadScene (levelName);

	}

	public void startFade() {

		LoadLevelAnimator.SetBool ("IsActive", true);

	}
}
