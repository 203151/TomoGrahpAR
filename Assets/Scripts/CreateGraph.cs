using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using UnityEngine.UI;

public class CreateGraph : MonoBehaviour
{
    public static int MAX_SIZE_OF_TOMOGRAPH = 10240;  //number of frames in AIM file. modificable
    public static int MAX_HEIGHT_OF_TOMOGRAPH = 2024;  //range of values in frames in AIM file. modificable
    public GameObject graphPrefab;
    private TextToSpeech textToSpeech;
    private BigImage bigImage;
    private Image bigFrameImage;
    private List<List<float>> values;

    private float [] maxGraphValue, minGraphValue;

    ReadAIMFile readAIM;
    // Use this for initialization
    void Start ()
    {
        textToSpeech = GameObject.Find("Audio Manager").GetComponent<TextToSpeech>();
        bigImage = GetComponent<BigImage>();
        readAIM = GameObject.FindGameObjectWithTag("Manager").GetComponent<ReadAIMFile>();
        values = readAIM.Values;
        //allImagesForFrames = readAIM.Frames;
        bigFrameImage = GetComponent<Image>();

        maxGraphValue = new float[1024];
        minGraphValue = new float[1024];

        
        foreach(var frame in values)
        {
            int i = 0;
            foreach (var point in frame)
            {
                if(point > maxGraphValue[i])
                {
                    maxGraphValue[i] = point;
                }
                else if(point < minGraphValue[i])
                {
                    minGraphValue[i] = point;
                }
                i++;
            }
           
        }

    }

    public void CreateGraphInPoint()
    {
        Debug.Log("CreateGraph.cs CreateGraphInPoint() start");
        Vector2Int point = new Vector2Int(0, 0);
       if(CheckPoints(ref point))
        {
#if !UNITY_EDITOR
            string text = "Creating Graph on coordinates equals X " + point.x + "Y " + point.y ;
            textToSpeech.StartSpeaking(text);
            textToSpeech.StopSpeaking(); 
#endif
        }
        else
        {
            return;
        }


        int numberOfFrame = 0;
        Texture2D graphImage = new Texture2D(MAX_SIZE_OF_TOMOGRAPH, MAX_HEIGHT_OF_TOMOGRAPH);
        
        GameObject graphGameObject;

        Debug.Log("before setting the graph");
        Debug.Log("point" + point.x + "  " + point.y);
        //Debug.Log(minGraphValue.Count);
        Debug.Log("minGrapValue = " + minGraphValue[(point.x * 32) + point.y]);
        Debug.Log("maxGrapValue = " + maxGraphValue[(point.x * 32) + point.y]);
        foreach (var value in values)
        {
 
            graphImage.SetPixel(numberOfFrame, (int)((value[(point.x * 32) + point.y] + ( Mathf.Abs(minGraphValue[(point.x * 32) + point.y]) + 0.1 ) ) * 1000), Color.red);
            numberOfFrame++;
        }

        Debug.Log("after setting the graph");

        graphImage.Apply();
        graphGameObject = Instantiate(graphPrefab);
        graphGameObject.gameObject.GetComponent<Image>().sprite = Sprite.Create(graphImage, new Rect(0, 0, numberOfFrame, MAX_HEIGHT_OF_TOMOGRAPH), new Vector2(0, 0));
        graphGameObject.transform.SetParent(gameObject.transform.parent, false);
        //graphGameObject.transform.localScale = new Vector3(0.4f * ((numberOfFrame / 32) + ((float)(numberOfFrame % 32) / 32)), graphGameObject.transform.localScale.y, graphGameObject.transform.localScale.z);
        bigFrameImage.enabled = false;

        Debug.Log("all done");

        bigImage.FirstPoint = new Vector2Int(0, 0);
        bigImage.SecondPoint = new Vector2Int(0, 0);
    }
    private bool CheckPoints(ref Vector2Int point)
    {
        if ((bigImage.FirstPoint.magnitude != 0) && (bigImage.SecondPoint.magnitude != 0))
        {
#if !UNITY_EDITOR
            string text = "Please select only one point on the frame";
            textToSpeech.StartSpeaking(text);
            textToSpeech.StopSpeaking(); 
#endif
            return false;
        }
        else if (bigImage.FirstPoint.magnitude != 0)
        {
            point = bigImage.FirstPoint;
            return true;
        }
        else if (bigImage.SecondPoint.magnitude != 0)
        {
            point = bigImage.SecondPoint;
            return true;
        }
        else
        {
#if !UNITY_EDITOR
            string text = "Please select one point on the frame";
            textToSpeech.StartSpeaking(text);
            textToSpeech.StopSpeaking(); 
#endif
            return false;
        }
    }
}
