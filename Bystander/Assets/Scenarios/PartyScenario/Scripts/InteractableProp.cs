﻿using UnityEngine;
using System.Collections;

public class InteractableProp : MonoBehaviour
{
    public bool ImportantProp,
                HasDialog,
                HasMultipleSteps,
                AnimationChanges,
                HasCloseupAnimation;
    public int MaxClicks,
               DialogSections;
    public float CameraMoveTime,
                 CameraSize,
                 ViewTime;
    public Vector3 MyPanelPos;
    public AudioClip MySFX;
    public Animation CloseUpAnimation;

    private bool _firstDialog;
    private int _myClickCount = 0;
    private string _dialog;
    private PartyGameManager _myGameManager;
    private Vector3 _myInitialSize;

    void Start()
    {
        _myGameManager = FindObjectOfType<PartyGameManager>();
        TextAsset rawText = Resources.Load("PartyDialogText/TestDialog") as TextAsset;
        _dialog = rawText.text;
        _myInitialSize = this.GetComponent<BoxCollider>().size;
    }

    void OnMouseDown()
    {
        if (MaxClicks != _myClickCount)
        {
            if (HasMultipleSteps)
            {
                animation.clip = animation.GetClip(this.name + _myClickCount);
                Invoke("ChangeAnimation", CameraMoveTime);
            }
            else if (AnimationChanges)
                Invoke("ChangeAnimation", CameraMoveTime);
            
            _myGameManager.PlayerClicked(ImportantProp, HasDialog, CameraMoveTime, MyPanelPos, CameraSize, ViewTime);

            if(HasDialog)
            {
                if (_myGameManager.DialogHandler(ImportantProp, _dialog, DialogSections, _myInitialSize, this.GetComponent<BoxCollider>()))
                    _myClickCount++;
            }
            if (HasCloseupAnimation)
            {
                StartCoroutine(_myGameManager.PlayCloseUpAnimation(CloseUpAnimation, _myClickCount, CameraMoveTime));
                _myClickCount++;
            }
        }
    }

    private void ChangeAnimation()
    {
        animation.Play();
    }
}