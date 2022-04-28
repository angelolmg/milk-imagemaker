using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScreenshotManager : MonoBehaviour
{

    private string screenshotDirectoryName = "Screenshots";
    private string labelDirectoryName = "Labels";

    public void TakeScreenshotAndLabel(List<LabelingInfo> labelingInfos)
    {
        DirectoryInfo screenshotDirectory = Directory.CreateDirectory(screenshotDirectoryName);
        DirectoryInfo labelDirectory = Directory.CreateDirectory(labelDirectoryName);

        string timeNow = DateTime.Now.ToString("dd-MMMM-yyyy HHmmss");
        string screenshotFileName = timeNow + ".jpg";
        string labelFileName = timeNow + ".txt";
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
