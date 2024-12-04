using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizablePlatform : MonoBehaviour
{
    [SerializeField] int platformX;

    [SerializeField] SpriteRenderer middleSprite;
    [SerializeField] SpriteRenderer rightSprite;
    [SerializeField] SpriteRenderer leftSprite;
    private void OnValidate()
    {
        if (platformX < 1) platformX = 1;
        platformX = Mathf.RoundToInt(platformX);
    }

    public void UpdateSprite()
    {
        middleSprite.size = new Vector2(platformX, transform.localScale.y);
        leftSprite.transform.localPosition = new Vector3(-1 + (platformX - 1) * (-0.5f), leftSprite.transform.localPosition.y, leftSprite.transform.localPosition.z);
        rightSprite.transform.localPosition = new Vector3(1 + (platformX - 1) * (0.5f), rightSprite.transform.localPosition.y, rightSprite.transform.localPosition.z);
    }
}
