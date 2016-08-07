using UnityEngine;
using System.Collections.Generic;

public class GameUIBinding : MonoBehaviour
{
    public UI_MainUI MainUI;

    List<UI_FillBar.FillBarData> _fillBars = new List<UI_FillBar.FillBarData>(20);

    void LateUpdate()
    {
        UI_MainUI.MainUIData data;

        _fillBars.Clear();

        foreach (var lifeComponent in Life.LifeComponents)
        {
            if (lifeComponent.ShowHealthBar == false)
                continue;

            UI_FillBar.FillBarData fillbarData;
            fillbarData.Color = lifeComponent.LifeData.BarColor;
            fillbarData.Width = lifeComponent.LifeData.Width;
            fillbarData.Height = lifeComponent.LifeData.Height;
            fillbarData.Percent = lifeComponent.Health / lifeComponent.StartHealth;
            fillbarData.ScreenPos = Camera.main.WorldToScreenPoint((Vector3)(lifeComponent.transform.position + Vector3.up * 3));
            _fillBars.Add(fillbarData);
        }

        data.FillBars = _fillBars;
        MainUI.SetData(data);
    }
}
