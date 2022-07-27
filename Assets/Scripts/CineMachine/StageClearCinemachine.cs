using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class StageClearCinemachine : MonoBehaviour
{
    private Dictionary<string, GameObject> e_enermy = new Dictionary<string, GameObject>();

    [SerializeField]
    PlayableDirector Director;
    //[SerializeField]
    private string name;
    //[SerializeField]
    //private string TrackName;
    [SerializeField]
    Animator anim;
    [SerializeField]
    bool isDamage;
    private GameObject go;

    bool isStart;
    // Start is called before the first frame update
    private void Start()
    {
        e_enermy = EnemyFactoryMethod.Instance.e_enemyPrefabDictionary;
    }
    void Update()
    {
        if (!isStart)
            Run();
    }

    private void Run()
    {
        isStart = true;
        Director = GetComponent<PlayableDirector>();
        name = EnemyFactoryMethod.Instance.GetTargetName(EnemyFactoryMethod.Instance.LastTarget);

        go = Instantiate(e_enermy[name]);
        CinemachineUnit unit = go.GetComponent<CinemachineUnit>();
        anim = go.GetComponent<Animator>();

        TimelineAsset asset = Director.playableAsset as TimelineAsset;

        foreach (TrackAsset track in asset.GetOutputTracks())
        {
            if (track.name == name)
            {
                Director.SetGenericBinding(track, anim);
            }

        }
       
    }


}
