using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemTypes : ScriptableObject
{
    public new string name;
    public float score;
    [Multiline] public string ToastMessage;

    public bool isChilli = false;
    public bool makePlayerBigger = false;
}
