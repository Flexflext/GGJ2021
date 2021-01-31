using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StatValueVisualizer
{
    private static Dictionary<ItemStat, string> statTranslations = new Dictionary<ItemStat, string>
    {
        {ItemStat.Health, "Health"},
        {ItemStat.HealthRegen, "HealthReg"},
        {ItemStat.Damage, "Damage"},
        {ItemStat.MovementSpeed, "Speed"},
        {ItemStat.AttackSpeed, "AttackSpeed"},
        {ItemStat.AttackRange, "Range"},
    };

    private static string LowGreenColorHex = "66ff72";
    private static string HighGreenColorHex = "00ff55";
    private static string LowRedColorHex = "ffa31d";
    private static string HighRedColorHex = "e3e3e3";

    private static Color LowGreenColor = GetColorFromHex(LowGreenColorHex);
    private static Color HighGreenColor = GetColorFromHex(HighGreenColorHex);
    private static Color LowRedColor = GetColorFromHex(LowRedColorHex);
    private static Color HighRedColor = GetColorFromHex(HighRedColorHex);


    // BOLD <b></b>
    // BOLD <i></i>
    // BOLD <u></u>
    // BOLD <sub></sub>
    // Linefeed \n
    // Tab \t
    // Size <size=??>  <size=+??> <size=-??> </size>
    // Color <color=red>  <#FF8000> </color>

    public static string ToString(int[] statValues)
    {
        var sb = new StringBuilder();
        foreach (ItemStat statEnum in Item.StatEnums)
        {
            int statValue = statValues[(int)statEnum];
            if (statValue != 0)
            {
                sb.Append($"<b><#e3e3e3>{statTranslations[statEnum]}</color></b>: <i><{GetStatColor(statValue)}>{statValue}\n</color></i>");
            }
        }

        return sb.ToString();
    }

    private static string GetStatColor(int _svalue)
    {
        Color lerp;
        if (Mathf.Sign(_svalue) > 0)
        {
            lerp = Color.Lerp(LowGreenColor, HighGreenColor, Mathf.Lerp(1,5, Mathf.Abs(_svalue)));
            return "#" + GetHexFromColor(lerp);
        }
        else
        {
            lerp = Color.Lerp(LowRedColor, HighRedColor, Mathf.Lerp(1, 5, Mathf.Abs(_svalue)));
            return "#" + GetHexFromColor(lerp);
        }
    }

    private static Color GetColorFromHex(string _hex)
    {
        float red = HexToFloatNormalized(_hex.Substring(0, 2));
        float green = HexToFloatNormalized(_hex.Substring(2, 2));
        float blue = HexToFloatNormalized(_hex.Substring(4, 2));
        float alpha = 1f;

        if (_hex.Length >= 8)
        {
            alpha = HexToFloatNormalized(_hex.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }

    private static string GetHexFromColor(Color color, bool _usealpha = false)
    {
        string red = FloatNormalizedToHex(color.r);
        string green = FloatNormalizedToHex(color.g);
        string blue = FloatNormalizedToHex(color.b);

        if (!_usealpha)
        {
            return red + green + blue;
        }
        else
        {
            string alpha = FloatNormalizedToHex(color.a);
            return red + green + blue + alpha;
        }
    }

    private static float HexToFloatNormalized(string _hex)
    {
        return HexToDec(_hex) / 255f;
    }

    private static string FloatNormalizedToHex(float _value)
    {
        return DecToHex(Mathf.RoundToInt(_value * 255f));
    }

    private static int HexToDec(string _hex)
    {
        int dec = System.Convert.ToInt32(_hex, 16);
        return dec;
    }

    private static string DecToHex(int _value)
    {
        return _value.ToString("X2");
    }
}