using LD40;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flasher : MonoBehaviour
{
    public Color redWash;
    public RawImage image;
    public RawImage whitewash;

    public void Flash()
    {
        StartCoroutine(IE_Flash());
    }

    public void FlashWin()
    {
        StartCoroutine(IE_FlashWin());
    }

    public IEnumerator IE_Flash()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        image.canvasRenderer.SetColor(redWash);
        image.CrossFadeAlpha(1, 0, true);
        whitewash.color = Color.white;
        whitewash.canvasRenderer.SetColor(Color.white);
        yield return new WaitForSecondsRealtime(0.5f);
        whitewash.CrossFadeAlpha(0, 2f, true);
        image.CrossFadeAlpha(0.3f, 5f, true);
        yield return new WaitForSecondsRealtime(2f);
        image.CrossFadeColor(Color.black, 2f, true, true);
        yield return new WaitForSecondsRealtime(3f);
        DestroyProjectiles();
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(0.1f);
        image.CrossFadeColor(Color.clear, 0.15f, true, true);
        whitewash.CrossFadeColor(Color.clear, 0.15f, true, true);
        Reset();
        yield return new WaitForSecondsRealtime(0.15f);
        image.canvasRenderer.SetColor(redWash);
        image.color = new Color(redWash.r, redWash.g, redWash.b, 0);
        whitewash.canvasRenderer.SetColor(Color.white);
        whitewash.color = new Color(1, 1, 1, 0);
    }

    public IEnumerator IE_FlashWin()
    {
        whitewash.color = Color.white;
        whitewash.canvasRenderer.SetColor(Color.white);
        whitewash.canvasRenderer.SetAlpha(0.85f);
        yield return new WaitForSecondsRealtime(0.1f);
        whitewash.CrossFadeAlpha(0, 2f, true);
    }

    public void Reset()
    {
        GameObject.FindGameObjectWithTag("Tank").GetComponent<Controller>().Reset();
    }
    public void DestroyProjectiles()
    {
        GameObject.FindGameObjectWithTag("Tank").GetComponent<Controller>().DestroyProjectiles();
    }
}
