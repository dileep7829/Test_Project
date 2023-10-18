using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Views.Game;

namespace Controllers.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private RectTransform gamePanel;
        [SerializeField] private PuzzleButton puzzleButton;
        [SerializeField] private  SpriteHolder spriteHolder;
        [SerializeField] private  GameObject gameOverPopup;
        
        private List<Sprite> _puzzleSprites = new List<Sprite>();
        private PuzzleButton _button1;
        private int _totalButtonsCount;
        private bool _isGameFinished;
        
        void Start()
        {
            _isGameFinished = false;
            _totalButtonsCount = GlobalData.rowCount * GlobalData.columnCount;
            GlobalData.targetCount = _totalButtonsCount / 2;
            
            Randomizer.RandomizeArray(ref spriteHolder.Sprites);
            CreatePuzzleButtons();
        }

        private void CreatePuzzleButtons()
        {
            CreatePuzzleSprites();
            for (int i = 0; i < _totalButtonsCount; i++)
            {
                PuzzleButton btn = Instantiate(puzzleButton, gamePanel, false);
                btn.SetButtonData(i, _puzzleSprites[i]);
            }
        }

        private void CreatePuzzleSprites()
        {
            for (int i = 0; i < _totalButtonsCount; i++)
            {
                _puzzleSprites.Add(spriteHolder.Sprites[i % (_totalButtonsCount/2)]);
            }
            Randomizer.RandomizeList(ref _puzzleSprites);
        }

        private void OnButtonClickedEvent(object sender, EventArgs e)
        {
            PuzzleButton currentButton = (PuzzleButton)sender;
            if (_button1 == null)
            {
                _button1 = currentButton;
            }
            else
            {
                if (_button1.ItemImg == currentButton.ItemImg)
                {
                    
                    EventsManager.Instance.OnItemMatched.Invoke(this,null);
                    //Debug.Log("It's a Match");
                    StartCoroutine(currentButton.RemoveItem());
                    StartCoroutine(_button1.RemoveItem());
                }
                else
                {
                    //Debug.Log("It's not a Match");
                    EventsManager.Instance.OnItemUnMatched.Invoke(this,null);
                    StartCoroutine(currentButton.HideItem());
                    StartCoroutine(_button1.HideItem());
                }
                _button1 = null;
            }
        }
        
        private void OnItemHideStartEvent(object sender, EventArgs e)
        {
            SoundPlayer.Instance.PlaySFX(SoundNames.INCORRECT);
        }
        
        private void OnItemRemoveStartEvent(object sender, EventArgs e)
        {
            SoundPlayer.Instance.PlaySFX(SoundNames.CORRECT);
        }
        
        private void OnGameFinishedEvent(object sender, EventArgs e)
        {
            _isGameFinished = true;
        }

        private void OnItemRemovedEvent(object sender, EventArgs e)
        {
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (_isGameFinished)
            {
                gameOverPopup.SetActive(true);
                SoundPlayer.Instance.PlaySFX(SoundNames.GAME_END);
            }
        }

        private void OnEnable()
        {
            EventsManager.Instance.OnButtonClicked += OnButtonClickedEvent;
            EventsManager.Instance.OnItemHideStart += OnItemHideStartEvent;
            EventsManager.Instance.OnItemRemoveStart += OnItemRemoveStartEvent;
            EventsManager.Instance.OnItemRemoved += OnItemRemovedEvent;
            EventsManager.Instance.OnGameFinished += OnGameFinishedEvent;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnButtonClicked -= OnButtonClickedEvent;
            EventsManager.Instance.OnItemHideStart -= OnItemHideStartEvent;
            EventsManager.Instance.OnItemRemoveStart -= OnItemRemoveStartEvent;
            EventsManager.Instance.OnItemRemoved -= OnItemRemovedEvent;
            EventsManager.Instance.OnGameFinished -= OnGameFinishedEvent;
        }
    }
}
