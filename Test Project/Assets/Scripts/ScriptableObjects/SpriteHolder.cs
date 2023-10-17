using UnityEngine;

namespace ScriptableObjects
{
   
   [CreateAssetMenu(fileName = "SpriteHolder", menuName = "SpriteHolder")]
   public class SpriteHolder : ScriptableObject
   {
      public Sprite[] Sprites;
   }
}
