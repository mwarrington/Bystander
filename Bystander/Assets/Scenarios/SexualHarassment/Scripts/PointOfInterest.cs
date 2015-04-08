﻿using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    private SHGameManager _myGameManager;
    private SHVigilHandler _myVirgil;
    private QuizHandler _myQuiz;
    private GameObject _myMiniComic;
    private Camera _myCamera;
    private bool _showingComic;

    protected GameObject myMiniComic;

    public Transform InstantiationTransform;
    public bool IsSexualHarassment,
                ComicShown = false;

    void Start()
    {
        //Standard instantiation junk. UP, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT. WHATCHU GONNA DO NOW? A, B, START! -Konami
        _myGameManager = FindObjectOfType<SHGameManager>();
        _myVirgil = FindObjectOfType<SHVigilHandler>();
        _myCamera = this.transform.parent.GetComponentInChildren<Camera>();
        _myQuiz = GameObject.Find("Quiz True False").GetComponent<QuizHandler>();

        //The .txt used for virgil's responses and the prefabs for the mini comic are named after the PointOfInterest in question
        myMiniComic = Resources.Load("Prefabs/MiniComic_" + this.name) as GameObject;
    }

    //If the player clicks and the comic isn't already being shown then the ShowComic is called
    void OnMouseUp()
    {
        if (!ComicShown && !_myGameManager.FocusedOnPOI)
        {
            _myMiniComic = (GameObject)Instantiate(myMiniComic, InstantiationTransform.position, Quaternion.identity);
            ComicShown = true;
            _showingComic = true;
            _myGameManager.CurrentPOI = this.GetComponent<PointOfInterest>();
            _myGameManager.FocusedOnPOI = true;
        }
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f))
        {
            if (Physics2D.OverlapCircle(_myCamera.ScreenToWorldPoint(Input.mousePosition), 0.01f).transform.parent.parent == this.transform)
            {
                if (!ComicShown && !_myGameManager.FocusedOnPOI && Input.GetKeyUp(KeyCode.Mouse0))
                {
                    _myMiniComic = (GameObject)Instantiate(myMiniComic, InstantiationTransform.position, Quaternion.identity);
                    ComicShown = true;
                    _showingComic = true;
                    _myGameManager.CurrentPOI = this.GetComponent<PointOfInterest>();
                    _myGameManager.FocusedOnPOI = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _showingComic)
        {
            Destroy(_myMiniComic);
            _showingComic = false;
            _myQuiz.ShowQuiz(InstantiationTransform.position, IsSexualHarassment, true);
        }
    }
}
