﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BuffImageScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

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

            gameObject.GetComponent<Image>().sprite = buff.Icon;
            text.SetText($"{remaining:0.00}");
        }
    }
}