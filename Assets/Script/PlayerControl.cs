using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float move_speed = 3.0f;

    public float dif_speed = 0.003f; // 1フレーム毎のアニメーション速度増加分
    float ani_speed = 0.0f; // アニメーション速度

    void Update()
    {
        float input_h = Input.GetAxis("Horizontal");
        float input_v = Input.GetAxis("Vertical");

        Vector3 dir_camera_forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 dir_move = dir_camera_forward * input_v + Camera.main.transform.right * input_h;

        Vector3 set_velocity = new Vector3(
            dir_move.x,
            gameObject.GetComponent<Rigidbody>().velocity.y,
            dir_move.z
            );

        if (0.1f < set_velocity.magnitude)
        {
            if (Input.GetButton("Fire1"))
            {
            }
            else
            {
                set_velocity *= 0.5f;
            }
            set_dir(new Vector3(
                gameObject.GetComponent<Rigidbody>().velocity.x,
                0.0f,
                gameObject.GetComponent<Rigidbody>().velocity.z
                ));
            gameObject.GetComponent<Rigidbody>().velocity = set_velocity * move_speed;

            gameObject.GetComponent<Animator>().SetFloat("speed", set_velocity.magnitude);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetFloat("speed", -1.0f);
        }

        //gameObject.GetComponent<Animator>().SetFloat("speed", ani_speed);
    }

    private void set_dir(Vector3 _dir)
    {
        transform.rotation = Quaternion.LookRotation(_dir);
    }


}
