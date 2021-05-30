using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QR_UI : UIPanelBase, ISecondLineElement
{
    protected override Action OnUIClosed => throw new NotImplementedException();

    #region SecondLineElement
    public void SetupFromFirstLine()
    {
        Setup();   
    }
    #endregion

    [Header("Main QR UI Container")]
    [SerializeField] private QR_UI_Container qr_UI_Container;

    protected override void OnClosedPanel()
    {
        qr_UI_Container.ForceCloseBase();
    }

    protected override void OnSetup()
    {
        qr_UI_Container.Setup();
    }

    public async void AsyncOpen(bool haveSavedData)
    {
        ForceOpenedBase(false);
        await new WaitForUpdate();
        qr_UI_Container.AsyncOpen(haveSavedData);
        SetLerpToStatus(true);
    }
}
