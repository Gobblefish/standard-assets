/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace Gobblefish.Graphics {

    /// <summary>
    /// 
    /// </summary>
    public static class CameraExtensions {

        public static Vector2 GetOrthographicDimensions(this Camera camera) {
            float height = 2f * camera.orthographicSize;
            float width = height * camera.aspect;
            return new Vector2(width, height);
        }

        public static (Vector2, Vector2) GetCorners(this Camera camera) {
            Vector2 dim = camera.GetOrthographicDimensions();
            Vector3 halfDim = (Vector3)(dim / 2f);
            Vector3 minCorner = camera.transform.position - halfDim;
            Vector3 maxCorner = camera.transform.position + halfDim;
            return (minCorner, maxCorner);
        }

        public static bool FullyOnScreen(this Camera camera, SpriteRenderer spriteRenderer) {
            Bounds spriteBounds = spriteRenderer.bounds;
            return camera.FullyOnScreen(spriteBounds.min, spriteBounds.max);
        }

        public static bool PartiallyOnScreen(this Camera camera, Vector2 min, Vector2 max) {
            (Vector2, Vector2) camCorners = camera.GetCorners();

            // if the min corner is within the screen
            // so greater than cam min and less than cam max.
            bool minCornerA = min.x > camCorners.Item1.x && min.y > camCorners.Item1.y;
            bool minCornerB = min.x < camCorners.Item2.x && min.y < camCorners.Item2.y;

            // if the max corner is within the screen
            // so greater than cam min and less than cam max.
            bool maxCornerA = max.x > camCorners.Item1.x && max.y > camCorners.Item1.y;
            bool maxCornerB = max.x < camCorners.Item2.x && max.y < camCorners.Item2.y;

            if (minCornerA && minCornerB) {
                return true;
            }
            if (maxCornerA && maxCornerB) {
                return true;
            }
            return false;
        }

        public static bool FullyOnScreen(this Camera camera, Vector2 min, Vector2 max) {
            (Vector2, Vector2) camCorners = camera.GetCorners();
            if (min.x < camCorners.Item1.x || max.x > camCorners.Item2.x) {
                return false;
            }
            if (min.y < camCorners.Item1.y || max.y > camCorners.Item2.y) {
                return false;
            }
            return true;
        }

        public static BoundsInt GetBoundsInt(this Camera camera, Grid grid) {
            (Vector2, Vector2) corners = camera.GetCorners();
            BoundsInt bounds = new BoundsInt();
            bounds.SetMinMax(grid.WorldToCell(corners.Item1), grid.WorldToCell(corners.Item2));
            return bounds;
        }

    }

}
