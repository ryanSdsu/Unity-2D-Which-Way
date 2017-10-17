using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/ClearItems")]
public class ClearItemsResponse : ActionResponse {


	public Room roomToChangeTo;

	public override bool DoActionResponse (GameController controller)
	{
		for (int i = 0; i < removeItems.Count; i++) {
			removeItems[i].removeItem = true;
		}

		for (int i = 0; i < changeItem.interactions.Length; i++) {
			if (changeItem.interactions[i].inputAction.keyWord.Equals ("examine"))
				changeItem.interactions[i].currentTextResponse = "(Ah the Captain's unfirom. A display of leadership and even possible mutiny.)"; 
		}



		return true;
	}

}
