using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class Graph : MonoBehaviour, IInputClickHandler
{

    private SliderManager sliderManager;
    private ReadAIMFile readAIM;
    private Image myImage;
    private float xScale, yScale;
    private int shiftedFrameNumber;

    private int totalNumberOfFrames;



    // Use this for initialization
    void Start()
    {
        sliderManager = GameObject.Find("Slider").GetComponent<SliderManager>();
        readAIM = GameObject.FindGameObjectWithTag("Manager").GetComponent<ReadAIMFile>();
        totalNumberOfFrames = (int)sliderManager.thisSlider.maxValue;
        myImage = GetComponent<Image>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        RaycastHit hit = GazeManager.Instance.HitInfo;

        Texture2D texture = myImage.sprite.texture;

        Vector2 pixelUV = hit.textureCoord;
        Debug.Log("Tomograph - OnInputClicked pixelUV = " + pixelUV);

            shiftedFrameNumber = (int)((1 - pixelUV.x) * (totalNumberOfFrames));
            sliderManager.thisSlider.value = shiftedFrameNumber;
            //sliderManager.ChangeSmallFramesToGivenFrame(shiftedFrameNumber);

    }
}
