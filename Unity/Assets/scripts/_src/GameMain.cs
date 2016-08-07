using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMain
{
    public static GameMain Instance { get; private set; }
    static GameMain()
    {
        Instance = new GameMain();
    }

    private GameMain()
    {
        _inputState = new KeyboardAndMouseController();
        _inputState.reset();
        GameState.PlayerInput = _inputState.getButtonStates();
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
        }

        public void appendInput()
        {
            _inputMapping[InputConcept.MoveUp] |= Input.GetKey(KeyCode.W);
            _inputMapping[InputConcept.MoveDown] |= Input.GetKey(KeyCode.S);
            _inputMapping[InputConcept.MoveLeft] |= Input.GetKey(KeyCode.A);
            _inputMapping[InputConcept.MoveRight] |= Input.GetKey(KeyCode.D);

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

    public void PostFixedUpdate()
    {
        _inputState.reset();
    }

    public void PreUpdate()
    {
        _inputState.appendInput();
        MessageManager.ProcessMessageQueue();
    }
}
