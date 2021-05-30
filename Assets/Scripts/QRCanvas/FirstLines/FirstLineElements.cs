using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class FirstLineElements : UIBase
{
    protected override Action OnUIClosed => null;

    protected override float DefaultLerpDuration => 0.5f;

    public void SetupFirstLine()
    {
        OnSetupFirstLine();
    }

    protected abstract void OnSetupFirstLine();

    public void DestroyFirstLine(bool isForced = true)
    {
        if(isForced)
        {
            ForceCloseBase();
        }
        else
        {
            SetLerpToStatus(false);
        }

        OnDestroyFirstLine();
    }

    protected abstract void OnDestroyFirstLine();

    public void ResetFirstLine()
    {
        OnResetFirstLine();
    }

    protected abstract void OnResetFirstLine();

    public virtual void OpenFirstLine()
    {
        Debug.LogWarning(transform.name + " no hace overwrite a método de >OpenFirstLine debería tener un polimorfismo");
    }
}
