using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using UnityEngine.UI;

public class CreateTomograph : MonoBehaviour
{
    public static int MAX_SIZE_OF_TOMOGRAPH = 1024;  //number of frames in AIM file. modificable
    public GameObject TomographPrefab;
    private TextToSpeech textToSpeech;
    private BigImage bigImage;
    private Image bigFrameImage;
    List<Texture2D> allImagesForFrames;
    ReadAIMFile readAIM;
    

    // Use this for initialization
    void Start ()
    {
        textToSpeech = GameObject.Find("Audio Manager").GetComponent<TextToSpeech>();
        bigImage = GetComponent<BigImage>();
        readAIM = GameObject.FindGameObjectWithTag("Manager").GetComponent<ReadAIMFile>();
        
        allImagesForFrames = readAIM.Frames;
        bigFrameImage = GetComponent<Image>();

        //string text = "some text to say....";
        //textToSpeech.StartSpeaking(text);
        //textToSpeech.StopSpeaking();
    }

    public void CreateTomographInLine()
    {
        Debug.Log("CreateTomograph.cs CreateTomographInLine() start");
        bool isItX = true;
        int numberOfRowOrColumn = 0;
        if(CheckPoints(ref isItX, ref numberOfRowOrColumn) )
        {
#if !UNITY_EDITOR
            //string text = "Creating Tomograph in one line" + 5 ;
            //textToSpeech.StartSpeaking(text);
            //textToSpeech.StopSpeaking(); 
#else
            Debug.Log("Good points, isItX = " + isItX + " numberOfRowOrColumn = " + numberOfRowOrColumn);
#endif
            
            Color [] oneRowOrColumn = null;
            int numberOfFrame = 0;
            GameObject tomographGameObject;

            if(isItX) //we have X tomograph
            {

#if !UNITY_EDITOR
            string text = "Creating Tomograph on X equals " + numberOfRowOrColumn ;
            textToSpeech.StartSpeaking(text);
            textToSpeech.StopSpeaking(); 
#endif

                Texture2D tomograhpImage = new Texture2D(MAX_SIZE_OF_TOMOGRAPH, 32);
                Debug.Log("We have X");
                foreach ( var frame in allImagesForFrames )
                {
                    oneRowOrColumn = frame.GetPixels(numberOfRowOrColumn, 0, 1, 31);
                    tomograhpImage.SetPixels( numberOfFrame, 0, 1, 31, oneRowOrColumn);
                    numberOfFrame++;
                }
                
                tomograhpImage.Apply();
                tomographGameObject = Instantiate(TomographPrefab);
                tomographGameObject.gameObject.GetComponent<Image>().sprite = Sprite.Create(tomograhpImage, new Rect(0, 0, numberOfFrame, 31), new Vector2(0, 0));
                tomographGameObject.transform.SetParent(gameObject.transform.parent, false);
                tomographGameObject.transform.localScale = new Vector3 (0.4f * ( (numberOfFrame / 32) + ( (float)(numberOfFrame % 32) / 32) ), tomographGameObject.transform.localScale.y, tomographGameObject.transform.localScale.z);
                bigFrameImage.enabled = false;

                bigImage.FirstPoint = new Vector2Int(0, 0);
                bigImage.SecondPoint = new Vector2Int(0, 0);
            }
            else     //we have Y tomograph
            {

#if !UNITY_EDITOR
            string text = "Creating Tomograph on Y equals " + numberOfRowOrColumn ;
            textToSpeech.StartSpeaking(text);
            textToSpeech.StopSpeaking(); 
#endif

                Texture2D tomograhpImage = new Texture2D(32, MAX_SIZE_OF_TOMOGRAPH);
                Debug.Log("We have Y");
                foreach ( var frame in allImagesForFrames )
                {
                    oneRowOrColumn = frame.GetPixels(0, numberOfRowOrColumn, 31, 1);
                    tomograhpImage.SetPixels(0, numberOfFrame, 31, 1, oneRowOrColumn);
                    numberOfFrame++;
                }

                tomograhpImage.Apply();
                tomographGameObject = Instantiate(TomographPrefab);
                tomographGameObject.gameObject.GetComponent<Image>().sprite = Sprite.Create(tomograhpImage, new Rect(0, 0, 31, numberOfFrame), new Vector2(0, 0));
                tomographGameObject.transform.SetParent(gameObject.transform.parent, false);
                //tomographGameObject.transform.localScale.y = new float(2f);
                tomographGameObject.transform.localScale = new Vector3(tomographGameObject.transform.localScale.x,
                                             0.4f * ( ( numberOfFrame / 32 ) + ( (float)( numberOfFrame % 32 ) / 32 ) ), 
                                                  tomographGameObject.transform.localScale.z);
                bigFrameImage.enabled = false;

                bigImage.FirstPoint = new Vector2Int(0, 0);
                bigImage.SecondPoint = new Vector2Int(0, 0);
            }

        }
        else
        {
#if !UNITY_EDITOR
            string text = "Please select points in one line and try again!";
            textToSpeech.StartSpeaking(text);
            textToSpeech.StopSpeaking();
#else
            Debug.Log("Wrong points");
#endif
        }

    }

    private bool CheckPoints(ref bool isItX, ref int numberOfRowOrColumn)
    {
        Vector2Int firstPoint = bigImage.FirstPoint;
        Vector2Int secondPoint = bigImage.SecondPoint;
        if(firstPoint.magnitude == 0 && secondPoint.magnitude == 0)
        {
            return false;
        }
        else if(firstPoint.x == secondPoint.x)
        {
            isItX = true;
            numberOfRowOrColumn = firstPoint.x;
            return true;
        }
        else if( firstPoint.y == secondPoint.y )
        {
            isItX = false;
            numberOfRowOrColumn = firstPoint.y;
            return true;
        }
        else
        {
            return false;
        }
    }
}
