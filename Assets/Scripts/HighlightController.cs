﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighlightController : MonoBehaviour
{

    public enum DoWhatOn
    {
        AddListener,
        RemoveListener,
        DoNothing
    }

    public DoWhatOn OnEnter;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //1. Define delegates and events
    //2. define methods to call the events 
    //3. subscribe gameobjects to the events
    //4. find a way to call the events (button click)


    public delegate void PadHandler();

    public static event PadHandler OnPad;


    public static bool Contains(Highlightable obj)
    {
        if (OnPad == null) return false;
        Delegate[] invocList = OnPad.GetInvocationList();

        foreach (Delegate d in invocList)
        {
#pragma warning disable 252
            if (d.Target == obj) // yes, compare references
#pragma warning restore 252
                return true;
        }


        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " entered me");
        Highlightable hi = other.gameObject.GetComponent<Highlightable>();

        switch (OnEnter)
        {
            case DoWhatOn.AddListener:
                if (!HighlightController.Contains(hi))
                {
                    OnPad += hi.ToggleHighlightMaterial;
                    //Debug.Log("Adding callback");
                }
                break;
            case DoWhatOn.RemoveListener:
                OnPad -= hi.ToggleHighlightMaterial;
                break;
            case DoWhatOn.DoNothing:
                break;
            default:
                break;
        }
    }








    public static void DoSomething()
    {

        if (OnPad != null) OnPad();

    }
}
