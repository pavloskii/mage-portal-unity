using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetController : Singleton<WidgetController>
{
    // Quick hack script to get the tutorial going
    [SerializeField]
    float m_maxScale = 100f;

    Vector3 m_latestDirection;
    Vector2 m_touchStartPos = Vector2.zero;
    Vector2 m_touchEndPos = Vector2.zero;

    LineRenderer m_lineRenderer;
    Camera m_mainCamera;

    void Start()
    {
        m_mainCamera = GetComponent<Camera>();
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    internal Vector3 GetDirection() { return m_latestDirection; }

    void Update()
    {
        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    m_touchStartPos = touch.position;
                    break;
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    m_touchEndPos = touch.position;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    m_touchStartPos = Vector3.zero;
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0)) { m_touchStartPos = Input.mousePosition; }
        else if (Input.GetMouseButtonUp(0)) { m_touchStartPos = Vector2.zero; }

        if (Input.GetMouseButton(0)) { m_touchEndPos = Input.mousePosition; }

        if (m_touchStartPos != Vector2.zero)
        {
            Vector3 startPos = m_touchStartPos;
            startPos.z = 1;

            Vector2 distance = m_touchEndPos - m_touchStartPos;
            float scaledLength = Mathf.Clamp(distance.magnitude, 0, m_maxScale);

            Vector3 endPos = m_touchStartPos + (distance.normalized * scaledLength);
            endPos.z = 1;

            startPos = m_mainCamera.ScreenToWorldPoint(startPos);
            endPos = m_mainCamera.ScreenToWorldPoint(endPos);

            m_lineRenderer.SetPosition(0, startPos);
            m_lineRenderer.SetPosition(1, endPos);

            m_latestDirection = Quaternion.AngleAxis(90 - transform.eulerAngles.x, Vector3.right) * (endPos - startPos);
        }
        else
        {
            m_lineRenderer.SetPosition(0, Vector3.zero);
            m_lineRenderer.SetPosition(1, Vector3.zero);
            m_latestDirection = Vector3.zero;
        }
    }

}
