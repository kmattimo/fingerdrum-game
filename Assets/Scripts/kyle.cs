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
    //initialized in unitysynthtest >:|
    public Dictionary<int, int> loopNotes;
    public List<int> allNotesInLoop;

    public static double keypressWindow = .4f;
    public static double keypressPreWindow = .4f;
    public static int metronomeKey = 126;

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
            foreach (int note in allNotesInLoop)
            {
                if (Input.GetKeyDown((KeyCode)note))
                {
                    if(playedNotes.ContainsKey(note) )
                    {
                        extraNotes++;
                    }
                    playedNotes[note] = DateTime.Now.AddSeconds(keypressPreWindow);
                }
            }

            //avoiding massive manual headache for which qwerty key
            foreach (int note in notes)
            {
                if(Input.GetKeyDown( (KeyCode)note) )
                {
                    print("MATCHING KEY " + note);
                    hitNotes++;
                    if(playedNotes.ContainsKey(note))
                    {
                        playedNotes.Remove(note);
                    }
                    validNotes.Remove(note);
                }
                else 
                {
                    //never hits here?
                   // print("lol fat fingers");
                    //extraNotes++;
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
                validNotes.Remove(note);
            }
        }
        List<int> playedNoteNumbers = new List<int>(playedNotes.Keys);
        foreach (int note in playedNoteNumbers)
        {
            if (playedNotes[note] < DateTime.Now)
            {
                Debug.Log("Played Note " + note + " Expired");
                playedNotes.Remove(note);
                extraNotes++;            
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
        if(playedNotes.ContainsKey(note) )
        {
            print("DEEZ NUTS GOT EM BITCH");
            playedNotes.Remove(note);
            hitNotes++;
            return;
        }

        if(validNotes.ContainsKey(note))
        {
            print("note missed, dupe");
            missedNotes++;
            validNotes.Remove(note);
        }
        validNotes[note] = DateTime.Now.AddSeconds(keypressWindow);
    }

  
}
