using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class kyle : MonoBehaviour {
    public static kyle S;
    // Use this for initialization
    public Dictionary<int, DateTime> validNotes;
    public static double keypressWindow = .3f;
    public bool gameStarted = false;

    public int missedNotes = 0;
    public int hitNotes = 0;
    public int extraNotes = 0;
    public int loopCount = 0;

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
                missedNotes++;
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
                    hitNotes++;
                    playNote(entry.Key);
                }
                else if(Input.GetKeyDown(entry.Key))
                {
                    print("lol fat fingers");
                    extraNotes++;
                }
            }
        }

    }

    public void newLoopStart()
    {
        kyle.S.loopCount++;
    }

    public void getNote(int note)
    {
        if(validNotes.ContainsKey(note))
        {
            print("note missed, dupe");
            missedNotes++;
            removeNote(note);
        }
        validNotes[note] = DateTime.Now.AddSeconds(keypressWindow);
    }

    public void playNote(KeyCode key)
    {
        removeNote((int)key);
    }

    public void removeNote(int note)
    {
        validNotes.Remove(note);
    }

}
