using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] private RawImage _randomImage;
    string url = "https://picsum.photos/200";

    private void Start()
    {
        StartCoroutine(LoadImage(url));
    }

    IEnumerator LoadImage(string ImageURL)
    {
        using (WWW www = new WWW(ImageURL))
        {
            yield return www;
            Texture2D texture = www.texture;
            _randomImage.texture = texture;
        }
    }
}
