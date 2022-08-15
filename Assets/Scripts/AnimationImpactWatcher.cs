using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationImpactWatcher : MonoBehaviour
{
    public event Action OnImpact;
    // animation event
    void Impact()
    {
        if (OnImpact != null)
        {
            OnImpact();
        }
    }
}
