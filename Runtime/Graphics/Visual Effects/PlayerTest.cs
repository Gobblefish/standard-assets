using UnityEngine;

namespace MagnetBear {

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerTest : MonoBehaviour {

        private Rigidbody2D rb;

        // [Header("Movement Params")]
        public float acceleration = 50f;
        public float maxSpeed = 5f;
        public float x;
        public float y;

        void Awake() {
            rb = GetComponent<Rigidbody2D>();

        }

        void FixedUpdate() {
            Movement(Time.fixedDeltaTime);
        }

        void Movement(float dt) {
            x = UnityEngine.Input.GetAxisRaw("Horizontal");
            y = UnityEngine.Input.GetAxisRaw("Vertical"); 
            // y = 0f;

            Vector2 targetVel = new Vector2(x, y).normalized * maxSpeed;
            Vector2 velDiff = targetVel-rb.velocity;
            if (velDiff.sqrMagnitude < acceleration * dt * acceleration * dt) {
                rb.velocity = targetVel;
            }
            else {
                rb.velocity += (velDiff.normalized * acceleration * dt);
            }

        }

    }

}