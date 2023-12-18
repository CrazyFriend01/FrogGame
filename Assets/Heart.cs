using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite HeartFull, HeartHalf, HeartEmpty;
    Image HeartImage;

    public void Awake()
    {
        HeartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status) 
        {
            case HeartStatus.Empty:
                HeartImage.sprite = HeartEmpty;
                break;
            case HeartStatus.Half:
                HeartImage.sprite = HeartHalf;
                break;
            case HeartStatus.Full:
                HeartImage.sprite = HeartFull;
                break;
        }
    }

}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}