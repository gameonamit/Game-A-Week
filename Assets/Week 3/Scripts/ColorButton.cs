using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public Color color;

    public void OnColorButtonClick()
    {
        SelectionController selection = FindObjectOfType<SelectionController>();
        selection.UpdateCurrentColor(color);
    }
}
