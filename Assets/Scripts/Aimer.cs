using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    public bool startEnabled;
    public bool lookAtMouse;
    public Transform targetTransform;

    private Camera m_MainCamera;
    private Vector3 m_MousePos;
    private Vector3 m_Direction;
    private float m_Angle;
    private bool m_Enabled;
    private bool paused;

    protected Transform m_Transform;

    #region Public Functions

    public void StartAiming()
    {
        m_Enabled = true;
    }

    public void StopAiming()
    {
        m_Enabled = false;
    }

    #endregion

    private void Start()
    {
        GameController.pauseEvent.AddListener(PauseListener);
        paused = false;
        m_Transform = this.transform;

        m_MainCamera = Camera.main;

        if (startEnabled)
        {
            m_Enabled = true;
        }
    }

    void PauseListener()
    {
        paused = !paused;
    }

    private void Update()
    {
        if (m_Enabled && !paused)
        {
            if (lookAtMouse)
            {
                m_MousePos = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);

                m_Direction = m_MousePos - transform.position;
                m_Angle = Mathf.Atan2(m_Direction.y, m_Direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
            }
            else
            {
                m_Direction = targetTransform.position - transform.position;
                m_Angle = Mathf.Atan2(m_Direction.y, m_Direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(m_Angle, Vector3.forward);
            }
        }
    }
}
