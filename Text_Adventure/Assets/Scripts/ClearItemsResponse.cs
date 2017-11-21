using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/ClearItems")]
public class ClearItemsResponse : ActionResponse {

	[TextArea]
	public string changeItemDescription = "Change Item Description";
	public InteractableObject changeItem;
	public List<InteractableObject> removeItems;


	public override bool DoActionResponse (GameController controller)
	{
		for (int i = 0; i < removeItems.Count; i++) {
			removeItems[i].removeItem = true;
		}

		for (int i = 0; i < changeItem.interactions.Length; i++) {
			if (changeItem.interactions[i].inputAction.keyWord.Equals ("examine"))
				changeItem.interactions[i].currentTextResponse = changeItemDescription; 
		}
			
		return true;
	}

}
