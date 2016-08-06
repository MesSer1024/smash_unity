using UnityEngine;
using System.Collections.Generic;

public class UI_MainUI : MonoBehaviour
{
    [Header("Prefabs")]
    public UI_FillBar FillBarPrefab;

    private List<UI_FillBar> _fillBars = new List<UI_FillBar>();

    [System.Serializable]
    public struct MainUIData
    {
        public List<UI_FillBar.FillBarData> FillBars;
    }

    public void SetData(MainUIData data)
    {
        // health bars
        while (_fillBars.Count < data.FillBars.Count)
        {
            var newFillBar = Instantiate(FillBarPrefab, transform, false) as UI_FillBar;
            _fillBars.Add(newFillBar);
        }

        for (int i = 0; i < data.FillBars.Count; i++)
        {
            var fillBar = _fillBars[i];
            if (fillBar.gameObject.activeSelf == false)
                fillBar.gameObject.SetActive(true);
            fillBar.SetData(data.FillBars[i]);
        }

        for (int i = data.FillBars.Count; i < _fillBars.Count; i++)
        {
            if (_fillBars[i].gameObject.activeSelf == true)
                _fillBars[i].gameObject.SetActive(false);
        }
    }
}
