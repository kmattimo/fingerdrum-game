using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class kyle : MonoBehaviour {
    public static kyle S;
    // Use this for initialization
    public Dictionary<int, DateTime> validNotes;
    public static float keypressWindow = 2f;

    void Awake()
    {
        S = this;
    }

	void Start () {
        validNotes = new Dictionary<int, DateTime>();
	}
	
	void FixedUpdate () {
        List<int> notes = new List<int>(validNotes.Keys);
        foreach(int note in notes)
        {
            if (validNotes[note] < DateTime.Now)
            {
                Debug.Log("note " + note + " removed");
                removeNote(note);
            }
        }

        if(Input.anyKeyDown)
        {
            //how to avoid massive manual headache for which qwerty key
            foreach (KeyValuePair<KeyCode, int> entry in GameLogic.KeyToNote)
            {
                if(Input.GetKeyDown(entry.Key) && validNotes.ContainsKey(GameLogic.KeyToNote[entry.Key] ))
                {
                    print("MATCHING KEY " + entry.Value);
                    playNote(entry.Key);
                }
            }
        }

    }

    public void getNote(int note)
    {
        if(validNotes.ContainsKey(note))
        {
            print("note missed, dupe");
        }
        validNotes[note] = DateTime.Now.AddSeconds(1);
    }

    public void playNote(KeyCode key)
    {
        removeNote(GameLogic.KeyToNote[key]);
    }

    public void removeNote(int note)
    {
        validNotes.Remove(note);
    }

}
