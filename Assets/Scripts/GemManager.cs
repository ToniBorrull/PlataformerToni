using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemManager : MonoBehaviour
{
    public int gemCount;
    public TMP_Text gemText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gemText.text = gemCount.ToString();
    }
}
