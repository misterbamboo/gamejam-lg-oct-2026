using UnityEngine;

namespace Assets.Shared
{
    // Makes a released object fall until its bottom rests on the table.
    public static class TableFall
    {
        public const float TableY = 0f;
        public const float Gravity = 20f;

        public static void Step(Transform transform, Renderer renderer, ref float fallSpeed)
        {
            var bottom = renderer.bounds.min.y;
            if (bottom <= TableY)
            {
                // Also lifts it back up if it was dropped below the table.
                transform.position += Vector3.up * (TableY - bottom);
                fallSpeed = 0f;
                return;
            }

            fallSpeed += Gravity * Time.deltaTime;
            transform.position += Vector3.down * Mathf.Min(fallSpeed * Time.deltaTime, bottom - TableY);
        }
    }
}
