using System;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
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
        [SerializeField] private  TMP_Text txtMatchCount;
        [SerializeField] private  TMP_Text txtTurnCount;
        private int matchCount;
        private int turnCount;
        private int totalButtonsCount;
        
        private List<Sprite> puzzleSprites = new List<Sprite>();
        private PuzzleButton button1;
        
        void Start()
        {
            matchCount = 0;
            turnCount = 0;
            totalButtonsCount = GlobalData.rowCount * GlobalData.columnCount;
            
            Randomizer.RandomizeArray(ref spriteHolder.Sprites);
            CreatePuzzleButtons();
        }

        private void CreatePuzzleButtons()
        {
            CreatePuzzleSprites();
            for (int i = 0; i < totalButtonsCount; i++)
            {
                PuzzleButton btn = Instantiate(puzzleButton, gamePanel, false);
                btn.SetButtonData(i, puzzleSprites[i]);
            }
        }

        private void CreatePuzzleSprites()
        {
            for (int i = 0; i < totalButtonsCount; i++)
            {
                puzzleSprites.Add(spriteHolder.Sprites[i % (totalButtonsCount/2)]);
            }
            Randomizer.RandomizeList(ref puzzleSprites);
        }

        private void OnButtonClickedEvent(object sender, EventArgs e)
        {
            PuzzleButton currentButton = (PuzzleButton)sender;
            if (button1 == null)
            {
                button1 = currentButton;
            }
            else
            {
                txtTurnCount.text = ++turnCount + "";
                if (button1.ItemImg == currentButton.ItemImg)
                {
                    txtMatchCount.text = ++matchCount + "";
                    //Debug.Log("It's a Match");
                    StartCoroutine(currentButton.RemoveItem());
                    StartCoroutine(button1.RemoveItem());
                    
                    SoundPlayer.Instance.PlaySFX(SoundNames.CORRECT);
                }
                else
                {
                    //Debug.Log("It's not a Match");
                    StartCoroutine(currentButton.HideItem());
                    StartCoroutine(button1.HideItem());
                    SoundPlayer.Instance.PlaySFX(SoundNames.INCORRECT);
                }
                button1 = null;
            }
        }

        private void OnItemRemovedEvent(object sender, EventArgs e)
        {
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (matchCount >= totalButtonsCount / 2)
            {
                SoundPlayer.Instance.PlaySFX(SoundNames.GAME_END);
                Debug.Log("You Won");
            }
        }

        private void OnEnable()
        {
            EventsManager.Instance.OnButtonClicked += OnButtonClickedEvent;
            EventsManager.Instance.OnItemRemoved += OnItemRemovedEvent;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnButtonClicked -= OnButtonClickedEvent;
            EventsManager.Instance.OnItemRemoved -= OnItemRemovedEvent;
        }
    }
}
