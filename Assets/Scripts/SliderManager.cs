using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider thisSlider;

    //private SpriteRenderer spriteRenderer;
    public GameObject[] smallFramesGameObjects;
    List<Texture2D> allImagesForFrames;
    ReadAIMFile readAIM;
    Image [] smallFramesImages;

    void Start ()
    {
        
        readAIM = GameObject.FindGameObjectWithTag("Manager").GetComponent<ReadAIMFile>();
        allImagesForFrames = readAIM.Frames;
        thisSlider.maxValue = allImagesForFrames.Count - 10; // there are 10 small frames, so we need to subtract 10 starting frames

        smallFramesImages = new Image[10];

        int i = 0;
        Debug.Log("number of smallframes = " + smallFramesGameObjects.Length);


        foreach ( var sFrame in smallFramesGameObjects )
        {
            Debug.Log(smallFramesGameObjects[i].name);
            smallFramesImages[i] = sFrame.GetComponent<Image>();
            smallFramesImages[i].sprite = Sprite.Create(allImagesForFrames[i], new Rect(0, 0, 32, 32), new Vector2());
            i++;
        }

    }

    public void ChangeSmallFrames()
    {
        int i = 0;
        int sliderNumber = (int)thisSlider.value;
        Debug.Log("slider number " + sliderNumber);
        foreach ( var sFrame in smallFramesImages )
        {
            smallFramesImages[i].sprite = Sprite.Create(allImagesForFrames[i + sliderNumber], new Rect(0, 0, 32, 32), new Vector2());
            i++;
        }
    }

    public void ChangeSmallFramesToGivenFrame(int givenFrame)
    {
        int i = 0;
        foreach ( var sFrame in smallFramesImages )
        {
            smallFramesImages[i].sprite = Sprite.Create(allImagesForFrames[i + givenFrame], new Rect(0, 0, 32, 32), new Vector2());
            i++;
        }
    }

    public void NextFrame()
    {
        //int i = 0;
        int sliderNumber = (int)thisSlider.value++;
        //sliderNumber++;
        Debug.Log("slider number " + sliderNumber);

    }

    public void PreviousFrame()
    {
        //int i = 0;
        int sliderNumber = (int)thisSlider.value--;
        //sliderNumber++;
        Debug.Log("slider number " + sliderNumber);

    }

}
