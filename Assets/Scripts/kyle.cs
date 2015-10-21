using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class kyle : MonoBehaviour {
    public static kyle S;

    //notes that can be 'played' this instant
    public Dictionary<int, DateTime> validNotes;

    //store notes for a bit if they are anticipated
    public Dictionary<int, DateTime> playedNotes;

    //counts of each note per loop, for scoring, etc
    public Dictionary<int, int> loopNotes; 

    public static double keypressWindow = .3f;
    public static double keypressPreWindow = .3f;

    public bool gameStarted = false;

    public int missedNotes = 0;
    public int hitNotes = 0;
    public int extraNotes = 0;
    public int loopCount = 0;
    public int totalNotes = 0;

    void Awake()
    {
        S = this;
    }

	void Start () {
        validNotes = new Dictionary<int, DateTime>();
        playedNotes = new Dictionary<int, DateTime>();


    }

    void FixedUpdate () {

        List<int> notes = new List<int>(validNotes.Keys);


        if(Input.anyKeyDown)
        {
            //how to avoid massive manual headache for which qwerty key
            foreach (int note in notes)
            {
                if(Input.GetKeyDown( (KeyCode)note) )
                {
                    print("MATCHING KEY " + note);
                    hitNotes++;
                    playNote((KeyCode)note);
                }
                else 
                {
                    print("lol fat fingers");
                    extraNotes++;
                }
            }
        }
        notes = new List<int>(validNotes.Keys);
        foreach (int note in notes)
        {
            if (validNotes[note] < DateTime.Now)
            {
                Debug.Log("note " + note + " removed");
                missedNotes++;
                removeNote(note);
            }
        }

    }

    public void newLoopStart()
    {
        missedNotes = 0;
        hitNotes = 0;
        extraNotes = 0;
        validNotes = new Dictionary<int, DateTime>();
       //modify played notes here? 
        loopCount++;
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
