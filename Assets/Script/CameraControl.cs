using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject m_targetObject;
    private Vector3 m_vec3Offset;

    private Vector3 save_pos;

    void Start()
    {
        gameObject.transform.LookAt(m_targetObject.transform);
        m_vec3Offset = m_targetObject.transform.position - gameObject.transform.position;

        save_pos = m_targetObject.transform.position;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        Vector3 move_delta = m_targetObject.transform.position - save_pos;
        gameObject.transform.position += move_delta;
        save_pos = m_targetObject.transform.position;

        // マウスの右クリックを押している間
        if (Input.GetMouseButton(0))
        {
            // カメラの移動速度
            float move_speed = 100.0f;
            // マウスの移動量
            float mouseInputX = Input.GetAxis("Mouse X");
            float mouseInputY = Input.GetAxis("Mouse Y") * -1.0f;

            transform.RotateAround(m_targetObject.transform.position, Vector3.up, mouseInputX * Time.deltaTime * move_speed);
            transform.RotateAround(m_targetObject.transform.position, transform.right, mouseInputY * Time.deltaTime * move_speed);

            m_vec3Offset = m_targetObject.transform.position - gameObject.transform.position;

        }
    }
}
