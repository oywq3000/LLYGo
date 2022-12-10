using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail_manager : MonoBehaviour
{

    [Header("target SkinnedMeshRenderer")]
    public GameObject game_obj_target;

    //The position of the previous target----------之前目标物体的位置
    private Vector3 v3_position_game_obj_target_before;

    [Header("how many trails-------定义多少个尾巴")]
    public int trail_count;

    [Header("The initial transparency of the tail------尾巴的初始透明度")][Range(0f, 1f)]
    public float trail_alpha;

    [Header("The time between each tail, in seconds---------每个尾巴间隔的时间，单位秒")]
    public float trail_interval_time;

    [Header("The speed at which each tail disappears------每个尾巴消失的速度")]
    public float trail_disappear_speed;

    [Header("Trail Color")]
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color color_trail;

    // Start is called before the first frame update
    void Start()
    {
        if (this.trail_count > 0&&this.game_obj_target!=null)
        {
            //Generate trail objects--------生成trail物体
            for (int i = 0; i < this.trail_count; i++)
            {
                GameObject trail = new GameObject("trail" + i);
                trail.transform.SetParent(this.transform);
                trail.AddComponent<MeshFilter>();
                trail.AddComponent<MeshRenderer>();
                trail.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off; //no shadow

                Material mat = new Material(Shader.Find("EasyGameStudio/trail"));
                mat.SetTexture("main_texture", this.game_obj_target.GetComponent<SkinnedMeshRenderer>().material.mainTexture);
                mat.SetColor("color_fresnel_emission", this.color_trail);
                trail.GetComponent<MeshRenderer>().material = mat;
                Trail_control trail_control = trail.AddComponent<Trail_control>();

                trail.SetActive(false);
            }

            StartCoroutine(this.trail_start());
        }

    }

    //The coroutine starts to make the tail----------协程开始制作尾巴
    IEnumerator trail_start()
    {
        while (true) 
        {
            for (int i = 0; i < this.trail_count; i++)
            {

                //If the position does not change, the tail will not be set--------位置没有变的话 就不设置尾巴
                if (this.v3_position_game_obj_target_before!= this.game_obj_target.transform.position)
                {
                    GameObject trail = this.transform.GetChild(i).gameObject;
                    trail.transform.position = this.game_obj_target.transform.position;
                    trail.transform.rotation = this.game_obj_target.transform.rotation;
                    if (trail.activeSelf == false)
                        trail.SetActive(true);
                    trail.GetComponent<Trail_control>().init(this.trail_disappear_speed, this.game_obj_target.GetComponent<SkinnedMeshRenderer>(), this.trail_alpha);               
                }
                this.v3_position_game_obj_target_before = this.game_obj_target.transform.position;

                yield return new WaitForSeconds(this.trail_interval_time);
            }
        }
    }
}
