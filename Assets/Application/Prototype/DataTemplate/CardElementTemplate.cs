using System;
using UnityEngine;
using UnityEngine.EventSystems;


[Serializable]
public class CardElementTemplate
{
    public string elementName;

    // Raw floats instead of Vector2
    public float anchoredPosX;
    public float anchoredPosY;

    public float sizeX;
    public float sizeY;

    public float rotationZ;
    public bool isVisible;

    // Constructor for easy conversion
    public CardElementTemplate(string name, Vector2 anchoredPosition, Vector2 sizeDelta, float rotationZ, bool isVisible)
    {
        this.elementName = name;
        this.anchoredPosX = anchoredPosition.x;
        this.anchoredPosY = anchoredPosition.y;
        this.sizeX = sizeDelta.x;
        this.sizeY = sizeDelta.y;
        this.rotationZ = rotationZ;
        this.isVisible = isVisible;
    }

    // Helper to restore original types
    public Vector2 GetAnchoredPosition() => new Vector2(anchoredPosX, anchoredPosY);
    public Vector2 GetSizeDelta() => new Vector2(sizeX, sizeY);
}

