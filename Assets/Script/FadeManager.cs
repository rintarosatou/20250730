using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    public Image fadeImage;
    public float fadeSpeed = 1f;

    private bool isFading = false;

    void Awake()
    {
        // Singleton��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �ŏ��͊��S�ɍ��ɂ��Ă����i�t�F�[�h�C���p�j
        SetAlpha(1f);
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string Test)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutIn(Test));
        }
    }

    IEnumerator FadeOutIn(string Test)
    {
        yield return StartCoroutine(FadeOut());
        yield return SceneManager.LoadSceneAsync(Test);
        yield return StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1f);
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0f);
        isFading = false;
    }

    void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = Mathf.Clamp01(alpha);
            fadeImage.color = c;
        }
    }
}
