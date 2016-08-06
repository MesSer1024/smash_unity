using UnityEngine;
using System.Collections;

public class MainLoop : MonoBehaviour
{
    public GameObject PlayerAsset;
    public GameObject WallContainerAsset;

    private GameMain _game;

    // Use this for initialization
    void Start()
    {
        _game = new GameMain();
    }

    // Update is called once per frame
    void Update()
    {
        _game.PreUpdate();
        _game.Update();
        _game.PostUpdate();
    }
}
