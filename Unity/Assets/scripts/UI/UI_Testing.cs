using UnityEngine;
using System.Collections;

public class UI_Testing : MonoBehaviour
{
    public UI_MainUI MainUI;

    public UI_MainUI.MainUIData DummyData;    

    void Update()
    {
        if (DummyData.FillBars.Count > 0)
        {
            var player = Object.FindObjectOfType<PlayerMovement>();
            var screenPos = Camera.main.WorldToScreenPoint(player.transform.position + Vector3.up * 2);

            var healthBar = DummyData.FillBars[0];
            healthBar.ScreenPos = screenPos;
            DummyData.FillBars[0] = healthBar;
        }

        MainUI.SetData(DummyData);
    }
}
