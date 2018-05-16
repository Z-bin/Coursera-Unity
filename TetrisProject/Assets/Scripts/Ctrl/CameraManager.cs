using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//摄像机控制
public class CameraManager : MonoBehaviour {

    private Camera mainCamera;
    private void Awake()
    {
        //获取主相机
        mainCamera = Camera.main;
    }

    //放大
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(14.79f, 0.5f);
    }
    //缩小
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(17.27811f, 0.5f);
    }
}
