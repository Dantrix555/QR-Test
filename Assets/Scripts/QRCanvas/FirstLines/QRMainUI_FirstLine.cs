using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QRMainUI_FirstLine : FirstLineElements
{

    [SerializeField] private QR_UI mainQR_UI;

    #region FirstLineElementsOverrides
    protected override void OnDestroyFirstLine()
    {
        mainQR_UI.ForceCloseBase();
    }

    protected override void OnResetFirstLine()
    {
        ForceOpenedBase();
        mainQR_UI.ForceOpenedBase();
    }

    protected override void OnSetupFirstLine()
    {
        ForceOpenedBase();
        mainQR_UI.ForceOpenedBase();
        mainQR_UI.AsyncOpen(true);
    }
    #endregion


}
