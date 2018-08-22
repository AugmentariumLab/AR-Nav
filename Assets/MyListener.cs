using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;


public class MyListener : MonoBehaviour, ISpeechHandler {

    
    public static Navigator nav;
    public void Start()
    {
        nav = movement.nav;
    }



    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        
        int sphere = -1;
        switch (eventData.RecognizedText.ToLower())
        {
            case "closest":
                nav.Closest();
                
                Debug.Log("Closest Reached!\n");
                break;
            case "go to sphere":
                sphere = 0;
                break;
            case "go to one":
                sphere = 1;
                break;
            case "go to two":
                sphere = 2;
                break;
            case "go to three":
                sphere = 3;
                break;
            case "go to four":
                sphere = 4;
                break;
            case "go to five":
                sphere = 5;
                break;
            case "go to six":
                sphere = 6;
                break;
            case "go to seven":
                sphere = 7;
                break;
            case "go to eight":
                sphere = 8;
                break;
            case "go to nine":
                sphere = 9;
                break;
            case "go to ten":
                sphere = 10;
                break;
            case "go to eleven":
                sphere = 11;
                break;
            case "go to twelve":
                sphere = 12;
                break;
            case "go to thirteen":
                sphere = 13;
                break;
            case "go to fourteen":
                sphere = 14;
                break;
            case "go to fifteen":
                sphere = 15;
                break;
            case "go to sixteen":
                sphere = 16;
                break;
            case "go to seventeen":
                sphere = 17;
                break;
            case "go to eighteen":
                sphere = 18;
                break;
            case "go to nineteen":
                sphere = 19;
                break;
            case "go to twenty":
                sphere = 20;
                break;
            case "go to twenty one":
                sphere = 21;
                break;
            case "go to twenty two":
                sphere = 22;
                break;
            case "go to twenty three":
                sphere = 23;
                break;
            case "go to twenty four":
                sphere = 24;
                break;
            case "go to twenty five":
                sphere = 25;
                break;
            case "go to twenty six":
                sphere = 26;
                break;
            case "go to twenty seven":
                sphere = 27;
                break;
            case "go to twenty eight":
                sphere = 28;
                break;
            case "go to twenty nine":
                sphere = 29;
                break;
            case "go to thirty":
                sphere = 30;
                break;
            case "go to thirty one":
                sphere = 31;
                break;
            case "go to target":
                nav.Target();
                break;
            case "reset":
                nav.Reset();
                
                break;
        }
        if (sphere != -1)
        {
            nav.ProcessCommand(sphere);
            
        }
    }

    
	
	// Update is called once per frame
	void Update () {
        
	}
}
