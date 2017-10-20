using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/RevealItems")]
public class RevealItemsResponse : ActionResponse {

	public string roomName;
	public List<InteractableObject> revealItems;

	public override bool DoActionResponse (GameController controller)
	{
		if (controller.roomNavigation.currentRoom.roomName == roomName) {
			for (int i = 0; i < revealItems.Count; i++) {
				revealItems[i].removeItem = false;
			}

			return true;
		
		}
		return false;
	}

}
