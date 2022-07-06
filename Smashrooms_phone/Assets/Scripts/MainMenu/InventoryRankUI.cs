using UnityEngine.UI;
using UnityEngine;

public class InventoryRankUI : MonoBehaviour
{
    [SerializeField] Button inventoryButton, rankUpButton;
    [SerializeField] GameObject inventoryMenu, rankUpMenu;
    [SerializeField] Sprite buttonActive, buttonDisabled; 

    private void Awake()
    {
        inventoryButton.onClick.AddListener(() => ChangeActiveState(InventoryRankUIState.openInventory));
        rankUpButton.onClick.AddListener(() => ChangeActiveState(InventoryRankUIState.openRankUp));

        ChangeActiveState(InventoryRankUIState.openInventory);
    }
    private void ChangeActiveState(InventoryRankUIState state)
    {
        inventoryButton.image.sprite = state == InventoryRankUIState.openInventory ? buttonActive : buttonDisabled;
        rankUpButton.image.sprite = state == InventoryRankUIState.openInventory ? buttonDisabled : buttonActive;

        inventoryMenu.SetActive(state == InventoryRankUIState.openInventory);
        rankUpMenu.SetActive(state == InventoryRankUIState.openRankUp);
    }

    private enum InventoryRankUIState
    {
        openInventory,
        openRankUp,
    }
}
