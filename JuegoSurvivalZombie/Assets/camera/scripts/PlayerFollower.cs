using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform obj_trans;
    public Demo main_player;
    public float camera_offset_pos_y;

    private void Awake()
    {
        obj_trans = this.transform;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (main_player != null){
            Vector3 camera_new_pos = obj_trans.position;
            camera_new_pos.x = main_player.obj_trans.position.x;
            camera_new_pos.y = main_player.obj_trans.position.y + camera_offset_pos_y;
            obj_trans.position = camera_new_pos;
        }
    }
}
