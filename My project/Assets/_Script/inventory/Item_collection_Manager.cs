using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_collection_Manager : MonoBehaviour
{
    public Image woodImage;
    public Image metalImage;
    public Image bandagesImage;

    public Text woodCountText;
    public Text metalCountText;
    public Text bandagesCountText;

    public GameObject itemPanel; // Panel that contains the item images and counts

    private int woodCount = 0;
    private int metalCount = 0;
    private int bandagesCount = 0;

    private void Update()
    {
        // Check for mouse button clicks
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            // Perform a raycast from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // If the ray hits something
            {
                // Check if the object hit has an Items component
                Items item = hit.collider.GetComponent<Items>();
                if (item != null)
                {
                    // Print the type of item collected
                    Debug.Log($"Collected: {item.item_type}");

                    // Increment the appropriate count based on item type
                    switch (item.item_type)
                    {
                        case Items.ItemTypes.wood:
                            woodCount++;
                            break;
                        case Items.ItemTypes.metal:
                            metalCount++;
                            break;
                        case Items.ItemTypes.bandages:
                            bandagesCount++;
                            break;
                    }

                    // Update the UI counts
                    UpdateItemCounts();

                    // Destroy the item game object
                    Destroy(item.gameObject);
                }
            }
        }

        // Check for E key press to display the UI
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleItemPanel();
        }
    }

    private void UpdateItemCounts()
    {
        // Update the UI texts with the current counts
        woodCountText.text = $"Wood: {woodCount}";
        metalCountText.text = $"Metal: {metalCount}";
        bandagesCountText.text = $"Bandages: {bandagesCount}";

        // Update the visibility of item images based on count
        woodImage.gameObject.SetActive(woodCount > 0);
        metalImage.gameObject.SetActive(metalCount > 0);
        bandagesImage.gameObject.SetActive(bandagesCount > 0);
    }

    private void ToggleItemPanel()
    {
        // Toggle the item panel visibility
        itemPanel.SetActive(!itemPanel.activeSelf);
        UpdateItemCounts(); // Ensure counts are updated when showing the panel
    }
}
