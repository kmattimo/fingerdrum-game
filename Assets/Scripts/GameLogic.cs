using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {


    //public static List<KeyCode> musicKeys;

    public static Dictionary<KeyCode, int> KeyToNote;


	// Use this for initialization
	void Awake () {
        //musicKeys = new List<KeyCode>();
        KeyToNote = new Dictionary<KeyCode, int>();

        //musicKeys.Add(KeyCode.J);
        //musicKeys.Add(KeyCode.K);

        KeyToNote.Add(KeyCode.J, 36);
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
