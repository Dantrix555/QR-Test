using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIPanelBase : UIBase
{
    protected override float DefaultLerpDuration => 0.2f;

    protected abstract void OnClosedPanel();

    protected override Action OnUIClosed => OnClosedPanel;

    public virtual async void AsyncOpen()
    {
        await new WaitForUpdate();
        Debug.LogWarning(transform.name + " no hace OVERWRITE a metodo de >AsycnOpen deberia tener un polimorfismo ");
    }

    protected async void HandleAutoClose(float autoCloseDelay)
    {
        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(autoCloseDelay));
        if (UIBaseIsTotallyActive)
        {
            SetLerpToStatus(false);
        }
        else
        {
            Debug.LogWarning("ALGO ANDA MAL? un metodo asycn pudo haberse filtrado, intento de cerrar una ventana muy posiblemente cerrada antes de manera forzada en HandleAutoClose");
        }
    }

    public void Setup()
    {
        ForceCloseBase();
        OnSetup();
    }

    protected abstract void OnSetup();
}
