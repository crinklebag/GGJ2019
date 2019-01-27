using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFades : MonoBehaviour
{
    [SerializeField] float fadeInSpeed;
    [SerializeField] float fadeOutSpeeds;

    // References
    Image blackScreen;


    void Start()
    {
        blackScreen = this.GetComponentInChildren<Image>();
        blackScreen.color = Color.clear;
    }

    public IEnumerator FadeIn ()
    {
        blackScreen.color = new Color(0, 0, 0, 0);
        
        float alpha = blackScreen.color.a;

        while(alpha < 1)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeOut ()
    {
        blackScreen.color = Color.black;

        float alpha = blackScreen.color.a;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeInSpeed;
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
