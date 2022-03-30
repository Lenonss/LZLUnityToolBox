using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class FlyText : MonoBehaviour
{
    public enum FlyDirection
    {
        Up,Left,Right,Down
    }

    [Header("飞行方向")]
    public FlyDirection flyDir;
    [Header("飞行距离")]
    public float flyDistance = 0;
    [Header("飞行时长")]
    public float flyDuration;
    [Header("是否整数飞行")]
    public bool isSnapping;
    [Header("飞行结束后调用")]
    public UnityEvent flyOverCall;

    private void Awake()
    {
        transform.DOMove(transform.position + GetPlusDictance(), flyDuration,isSnapping);
        DOVirtual.DelayedCall(flyDuration, () => flyOverCall?.Invoke());
    }

    private Vector3 GetPlusDictance()
    {
        Vector3 result = Vector3.zero;
        switch (flyDir)
        {
            case FlyDirection.Up:
                result.y = Mathf.Abs(flyDistance);
                break;
            case FlyDirection.Down:
                result.y = -Mathf.Abs(flyDistance);
                break;
            case FlyDirection.Left:
                result.x = -Mathf.Abs(flyDistance);
                break;
            case FlyDirection.Right:
                result.x = Mathf.Abs(flyDistance);
                break;
        }

        return result;
    }
}
