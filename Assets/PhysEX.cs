using System;
using UnityEngine;

namespace EMILtools.Extensions
{
    public static class PhysEX
    {

        [Serializable]
        public struct GroundedSettings
        {
            public Transform feetPoint;
            public float checkDist;
            public LayerMask mask;
        }


        static void GroundDefaultCheck(this Transform t, ref GroundedSettings ground)
        {
            if (!ground.feetPoint)
            {
                var newFeetPoint = new GameObject("Feet Point Auto-Generated");
                newFeetPoint.transform.parent = t;
                newFeetPoint.transform.localPosition = t.position.With(y: t.position.y + 0.02f);
                ground.feetPoint = newFeetPoint.transform;
            }

            if (ground.checkDist == 0) ground.checkDist = 0.08f;
        }

        public static bool IsGrounded(this Transform transform, ref GroundedSettings ground)
        {
            transform.GroundDefaultCheck(ref ground);

            bool isGrounded = Physics.Raycast(ground.feetPoint.position,
                                        -transform.up,
                                        out RaycastHit hit,
                                        ground.checkDist,
                                        ground.mask);

            return isGrounded;
        }
        
        public static bool IsGrounded2D(this Transform transform, ref GroundedSettings ground)
        {
            transform.GroundDefaultCheck(ref ground);

            bool isGrounded = Physics2D.Raycast(
                ground.feetPoint.position,
                -transform.up,
                ground.checkDist,
                ground.mask);

            return isGrounded;
        }


    }
}
