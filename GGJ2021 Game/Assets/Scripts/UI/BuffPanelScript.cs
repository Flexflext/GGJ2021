using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject BuffImagePrefab;

    public void addBuff(PlayerBuff playerBuff)
    {
        var buffImage = Instantiate(BuffImagePrefab, gameObject.transform);
        buffImage.GetComponent<BuffImageScript>().buff = playerBuff;
        
        OnPointer onPointer = buffImage.AddComponent<OnPointer>();
        onPointer.AddEnterListener(e =>
        {
            var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
            itemInfoPanel.SetDisplayItem(playerBuff, true);
        });
        onPointer.AddExitListener(e =>
        {
            //Debug.Log("Mouse exit " + slotId);
            var itemInfoPanel = Game.Instance.UIManager.ItemInfoPanel.GetComponent<ItemInfoPanel>();
            itemInfoPanel.SetDisplayItem(null, true);
        });
    }
}
