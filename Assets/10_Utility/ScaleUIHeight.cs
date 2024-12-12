using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleUIHeight : MonoBehaviour
{
    public bool CalculateScales = false;

    private float oldYpos;
    private float oldHeight;

    private RectTransform transform;
    private VerticalLayoutGroup verticalLayoutGroup;

    private float spacing;
    public ScaleUIHeight(RectTransform transform, VerticalLayoutGroup verticalLayoutGroup)
    {
        this.transform = transform;
        this.verticalLayoutGroup = verticalLayoutGroup;
        GetConsoleUIInfo();
    }

    private void GetConsoleUIInfo()
    {
        oldYpos = transform.localPosition.y;
        oldHeight = transform.sizeDelta.y;
        spacing = verticalLayoutGroup.spacing;
    }

    public void CalculateNewYPos(float newHeight)
    {
        float newYpos = oldYpos - (newHeight / 2) + (oldHeight / 2);

        transform.localPosition = new Vector3(transform.localPosition.x, newYpos);

        transform.sizeDelta = new Vector2(transform.sizeDelta.x, newHeight);

        CalculateScales = false;
    }

    public bool CheckNewHeight(float newHeight)
    {
        if (newHeight < oldHeight)
        {
            return false;
        }
        else
        {
            return true;
        }

    }



}
