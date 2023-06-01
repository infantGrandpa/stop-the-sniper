using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace SniperProject
{
    public class WallBehaviour : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] List<Collider2D> colliders;

        [Header("Spawning")]
        [MinMaxSlider(-360, 360, true)]
        [SerializeField] Vector2 minMaxRotation = new(-360, 360);

        [Header("As Ghost...")]
        [SerializeField] Color validPositionColor;
        [SerializeField] Color invalidPositionColor;
        private Color startColor;

        private bool isGhost;

        private void GetSpriteStartColor()
        {
            startColor = sprite.color;
        }

        public void InitializeAsGhost()
        {
            GetSpriteStartColor();
            SetLayerAllChildren(References.nonCollidingObjectsLayerInt);
            ChooseRandomRotation();
            isGhost = true;
        }

        public void InitializeAsWall()
        {
            ResetColor();
            SetLayerAllChildren(References.obstacleLayerInt);

            isGhost = false;
        }

        private void ResetColor()
        {
            sprite.color = startColor;
        }

        public void UpdateGhostPosition(Vector2 newPosition)
        {
            if (!isGhost)
            {
                return;
            }

            transform.position = newPosition;
            SetGhostColor();
        }

        public bool IsCurrentPositionValid()
        {
            bool isValid = true;

            foreach (Collider2D thisCollider in colliders)
            {
                bool isColliding = thisCollider.IsTouchingLayers(References.obstacleLayerMaskInt);
                if (isColliding)
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }



        private void SetLayerAllChildren(int layer)
        {
            var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = layer;
            }
        }

        private void SetGhostColor()
        {
            Color targetColor = invalidPositionColor;
            if (IsCurrentPositionValid())
            {
                targetColor = validPositionColor;
            }

            sprite.color = targetColor;
        }

        private void ChooseRandomRotation()
        {
            float randomAngle = Random.Range(minMaxRotation.x, minMaxRotation.y);
            Vector3 rotation = new(transform.eulerAngles.x, transform.eulerAngles.y, randomAngle);
            transform.eulerAngles = rotation;
        }

    }
}
