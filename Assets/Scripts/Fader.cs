using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    IEnumerator Start()
    {
        Image fadeImage = GetComponent<Image>();
        Color color = fadeImage.color;
        while (color.a < 1f)
        {
            color.a += 1f * Time.deltaTime;
            fadeImage.color = color;
            yield return null;
        }
    }

}
