using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_FillBar : MonoBehaviour
{
    [System.Serializable]
    public struct FillBarData
    {
        public Vector2 ScreenPos;
        public Color32 Color;
        public float Percent;
        public float Width;
        public float Height;
    }

    public RectTransform Fill;
    public Image FillImage;

    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = transform as RectTransform;
    }

    public void SetData(FillBarData data)
    {
        _rectTransform.sizeDelta = new Vector2(data.Width, data.Height);
        _rectTransform.anchoredPosition = data.ScreenPos;
        Fill.localScale = new Vector3(data.Percent, 1, 1);
        FillImage.color = data.Color;
    }
}
