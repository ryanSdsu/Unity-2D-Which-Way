using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPannelController : MonoBehaviour {

	public GameObject HelpPanel;

	public void toggleHelpPannel () {

		if (HelpPanel.activeSelf) {
			HelpPanel.SetActive(false);
		} else {
			HelpPanel.SetActive(true);
		}
	}


}
