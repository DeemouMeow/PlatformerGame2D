using UnityEngine;

namespace Game.Utils
{
    public class VelocityLerper
    {
        private Vector2 _lerpedValue;

        public Vector2 Value => _lerpedValue;

        public Vector2 Lerp(Vector2 current, Vector2 end, float factor)
        {
            _lerpedValue.x = Mathf.Lerp(current.x, end.x, factor * Time.fixedDeltaTime);
            _lerpedValue.y = Mathf.Lerp(current.y, end.y, factor * Time.fixedDeltaTime);

            return _lerpedValue;
        }

        public void Reset()
        {
            _lerpedValue = Vector2.zero;
        }
    }
}
