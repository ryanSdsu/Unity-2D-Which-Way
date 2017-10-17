using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject {

	public string roomName;
	public bool breakableItem;
	public abstract bool DoActionResponse (GameController controller);
	public Color roomColor = Color.black;
	public bool changeRoomColor;
	public List<InteractableObject> removeItems;
	public InteractableObject changeItem;



}
