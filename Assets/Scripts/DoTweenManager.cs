using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenManager : MonoBehaviour
{
    void Awake()
    {
        DOTween.Init();
    }
}
