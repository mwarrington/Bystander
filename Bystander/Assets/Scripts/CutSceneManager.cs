﻿using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour
{
    public Step[] Page;

    private Rect _rect,
                 _rectDiff;
    private Vector2 _pos;
    private float _camTravelTime,
                  _camSize,
                  _camSizeDiff;
    private int _currentStep;
    private bool _movingCamera;

    void OnMouseDown()
    {
        SetCameraToMove();
        _currentStep++;
    }

    void Update()
    {
        if (_movingCamera)
            MoveCameraTo();
    }

    //This method initializes the moving process; setting impertinant fields and also invokes StopMoving
    public void SetCameraToMove()
    {
        _pos = Page[_currentStep].CamLocation;
        _rect = Page[_currentStep].CamRectangle;
        _camTravelTime = Page[_currentStep].CamTravelTime;
        _camSize = Page[_currentStep].CamSize;
        _camSizeDiff = Mathf.Abs(this.camera.orthographicSize - Page[_currentStep].CamSize);
        _rectDiff = new Rect(Mathf.Abs(this.camera.rect.x - _rect.x), Mathf.Abs(this.camera.rect.y - _rect.y), Mathf.Abs(this.camera.rect.width - _rect.width), Mathf.Abs(this.camera.rect.height - _rect.height));
        _movingCamera = true;
        Invoke("StopMoving", Page[_currentStep].CamTravelTime);
    }

    private void MoveCameraTo()
    {
        //Using the iTween method MoveTo we set an object to move, a location, and a speed; iTween handles the rest
        iTween.MoveTo(this.gameObject, new Vector3(_pos.x, _pos.y, this.transform.position.z), _camTravelTime);

        //If the current camera size is greater than _camSize then we know we need to decrease the size of the camera
        if (this.camera.orthographicSize > _camSize)
        {
            this.camera.orthographicSize -= _camSizeDiff / (_camTravelTime / Time.deltaTime);

            //Eventually the camera size will be slightly smaller than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize < _camSize)
                this.camera.orthographicSize = _camSize;
        }
        else if (this.camera.orthographicSize < _camSize) //If the current camera size is less than _camSize then we know we need to increase the size of the camera
        {
            this.camera.orthographicSize += _camSizeDiff / (_camTravelTime / Time.deltaTime);

            //Eventually the camera size will be slightly larger than the desired size; once this happens we simply set the size to _camSize
            if (this.camera.orthographicSize > _camSize)
                this.camera.orthographicSize = _camSize;
        }

        //These lines handle the rectangle changes
        float x = this.camera.rect.x;
        float y = this.camera.rect.y;
        float width = this.camera.rect.width;
        float height = this.camera.rect.height;
        if (x > _rect.x)
        {
            x -= _rectDiff.x / (_camTravelTime / Time.deltaTime);
        }
        else
        {
            x = _rect.x;
        }
        if (y > _rect.y)
        {
            y -= _rectDiff.y / (_camTravelTime / Time.deltaTime);
        }
        else
        {
            y = _rect.y;
        }
        if (width > _rect.width)
        {
            width -= _rectDiff.width / (_camTravelTime / Time.deltaTime);
        }
        else
        {
            width = _rect.width;
        }
        if (height > _rect.height)
        {
            height -= _rectDiff.height / (_camTravelTime / Time.deltaTime);
        }
        else
        {
            height = _rect.height;
        }
        this.camera.rect = new Rect(x, y, width, height);
    }
}