using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class KongrooHeaderAttribute : PropertyAttribute
{
    public string header;
    public string color;

    public KongrooHeaderAttribute()
    {
        this.header = null;
        this.color = "#A8D9FF";
    }

    public KongrooHeaderAttribute(Color color)
    {
        this.header = null;
        this.color = ColorUtility.ToHtmlStringRGB(color);
    }

    public KongrooHeaderAttribute(string header)
    {
        this.header = header;
        this.color = "#A8D9FF";
    }

    public KongrooHeaderAttribute(string header, string color)
    {
        this.header = header;
        this.color = color;
    }

    public KongrooHeaderAttribute(string header, Color color)
    {
        this.header = header;
        this.color = ColorUtility.ToHtmlStringRGB(color);
    }
}
