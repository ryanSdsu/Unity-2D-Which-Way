using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room> ();
	GameController controller;
	public Animator winScreenAnimator;
	public CanvasGroup mainCanvas;


	void Awake() 
	{
		controller = GetComponent<GameController> ();
	}

	public void UnpackExitsInRoom() {
	
		for (int i = 0; i < currentRoom.exits.Length; i++) {
			exitDictionary.Add (currentRoom.exits [i].keyString, currentRoom.exits [i].valueRoom);
			if (!currentRoom.exits [i].exitDescription.Equals(""))
				controller.interactionDescriptionsInRoom.Add (currentRoom.exits [i].exitDescription);
		}
	}

	public void AttemptToChangeRooms(string directionNoun) {
	
		if (exitDictionary.ContainsKey (directionNoun)) {

			currentRoom = exitDictionary [directionNoun];


			if (currentRoom.isWinRoom) {

				winScreenAnimator.SetBool ("IsActive", true);

			}
				
			controller.LogStringWithReturn ("You head off to the " + directionNoun);
			controller.DisplayRoomText ();
			controller.PlayClip ("go");

			StartCoroutine(loadCutScene());




		} else {

			controller.LogStringWithReturn ("That is not a valid path.");
		}
	
	}

	IEnumerator loadCutScene()
	{

		Scene main = SceneManager.GetSceneByName ("Main");
		SceneManager.LoadScene ("WalkingScene", LoadSceneMode.Additive);
		float time = 1f;
		while(mainCanvas.alpha > 0)
		{
			mainCanvas.alpha -= Time.deltaTime / time;
		}

		//Wait for 15 seconds
		yield return new WaitForSeconds(15);		Debug.Log ("waiting");

		while(mainCanvas.alpha != 1)
		{
			mainCanvas.alpha += Time.deltaTime / time;
		}
		SceneManager.UnloadSceneAsync ("WalkingScene");


	}
		

	public void ClearExits() {

		exitDictionary.Clear ();
	}

}
