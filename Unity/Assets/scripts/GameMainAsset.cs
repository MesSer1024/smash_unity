using UnityEngine;
using System.Collections;

public class GameMainAsset : MonoBehaviour
{
    public GameObject PlayerAsset;
    public GameObject WallContainerAsset;
    public Light ArenaLight;
    public LevelController Level;

    private GameMain _game;
    private bool _waitForInput;

    // Use this for initialization
    void Start()
    {
        _game = GameMain.Instance;
        _game.init();
        GameState.PlayerAsset = PlayerAsset;
        GameState.LevelAsset = Level;
        GameState.GameAsset = this;
        ArenaLight.intensity = 0.1f;
        _waitForInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        _game.PreUpdate(); //update keyboard and stuffs


        if (_waitForInput && GameState.PlayerInput[GameMain.InputConcept.Accept])
        {
            _waitForInput = false;
            MessageManager.QueueMessage(new StartArenaMessage(1));
        }
    }

    /// <summary>
    /// 0..1 is probably expected
    /// </summary>
    /// <param name="value"></param>
    public void _SetLightIntensity(float value)
    {
        ArenaLight.intensity = value;
    }
}
