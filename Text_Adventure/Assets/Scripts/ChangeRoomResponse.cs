using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse {

	public Room roomToChangeTo;
	public string UseAtRoomName;

	public override bool DoActionResponse (GameController controller)
	{
		if (controller.roomNavigation.currentRoom.roomName == UseAtRoomName) {
			controller.roomNavigation.currentRoom = roomToChangeTo;
			controller.DisplayRoomText ();
			return true;
		}

		return false;

	}

}
