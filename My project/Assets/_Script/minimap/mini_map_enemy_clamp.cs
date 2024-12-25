using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mini_map_enemy_clamp : MonoBehaviour
{
    public Camera minimapCamera;         // The minimap camera
    public RectTransform minimapUI;      // The minimap UI (RawImage or RectTransform)
    public LayerMask enemyLayer;         // The layer indicating enemies (enemy_mini)
    public Transform player;             // The player's position to center the minimap

    private void Update()
    {
        // Find all enemies within a large radius using the 'enemy_mini' layer
        Collider[] enemies = Physics.OverlapSphere(player.position, 1000f, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            Transform enemyIcon = enemy.transform;  // Assuming each enemy has a UI icon in the "enemy_mini" layer

            // Convert enemy world position to minimap camera's viewport coordinates
            Vector3 viewportPos = minimapCamera.WorldToViewportPoint(enemyIcon.position);

            RectTransform enemyIconRect = enemyIcon.GetComponent<RectTransform>();

            // Check if the enemy is inside the minimap's viewport bounds
            if (viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 && viewportPos.z >= 0)
            {
                // Enemy is within the minimap bounds, place the icon accordingly
                enemyIconRect.anchoredPosition = new Vector2(
                    (viewportPos.x - 0.5f) * minimapUI.sizeDelta.x,
                    (viewportPos.y - 0.5f) * minimapUI.sizeDelta.y);
            }
            else
            {
                // Enemy is outside the minimap bounds, clamp the icon to the edge
                Vector2 clampedPos = ClampToMinimapEdge(viewportPos, minimapUI);
                enemyIconRect.anchoredPosition = clampedPos;
            }
        }
    }

    // Function to clamp the enemy position to the edge of the minimap
    private Vector2 ClampToMinimapEdge(Vector3 viewportPos, RectTransform minimapUI)
    {
        Vector2 minimapCenter = minimapUI.sizeDelta / 2f;  // Get the center point of the minimap
        Vector2 direction = new Vector2(viewportPos.x - 0.5f, viewportPos.y - 0.5f).normalized;

        // Set the enemy icon position to the edge of the minimap
        return minimapCenter + direction * (minimapUI.sizeDelta.x / 2f);
    }

}