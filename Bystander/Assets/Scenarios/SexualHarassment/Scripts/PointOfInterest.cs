﻿using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    protected bool mouseOver
    {
        get
        {
            return _mouseOver;
        }
        set
        {
            if (value != _mouseOver)
            {
                if (value)
                {
                    MouseOverSprite.enabled = true;
                    _cursorHandler.ChangeCursor(0);
                    _myGameManager.DeselectPOIs(this);

                    if (CompanionPOI != null)
                        CompanionPOI.MouseOverSprite.enabled = true;
                }
                else
                {
                    MouseOverSprite.enabled = false;
                    _cursorHandler.ChangeCursor(1);
                    if (CompanionPOI != null)
                        CompanionPOI.MouseOverSprite.enabled = false;
                }

                _mouseOver = value;
            }
        }
    }
    private bool _mouseOver;

    private CursorHandler _cursorHandler;
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private QuizHandler _myQuiz;
    private GameObject _myMiniComic;
    private Camera _myCamera;
    private bool _showingComic;

    protected GameObject myMiniComic;

    public PointOfInterest CompanionPOI;
    public Transform InstantiationTransform;
    public SpriteRenderer MouseOverSprite;
    public bool IsSexualHarassment,
                ComicShown = false;

    void Start()
    {
        //Standard instantiation junk. UP, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT. WHATCHU GONNA DO NOW? A, B, START! -Konami
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myCamera = this.transform.parent.GetComponentInChildren<Camera>();
        _myQuiz = GameObject.Find("Quiz True False").GetComponent<QuizHandler>();
        _cursorHandler = FindObjectOfType<CursorHandler>();

        //The .txt used for virgil's responses and the prefabs for the mini comic are named after the PointOfInterest in question
        myMiniComic = Resources.Load("Prefabs/MiniComic_" + this.name) as GameObject;
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f) && (Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f).transform.parent != null) && (Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f).transform.parent.parent != null))
        {
            if (Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f).transform.parent.parent == this.transform && !_myGameManager.FocusedOnPOI)
            {
                mouseOver = true;
                if (!ComicShown && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    _myMiniComic = (GameObject)Instantiate(myMiniComic, InstantiationTransform.position, Quaternion.identity);
                    mouseOver = false;
                    ComicShown = true;
                    _showingComic = true;
                    _myGameManager.CurrentPOI = this.GetComponent<PointOfInterest>();
                    _myGameManager.FocusedOnPOI = true;
                }
            }
        }
        else
            mouseOver = false;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _showingComic)
        {
            Destroy(_myMiniComic);
            _showingComic = false;
            _myQuiz.ShowQuiz(InstantiationTransform.position, IsSexualHarassment, true);
        }
    }
}
