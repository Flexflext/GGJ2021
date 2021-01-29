using System;
using TMPro;
using UnityEngine;

public class BuffImageScript : MonoBehaviour
{
    [SerializeField] private GameObject text;

    public PlayerBuff buff;

    private void Update()
    {
        if (buff != null)
        {
            var remaining = buff.Duration - (buff.ActiveSince - DateTime.Now).TotalSeconds;
            if (remaining <= 0)
            {
                Destroy(this);
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = buff.Icon;
            text.GetComponent<TextMeshPro>().SetText($"{remaining:0.00}");
        }
    }
}