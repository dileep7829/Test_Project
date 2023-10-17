using System.Collections.Generic;
using UnityEngine;
using Views.Game;

namespace Controllers.Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private RectTransform gamePanel;
        [SerializeField] private PuzzleButton puzzleButton;
        private List<PuzzleButton> puzzleButtons = new List<PuzzleButton>();
        // Start is called before the first frame update
        void Start()
        {
            CreatePuzzleButtons();
        }

        private void CreatePuzzleButtons()
        {
            int totalButtonsCount = GlobalData.rowCount * GlobalData.columnCount;
        
            for (int i = 0; i < totalButtonsCount; i++)
            {
                PuzzleButton btn = Instantiate(puzzleButton, gamePanel, false);
                btn.SetButtonData(i);
            }
        }
    }
}
