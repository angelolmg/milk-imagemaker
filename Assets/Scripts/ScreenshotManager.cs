using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{
    const int FILENAMELENGHT = 5;
    private string screenshotDirectoryName = "Screenshots";
    private string labelDirectoryName = "Labels";

    private string GenerateRandomString(int charAmount){
        const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
        string myString = "";

        for(int i=0; i < charAmount; i++)
            myString += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];

        return myString;

    }

    public void TakeScreenshotAndLabel(List<LabelingInfo> labelingInfos)
    {
        DirectoryInfo screenshotDirectory = Directory.CreateDirectory(screenshotDirectoryName);
        DirectoryInfo labelDirectory = Directory.CreateDirectory(labelDirectoryName);

        //string timeNow = DateTime.Now.ToString("dd-MMMM-yyyy HHmmss");
        string fileName = GenerateRandomString(FILENAMELENGHT);
        string screenshotFileName = fileName + ".jpg";
        string labelFileName = fileName + ".txt";
        string stringToWrite = "";

        foreach(LabelingInfo li in labelingInfos){
            stringToWrite +=    li.index + " " + 
                                li.x.ToString().Replace(',', '.') + " " + 
                                li.y.ToString().Replace(',', '.') + " " + 
                                li.width.ToString().Replace(',', '.') + " " + 
                                li.height.ToString().Replace(',', '.') + "\n";
        }

        string fullScreenshotPath = Path.Combine(screenshotDirectory.FullName, screenshotFileName);
        string fullLabelPath = Path.Combine(labelDirectory.FullName, labelFileName);

        //ScreenCapture.CaptureScreenshot(fullPath, 3);
        ScreenCapture.CaptureScreenshot(fullScreenshotPath);
        System.IO.File.WriteAllText(fullLabelPath, stringToWrite);
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown("space"))
        {
            //print("Taking screenshot...");
            TakeScreenshot();
        }*/
    }

}
