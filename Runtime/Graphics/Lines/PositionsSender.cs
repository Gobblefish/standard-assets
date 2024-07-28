// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;

namespace GobbleFish.Graphics {

    public abstract class PositionsSender : MonoBehaviour {

        public IPositionsReciever positionsReciever;

        public void SendPositions(Vector3[] positions) {
            if (positionsReciever == null) {
                positionsReciever = GetComponent<IPositionsReciever>();
            }
            positionsReciever.RecievePositions(positions);
        }

    }

}