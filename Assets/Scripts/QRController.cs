using ZXing;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using ZXing.QrCode;

public class QRController : MonoBehaviour
{
    [SerializeField] private RawImage _imageTex;
    [SerializeField] private RawImage _qrImageTex;
    [SerializeField] private Text _qrOutputText;
    [SerializeField] private InputField _stringField;
    [SerializeField] private Button _generateQRButton;
    public Button GenerateQRButton => _generateQRButton;

    [SerializeField] private Button _readQRButton;
    public Button ReadQRButton => _readQRButton;

    private WebCamTexture _camTexture;
    private bool _isReadingQR = false;

    void Awake()
    {
        #if PLATFORM_ANDROID
                //Request microphone permission if is in android
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                {
                    Permission.RequestUserPermission(Permission.Camera);
                }
        #endif
    }

    private void Start()
    {
        _camTexture = new WebCamTexture();
        _imageTex.texture = _camTexture;
        _imageTex.material.mainTexture = _camTexture;
    }

    public void StartCamera()
    {
        if(!_isReadingQR)
        {
            _isReadingQR = true;

            //Capture web cam in rawImage texture
            _imageTex.gameObject.SetActive(true);
            _qrImageTex.gameObject.SetActive(false);
            
            _camTexture.Play();
        }
    }

    private void PauseCamera() 
    {
        if(_isReadingQR)
        {
            _isReadingQR = false;
            _camTexture.Pause();
            _imageTex.gameObject.SetActive(false);
            _qrImageTex.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(_isReadingQR)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                // decode the current frame
                var result = barcodeReader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);
                if (result != null)
                {
                    _qrOutputText.text = result.Text;
                }
                else
                {
                    _qrOutputText.text = "";
                }
            }
            catch (Exception ex) { _qrOutputText.text = "Please connect a camera!"; }
        }
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public Texture2D generateQR(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }

    public void GenerateQRCode()
    {
        if(_stringField.text.Length > 0)
        {
            PauseCamera();
            Texture2D myQR = generateQR(_stringField.text);
            _qrImageTex.texture = myQR;
        }
    }
}

