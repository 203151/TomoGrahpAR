using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableObjectClass : ScriptableObject
{
    /// <summary>
    /// Number of point in one frame. DO NOT CHANGE!!!
    /// </summary>
    public int oneFrameValuesSize = 1024;

    /// <summary>
    /// Number of frames in the Aim file
    /// </summary>
    public int numberOfFrames = 5000;

    public string aimFileName = "AimFile.aim";

}
