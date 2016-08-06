using UnityEngine;
using System.Collections;

public class PreUpdateLoop : MonoBehaviour
{
    public GameObject PlayerAsset;
    public GameObject WallContainerAsset;

    private GameMain _game;

    // Use this for initialization
    void Start()
    {
        _game = GameMain.Instance;
        GameState.PlayerAsset = PlayerAsset;
        GameState.Enemies = new System.Collections.Generic.List<IEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        _game.PreUpdate();
    }
}
