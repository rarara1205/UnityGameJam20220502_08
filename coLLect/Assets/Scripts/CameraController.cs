using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameraList;
    [HideInInspector] public Transform follow;

    private int unselectPriority = 0;
    private int selectPriority = 10;
    private int currentCameraNum = 0;

    private void Awake()
    {
        for(int i=0; i<cameraList.Length; i++)
        {
            cameraList[i].Priority = (i == currentCameraNum? selectPriority : unselectPriority);
        }
    }

    public void FollowPlayer(Transform playerTransform)
    {
        cameraList[currentCameraNum].Follow = playerTransform;
    }

    public void ChangeCamera(int nextCameraNum)
    {
        CinemachineVirtualCamera cameraCurrent = cameraList[currentCameraNum];
        cameraCurrent.Priority = unselectPriority;
        CinemachineVirtualCamera cameraNext = cameraList[nextCameraNum];
        cameraNext.Priority = selectPriority;
        currentCameraNum = nextCameraNum;
    }
}
