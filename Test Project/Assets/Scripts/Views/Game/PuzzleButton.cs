using UnityEngine;
using UnityEngine.UI;

namespace Views.Game
{
    public class PuzzleButton : MonoBehaviour
    {
        [SerializeField] private Sprite bgImg;
        private Sprite itemImg;
        private Button button;

        public void OnButtonClicked()
        {
            EventsManager.Instance.OnButtonClicked.Invoke(this, "");
        }

        public void SetButtonData(int index)
        {
            name = index + "";
            button = GetComponent<Button>();
            button.image.sprite = bgImg;
        }
        
    }
}
