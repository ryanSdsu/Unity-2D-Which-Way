using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RunScene : MonoBehaviour {

	public PlayableDirector maintimeline;

	// Use this for initialization
	void Start () {
		maintimeline.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
