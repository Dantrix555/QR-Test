using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QR_UI_Container : UIContainerBase
{
    [Header("Components")]
    [SerializeField] private QRMainUI_FirstLine firstLineRef = default;
    [SerializeField] private QRController ControllerRef = default;

    public async void AsyncOpen(bool haveSavedData)
    {
        ForceOpenedBase();
        await new WaitForFixedUpdate();
        SetLerpToStatus(true);
        ActivateBehaviourInteractionFlagWithDelay();
        OnContainerSetup();
    }

    protected override void OnContainerClosed()
    {
        //
    }

    protected override void OnContainerSetup()
    {
        ControllerRef.ReadQRButton.onClick.AddListener(ControllerRef.StartCamera);
        ControllerRef.GenerateQRButton.onClick.AddListener(ControllerRef.GenerateQRCode);
    }

    protected override void ResetAllContainedElements()
    {
        //
    }
}
