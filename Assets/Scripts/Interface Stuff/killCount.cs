using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class killCount : MonoBehaviour
{
    public KillUI killCt;

    private TextMeshProUGUI m_killCountText;

    private void OnEnable()
    {
        m_killCountText = this.GetComponent<TextMeshProUGUI>();
        m_killCountText.text = killCt.killNum.ToString();
    }
}
