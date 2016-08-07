using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameMain : IMessageListener
{
    public static GameMain Instance { get; private set; }
    static GameMain()
    {
        Instance = new GameMain();
    }

    private GameMain()
    {
        MessageManager.AddListener(typeof(ArenaFinishedMessage), this);
        MessageManager.AddListener(typeof(StartArenaMessage), this);
    }

    internal void init()
    {
        _inputState = new KeyboardAndMouseController();
        _inputState.reset();
        GameState.PlayerInput = _inputState.getButtonStates();
        GameState.Enemies = new System.Collections.Generic.List<IEnemy>();
        Time.timeScale = 0.0f;
        _arenaFinished = false;
    }

    #region input
    public enum InputConcept
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Run,
        Fire,
        FireSecondary,
        Ability1,
        Ability2,
        Ability3,
        Accept,
        Decline,
    }

    public class KeyboardAndMouseController
    {
        private Dictionary<InputConcept, bool> _inputMapping;

        public KeyboardAndMouseController()
        {
            _inputMapping = new Dictionary<InputConcept, bool>();
            _inputMapping.Add(InputConcept.MoveUp, false);
            _inputMapping.Add(InputConcept.MoveDown, false);
            _inputMapping.Add(InputConcept.MoveLeft, false);
            _inputMapping.Add(InputConcept.MoveRight, false);

            _inputMapping.Add(InputConcept.Run, false);
            _inputMapping.Add(InputConcept.Fire, false);
            _inputMapping.Add(InputConcept.FireSecondary, false);

            _inputMapping.Add(InputConcept.Ability1, false);
            _inputMapping.Add(InputConcept.Ability2, false);
            _inputMapping.Add(InputConcept.Ability3, false);

            _inputMapping.Add(InputConcept.Accept, false);
            _inputMapping.Add(InputConcept.Decline, false);
        }

        public void reset()
        {
            _inputMapping[InputConcept.MoveUp] = false;
            _inputMapping[InputConcept.MoveDown] = false;
            _inputMapping[InputConcept.MoveLeft] = false;
            _inputMapping[InputConcept.MoveRight] = false;

            _inputMapping[InputConcept.Run] = false;
            _inputMapping[InputConcept.Fire] = false;
            _inputMapping[InputConcept.FireSecondary] = false;

            _inputMapping[InputConcept.Ability1] = false;
            _inputMapping[InputConcept.Ability2] = false;
            _inputMapping[InputConcept.Ability3] = false;

            _inputMapping[InputConcept.Accept] = false;
            _inputMapping[InputConcept.Decline] = false;
        }

        public void appendInput()
        {
            _inputMapping[InputConcept.MoveUp] |= Input.GetKey(KeyCode.W);
            _inputMapping[InputConcept.MoveDown] |= Input.GetKey(KeyCode.S);
            _inputMapping[InputConcept.MoveLeft] |= Input.GetKey(KeyCode.A);
            _inputMapping[InputConcept.MoveRight] |= Input.GetKey(KeyCode.D);

            _inputMapping[InputConcept.Accept] |= Input.GetKey(KeyCode.Space);
            _inputMapping[InputConcept.Decline] |= Input.GetKey(KeyCode.Backspace);

            _inputMapping[InputConcept.Run] |= (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
            _inputMapping[InputConcept.Fire] |= Input.GetMouseButton(0);
            _inputMapping[InputConcept.Fire] |= Input.GetMouseButton(2);
        }

        public Dictionary<InputConcept, bool> getButtonStates()
        {
            return _inputMapping;
        }

        public bool getConceptState(InputConcept concept)
        {
            return _inputMapping[concept];
        }
    }
    #endregion

    KeyboardAndMouseController _inputState;
    private bool _arenaFinished;

    public void PostFixedUpdate()
    {
        _inputState.reset();
        if(_arenaFinished)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }

    public void PreUpdate()
    {
        _inputState.appendInput();
        MessageManager.ProcessMessageQueue();
    }

    public void HandleMessage(IMessage message)
    {
        if(message is ArenaFinishedMessage)
        {
            _arenaFinished = true;
        }
        else if (message is StartArenaMessage)
        {
            var msg = message as StartArenaMessage;
            GameState.LevelAsset.StartArena();
            GameState.GameAsset._SetLightIntensity(1.0f);
            Time.timeScale = 1.0f;
            _arenaFinished = false;
            _inputState.reset();
        }
    }
}
