using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private QRMainUI_FirstLine mainUI;
    public QRMainUI_FirstLine MainUI => mainUI;

    private void Awake()
    {
        MainUI.SetupFirstLine();
        MainUI.OpenFirstLine();
    }
}
