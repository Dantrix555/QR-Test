# QR-Test
 This is a QR implementation project based on this [zxing Unity QR repository](https://github.com/nenuadrian/qr-code-unity-3d-read-generate/ "zxing Unity QR repository").
 
 You can read and generate QR codes, just start "QRScene" located in Scenes folder and try to create a QR code by typing a text you want to encrypt or read a qr code and the text component (at the top of the scene) will show text inside the read code. If you want to do an implementation using QR in another project, I will give you some recommendations (or "mini-tutorial") below.

## ZXing plugin configuration
 First of all It's important to import zxing.unity.dll inside your plugins folder (if you don't have that folder, just create a folder with that name inside your assets folder).

 After that you can use zxing to create or generate your qr codes, for example:

 ```C#
 using ZXing;
 using System;
 using UnityEngine;
 using UnityEngine.UI;
 using ZXing.QrCode;

 public class QRGeneratorController : MonoBehaviour
 {
    public RawImage _qrImageTex;
    public private Text _qrOutputText;
    public private InputField _stringField;
 
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
        Texture2D myQR = generateQR(_stringField.text);
        _qrImageTex.texture = myQR;
    }

    public void CreateCodeButtonMethod()
    {
        GenerateQRCode();
    }
 }
 
```

 In the previous example is shown an implementation of zxing inside a QR generator C# Script.

 After that test your script ;)

## Android Camera Permision
 The project can be exported to Android, but it's important to do some configuration before project building:

* You need to create a custom Android manifest file. To do this you can go to top menu "Edit/Project Settings/Player Settings/Publishing Settings" and check "Custom Main Manifest". After doing that in your project will appear an android folder inside your plugins folder.

* Go to the Android folder inside plugins folder and open AndroidManifest.xml, update your code with something like this:
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    xmlns:android="http://schemas.android.com/apk/res/android"
    package="com.unity3d.player"
    xmlns:tools="http://schemas.android.com/tools">
    <application>
        <activity android:name="com.unity3d.player.UnityPlayerActivity"
                android:theme="@style/UnityThemeSelector">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
            <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
        </activity>
    </application>
<uses-permission android:name="android.permission.CAMERA"/>
</manifest>
```

* A good practice in unity when you're using Android permissions it's to add a permission inside your code using Unity's android namespace (normally that permission is better to call it once in your code), an example code could be the following:

```C#
using UnityEngine.Android;

public class InitGameScript : MonoBehaviour
{
    void Awake()
    {
        #if PLATFORM_ANDROID
                //Request camera permission if executing platform is android
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                {
                    Permission.RequestUserPermission(Permission.Camera);
                }
        #endif
    }
}
```

* As adittional tip to your Android implementation is to rotate your QR code Raw Image 270° in z axis, that's because for some reason when the apk is generated, the raw image (where you create or read your code) looks rotated.

Feel free to clone my project and I strongly recommend to check the repository that I use as my guide to create this one: https://github.com/nenuadrian/qr-code-unity-3d-read-generate
