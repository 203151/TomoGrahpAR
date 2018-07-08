using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class BigImage : MonoBehaviour, IInputClickHandler
{

    private Image myImage;
    private SpriteRenderer spriteRenderer;

    private Vector2Int firstPoint = new Vector2Int(0,0), 
                       secondPoint = new Vector2Int(0,0);
    public Vector2Int FirstPoint
    {
        get
        {
            return firstPoint;
        }
        set
        {
            firstPoint = value;
        }
    }
    public Vector2Int SecondPoint
    {
        get
        {
            return secondPoint;
        }
        set
        {
            secondPoint = value;
        }
    }

    private Color firstPointColor = new Color(0f, 0f, 0f),
                  secondPointColor = new Color(0f, 0f, 0f);

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("BigImage OnInputClicked");

        RaycastHit hit = GazeManager.Instance.HitInfo;

        Texture2D texture = myImage.sprite.texture;

        Vector2 pixelUV = hit.textureCoord;
        Vector2 shifted;

        pixelUV.x *= texture.width;
        pixelUV.y *= texture.height;

        shifted.x = 32 - pixelUV.x;
        shifted.y = 32 - pixelUV.y;

        ChangeClickedPixel(pixelUV, shifted, texture);

        Debug.Log("first.x = " + firstPoint.x + " first.y = " + firstPoint.y + "\nfirst.color = " + firstPointColor);
        Debug.Log("second.x = " + secondPoint.x + " second.y = " + secondPoint.y + "\nsecond.color = " + secondPointColor);

        //Debug.Log("pixel.x = " + (int)pixelUV.x + "pixel.y = " + (int)pixelUV.y + "\ncolor = " + col);

        //texture.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
        //texture.SetPixel((int)shifted.x, (int)shifted.y, Color.white);
        texture.Apply();

    }

    // Use this for initialization
    void Start ()
    {
        //spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        myImage = GetComponent<Image>();
    }
	
    private void ChangeClickedPixel(Vector2 pixelUV, Vector2 shifted, Texture2D texture)
    {
        if ( firstPoint.magnitude == 0 && secondPoint.magnitude == 0 )
        {
            firstPoint.x = (int)shifted.x;
            firstPoint.y = (int)shifted.y;
            firstPointColor = texture.GetPixel(firstPoint.x, firstPoint.y);

            //texture.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
            texture.SetPixel((int)shifted.x, (int)shifted.y, new Color(0, 0, 0, 1));
        }
        else if ( firstPoint.magnitude == 0 && secondPoint.magnitude != 0 &&
            ( secondPoint.x != (int)shifted.x || secondPoint.y != (int)shifted.y ) )
        {
            //Debug.Log("pierwszy else if");
            firstPoint.x = (int)shifted.x;
            firstPoint.y = (int)shifted.y;
            firstPointColor = texture.GetPixel(firstPoint.x, firstPoint.y);

            //texture.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
            texture.SetPixel((int)shifted.x, (int)shifted.y, new Color(0, 0, 0, 1));
        }
        else if ( firstPoint.magnitude != 0 && secondPoint.magnitude == 0 &&
            ( firstPoint.x != (int)shifted.x || firstPoint.y != (int)shifted.y ) )
        {
            //Debug.Log("drugi else if");
            secondPoint.x = (int)shifted.x;
            secondPoint.y = (int)shifted.y;
            secondPointColor = texture.GetPixel(secondPoint.x, secondPoint.y);

            //texture.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
            texture.SetPixel((int)shifted.x, (int)shifted.y, new Color(0, 0, 0, 1));
        }
        else
        {
            if ( firstPoint.x == (int)shifted.x && firstPoint.y == (int)shifted.y )
            {
                firstPoint.x = 0;
                firstPoint.y = 0;
                texture.SetPixel((int)shifted.x, (int)shifted.y, firstPointColor);
                firstPointColor = new Color(0,0,0,1);
            }
            else if ( secondPoint.x == (int)shifted.x && secondPoint.y == (int)shifted.y )
            {
                secondPoint.x = 0;
                secondPoint.y = 0;
                texture.SetPixel((int)shifted.x, (int)shifted.y, secondPointColor);
                secondPointColor = new Color(0, 0, 0, 1);
            }
        }
    }
}
