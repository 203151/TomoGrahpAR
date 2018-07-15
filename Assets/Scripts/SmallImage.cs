using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class SmallImage : MonoBehaviour, IInputClickHandler
{
    private GameObject bigFrameGameObject;
    private BigImage bigImage;
    private Image bigFrameImage;
    private Image myImage;
    public Image MyImage
    {
        get
        {
            return myImage;
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("SmallImage - OnInputClicked");
        if(!bigFrameImage.enabled)
        {
            bigFrameImage.enabled = true;
        }
        bigImage.FirstPoint = new Vector2Int(0, 0);
        bigImage.SecondPoint = new Vector2Int(0, 0);

        bigFrameImage.sprite = Sprite.Create(CopyTexture2D(myImage.sprite.texture), new Rect(0, 0, 32, 32), new Vector2());
    }

    // Use this for initialization
    void Start ()
    {
        bigFrameGameObject = GameObject.FindGameObjectWithTag("BigFrame");
        bigFrameImage = bigFrameGameObject.GetComponent<Image>();
        bigImage = bigFrameGameObject.GetComponent<BigImage>();
        bigFrameImage.enabled = false;
        myImage = GetComponent<Image>();
        
    }

    public Texture2D CopyTexture2D(Texture2D copiedTexture)
    {
        //Create a new Texture2D, which will be the copy.
        Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
        //Choose your filtermode and wrapmode here.
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(copiedTexture.GetPixels());

        //This finalizes it. If you want to edit it still, do it before you finish with .Apply(). Do NOT expect to edit the image after you have applied. It did NOT work for me to edit it after this function.
        texture.Apply();

        //Return the variable, so you have it to assign to a permanent variable and so you can use it.
        return texture;
    }

}
