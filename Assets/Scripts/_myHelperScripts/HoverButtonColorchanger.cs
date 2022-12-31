using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverButtonColorchanger : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Color startingColor;

    public void HoverOverButtonColor()
    {
        background.color = new Color(.9f, .88f, .8f);
    }

    public void OriginalColor()
    {
        background.color = startingColor;
    }
}
