using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;

	Dictionary<string, Room> exitDictionary = new Dictionary<string, Room> ();
	GameController controller;
	public Animator winScreenAnimator;


	void Awake() 
	{
		controller = GetComponent<GameController> ();
	}

	public void UnpackExitsInRoom() {
	
		for (int i = 0; i < currentRoom.exits.Length; i++) {
			exitDictionary.Add (currentRoom.exits [i].keyString, currentRoom.exits [i].valueRoom);
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

			//Changing the background color
			//BackgroundOfRoom.color = currentRoom.roomColor;


		} else {

			controller.LogStringWithReturn ("That is not a valid path.");
		}
	
	}

	public void ClearExits() {

		exitDictionary.Clear ();
	}

}
