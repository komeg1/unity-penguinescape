using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField]private Animator playerAnimator;
    [SerializeField] private AnimatorOverrideController[] skinArray;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetAnimations(int skinNumber)
    {
        playerAnimator.runtimeAnimatorController = skinArray[skinNumber];
    }
}
