using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "TextAdventure/Room")]
public class Room : ScriptableObject {

	[TextArea]
	public string description;
	public string roomName;
	public Exit[] exits;
	public InteractableObject[] interactableObjectsInRoom;
	public bool isWinRoom;
	public Color roomColor = Color.black;
	GameController controller;

}
