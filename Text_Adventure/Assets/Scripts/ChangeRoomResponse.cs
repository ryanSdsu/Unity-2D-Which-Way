using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse {

	public Room roomToChangeTo;

	public override bool DoActionResponse (GameController controller)
	{
		if (controller.roomNavigation.currentRoom.roomName == roomName) {
			controller.roomNavigation.currentRoom = roomToChangeTo;
			controller.DisplayRoomText ();
			return true;
		}

		return false;

	}

}
