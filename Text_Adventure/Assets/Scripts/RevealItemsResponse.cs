using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/RevealItems")]
public class RevealItemsResponse : ActionResponse {

	public List<InteractableObject> revealItems;
	public List<Room> acceptableRooms;
	public Room changeRoom;
	public bool changeRoomDescription;
	[TextArea]
	public string changeRoomDescriptionText = "Change Room Description";

	public override bool DoActionResponse (GameController controller)
	{

		for (int r = 0; r < acceptableRooms.Count; r++) {
			if (controller.roomNavigation.currentRoom == acceptableRooms[r]) {
				for (int i = 0; i < revealItems.Count; i++) {
					revealItems [i].removeItem = false;
				}

				if (changeRoomDescription == true) {
					Debug.Log ("Changing " + changeRoom.description);
					changeRoom.description = changeRoomDescriptionText;
					Debug.Log ("It's now " + changeRoom.description);
				}

				return true;
		
			}
		}
		return false;
	}

}
