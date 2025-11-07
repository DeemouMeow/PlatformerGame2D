using UnityEngine;

namespace Game.Collision.Triggers
{
    using Character;
    using Utils;

    public class LevelEnd : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterBase character))
            {
                SceneLoader.LoadNextLevel();
            }
        }
    }
}
