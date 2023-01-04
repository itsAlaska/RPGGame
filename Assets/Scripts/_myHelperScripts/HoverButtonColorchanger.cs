using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButtonColorchanger : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Color startingColor;
    [SerializeField] private Color hoverColor;

    public void HoverOverButtonColor()
    {
        background.color = hoverColor;
    }

    public void OriginalColor()
    {
        background.color = startingColor;
    }
}
