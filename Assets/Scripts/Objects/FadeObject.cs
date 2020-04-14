using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    private bool fade;
    private Material material;
    private Color ogColor;
    Color color;
    public float fadePerSecond = 0.1f;

    private void OnEnable()
    {
        FadeAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        //if (fade)
        //{
        //    Debug.Log(material.color.ToString());
        //    material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSecond * Time.deltaTime));
        //    if (material.color.a <= 0)
        //    {
        //        GetComponent<MeshRenderer>().enabled = false;
        //        material.color = ogColor;
        //        gameObject.SetActive(false);
        //    }
        //}
    }

    public void FadeAlpha()
    {
        material = GetComponent<MeshRenderer>().material;
        ogColor = material.color;
        color = ogColor;
        //fade = true;
        StartCoroutine(FadeTo(1.0f, 0.5f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            material.color = newColor;
            yield return null;
        }
    }
}
