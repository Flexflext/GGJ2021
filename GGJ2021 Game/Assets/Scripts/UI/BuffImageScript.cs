using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BuffImageScript : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI text;

    public PlayerBuff buff;

    private void Update()
    {
        if (buff != null)
        {
            var remaining = buff.Duration - (DateTime.Now - buff.ActiveSince).TotalSeconds;
            if (remaining <= 0)
            {
                Destroy(gameObject);
            }

            gameObject.GetComponent<Image>().sprite = buff.Icon;
            text.SetText($"{remaining:0.0}");
        }
    }
}