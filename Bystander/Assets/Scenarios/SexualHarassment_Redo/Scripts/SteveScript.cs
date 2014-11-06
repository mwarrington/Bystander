﻿using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SteveScript : MonoBehaviour {

	public UILabel mySpeechText; 

	private int steveLinesCounter;

	public GameObject myManager;
	private GuiDanceOfficeManager myManagerScript;

	Dictionary<int, string> steveLines = new Dictionary<int, string>();

	//private List<string> steveLines = new List<string>();

	// Use this for initialization
	void Start () {

		myManagerScript = myManager.GetComponent<GuiDanceOfficeManager>();

		steveLines.Add (1, "Wha ");
		steveLines.Add (3,"What's the matter? ajsdlfkjalkdjflkjlaksdfjlajdlfjlajljsdlfjlkajldjfljalkdsjflkjakldfjkljaldjflkjalkjsdlkfjlkjalkjsldkfjlkjalksjdlfkjlkjlka"); 
		steveLines.Add (5, "(Clears throat)*"); 
		steveLines.Add (7, "Well, here we are. At the counselor's office"); 
		steveLines.Add (9,"Step up. It's time to shed some light on this."); 
		steveLines.Add (11, "I can only lead you so far."); 
		steveLines.Add (13, "Do you see it a lot? In the halls and stuff?"); 

	}

	public void displayCorrectDialogue(){
		steveLinesCounter++;

		string temp = null;

		if (steveLines.TryGetValue(steveLinesCounter, out temp)) {
			mySpeechText.text = steveLines [steveLinesCounter];
		}
	}
}
