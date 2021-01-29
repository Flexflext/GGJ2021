using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanelScript : MonoBehaviour
{
    [SerializeField]
    private GameObject BuffImagePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBuff(PlayerBuff playerBuff)
    {
        var buffImage = Instantiate(BuffImagePrefab);
        buffImage.GetComponent<BuffImageScript>().buff = playerBuff;
    }
}
