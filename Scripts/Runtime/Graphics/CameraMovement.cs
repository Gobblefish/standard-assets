/* --- Libraries --- */
// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Gobblefish.Graphics {

    ///<summary>
    /// Controls the position and quality of the camera.
    ///<summary>
    public class CameraMovement : MonoBehaviour {

        // The distance of the plane that the camera sits on.
        public const float CAMERA_PLANE_DISTANCE = -10f;
        
        // The plane that this camera sits on.
        public static Vector3 CameraPlane => Vector3.forward * CAMERA_PLANE_DISTANCE;

        // The target that this camera follows if it has nothing else to follow.
        [SerializeField]
        private Transform m_DefaultTarget;

        // The position that this camera is meant to be at.
        [SerializeField]
        private List<Transform> m_Targets = new List<Transform>();

        // The speed with which the camera moves.
        [SerializeField]
        private float m_MoveSpeed = 0f;

        // Runs every fixed interval.
        void FixedUpdate() {
            MoveToTarget(Time.fixedDeltaTime);
        }

        public void SetDefaultTarge(Transform target) {
            m_DefaultTarget = target;
        }

        // Sets the target position of the camera.
        public void AddTarget(Transform target) {
            if (!m_Targets.Contains(target)) {
                m_Targets.Add(target);
            }
        }

        // Sets the target position of the camera.
        public void RemoveTarget(Transform target) {
            if (m_Targets.Contains(target)) {
                m_Targets.Remove(target);
            }
        }
        
        // Moves the camera to the target position.
        public void MoveToTarget(float dt) {
            Vector2 aggregatedTargets = new Vector2(0f, 0f);
            if (m_Targets.Count == 0) {
                aggregatedTargets = m_DefaultTarget.position;
            }
            else {
                GetTarget(out aggregatedTargets);
            }
            
            Vector3 targetPosition = (Vector3)aggregatedTargets + CameraPlane;
            // transform.Move(targetPosition, m_MoveSpeed, dt);
        
        }

        // Moves an obstacle towards a target.
        public void Move(Vector2 destination, float speed, float deltaTime) {
            Vector2 pos = (Vector2)transform.position;
            if (destination == pos) {
                return;
            }

            Vector2 displacement = destination - pos;
            Vector3 deltaPosition = displacement.normalized * speed * deltaTime;
            if (displacement.magnitude < deltaPosition.magnitude) {
                deltaPosition = displacement;
            }

            transform.position += deltaPosition;
            
        }

        void GetTarget(out Vector2 target) {
            target = new Vector2(0f, 0f);
            for (int i = 0; i < m_Targets.Count; i++) {
                target += (Vector2)m_Targets[i].position;
            }
            target /= m_Targets.Count;
        }

    }

}