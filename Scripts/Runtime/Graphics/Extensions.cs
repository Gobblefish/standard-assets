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

        public static bool OnScreen(this Camera camera, SpriteRenderer spriteRenderer) {
            Vector2 dim = camera.GetOrthographicDimensions();
            Vector3 halfDim = (Vector3)(dim / 2f);
            Vector3 minCorner = camera.transform.position - halfDim;
            Vector3 maxCorner = camera.transform.position + halfDim;

            Bounds spriteBounds = spriteRenderer.bounds;

            if (spriteBounds.min.x < minCorner.x || spriteBounds.max.x > maxCorner.x) {
                return false;
            }
            if (spriteBounds.min.y < minCorner.y || spriteBounds.max.y > maxCorner.y) {
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
