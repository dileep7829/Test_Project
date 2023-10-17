using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Views.Game;

namespace Controllers.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private RectTransform gamePanel;
        [SerializeField] private PuzzleButton puzzleButton;
        [SerializeField] private  SpriteHolder spriteHolder;
        
        private List<Sprite> puzzleSprites = new List<Sprite>();
        private PuzzleButton button1;
        
        void Start()
        {
            Randomizer.RandomizeArray(ref spriteHolder.Sprites);
            CreatePuzzleButtons();
        }

        private void CreatePuzzleButtons()
        {
            int totalButtonsCount = GlobalData.rowCount * GlobalData.columnCount;
            CreatePuzzleSprites(totalButtonsCount);
            for (int i = 0; i < totalButtonsCount; i++)
            {
                PuzzleButton btn = Instantiate(puzzleButton, gamePanel, false);
                btn.SetButtonData(i, puzzleSprites[i]);
            }
        }

        private void CreatePuzzleSprites(int totalButtonsCount)
        {
            for (int i = 0; i < totalButtonsCount; i++)
            {
                puzzleSprites.Add(spriteHolder.Sprites[i % (totalButtonsCount/2)]);
            }
            Randomizer.RandomizeList(ref puzzleSprites);
        }

        private void OnButtonClickedEvent(object sender, string e)
        {
            PuzzleButton currentButton = (PuzzleButton)sender;
            if (button1 == null)
            {
                button1 = currentButton;
            }
            else
            {
                if (button1.ItemImg == currentButton.ItemImg)
                {
                    Debug.Log("It's a Match");
                    StartCoroutine(currentButton.RemoveItem());
                    StartCoroutine(button1.RemoveItem());
                }
                else
                {
                    Debug.Log("It's not a Match");
                    StartCoroutine(currentButton.HideItem());
                    StartCoroutine(button1.HideItem());
                }
                button1 = null;
            }
        }

        private void OnEnable()
        {
            EventsManager.Instance.OnButtonClicked += OnButtonClickedEvent;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnButtonClicked -= OnButtonClickedEvent;
        }
    }
}
