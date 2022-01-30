using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideFromSide : ScriptAnimation
{
    public Vector3 origin;
    public Vector3 end;

    public float duration;

    public System.Action onAnimationComplete;


    [ContextMenu("AnimateIn")]
    public override void AnimateForward()
    {
        if (transform.localPosition != end)
        {
            transform.DOLocalMove(end, duration);
        }
    }

    [ContextMenu("AnimateOut")]
    public override void AnimateBackward()
    {
        if (transform.localPosition != origin)
        {
            transform.DOLocalMove(origin, duration);
        }
    }
}
