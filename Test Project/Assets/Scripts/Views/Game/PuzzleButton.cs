using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Views.Game
{
    public class PuzzleButton : MonoBehaviour
    {
        [SerializeField] private Sprite bgImg;
        private Sprite itemImg;

        public Sprite ItemImg
        {
            get => itemImg;
        }

        private Button button;

        public void OnButtonClicked()
        {
            if(button.image.sprite == itemImg)
                return;
            ShowItem();
            EventsManager.Instance.OnButtonClicked.Invoke(this, "");
        }

        public void SetButtonData(int index, Sprite sprite)
        {
            name = index + "";
            itemImg = sprite;
            button = GetComponent<Button>();
            button.image.sprite = bgImg;
        }

        private void ShowItem()
        {
            button.image.sprite = itemImg;
        }

        public IEnumerator HideItem()
        {
            yield return new WaitForSeconds(1);
            button.image.sprite = bgImg;
            
        }

        public IEnumerator RemoveItem()
        {
            yield return new WaitForSeconds(0.5f);
            button.image.color = new Color(1, 1, 1, 0);
            button.interactable = false;
        }
        
    }
}
