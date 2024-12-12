using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormat
{
    public static string FormatUIOutput(float value, TMPro.TMP_Text uiOutput)
    {
        string formattedOutput = $"{value:F2}";
        //TempValue = formattedOutput;
        uiOutput.text = formattedOutput;
        return formattedOutput;
    }
}
