using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SwordSkillCinemachine : MonoBehaviour
{
    [System.Serializable]
    public class EnemiesList : SerializableDictionary<string, GameObject> { };
    // 1번째는 피격 2번째는 죽음

    public EnemiesList e_enermy = new EnemiesList();


    [SerializeField]
    PlayableDirector Director;
    //[SerializeField]
    private string name;
    [SerializeField]
    private string TrackName;
    [SerializeField]
    Animator anim;
    [SerializeField]
    bool isDamage;
    private GameObject go;

    bool isStart;
    // Start is called before the first frame update
    void Update()
    {
        if (!isStart)
            Run();
    }

    private void Run()
    {
        isStart = true;
        Director = GetComponent<PlayableDirector>();
        name = EnemyFactoryMethod.Instance.GetTargetName();

        go = Instantiate(e_enermy[name]);
        CinemachineUnit unit = go.GetComponent<CinemachineUnit>();
        anim = go.GetComponent<Animator>();

        TimelineAsset asset = Director.playableAsset as TimelineAsset;
        //foreach (TrackAsset track in asset.GetOutputTracks())
        //    if (track.name == TrackName)
        //        asset.DeleteTrack(track);
        //AnimationTrack newTrack = asset.CreateTrack<AnimationTrack>(null,TrackName);

        //TimelineClip clip = newTrack.CreateClip(unit.clips[0]);
        //clip.start = AnimStart;
        //clip.timeScale = TimeScale;
        //clip.duration = clip.duration / clip.timeScale;
        foreach (TrackAsset track in asset.GetOutputTracks())
        {
            if (track.name == TrackName)
            {

                Director.SetGenericBinding(track, anim);
                // var animPlayableAsset = (AnimationPlayableAsset)track.GetClips();
                //animPlayableAsset.position = pos;
                //animPlayableAsset.rotation = rotate;
            }

        }
        //var animPlayableAsset = (AnimationPlayableAsset)asset.CreateClip(myClip).asset;
        //Director.SetGenericBinding(TrackName, anim);
        //Director.Play();
        StartCoroutine(PlayTimelineRoutine(Director));
    }


    private IEnumerator PlayTimelineRoutine(PlayableDirector playableDirector)
    {
        playableDirector.Play();
        yield return new WaitForSeconds((float)playableDirector.duration);
        onComplete();

    }
    private void onComplete()
    {
        Debug.Log("끝");
        Destroy(go);
        CutSceneManager.Instance.OnScene(false, CutSceneManager.Events.SwordSkill,true);
        if (isDamage)
        {
            IDamageable damageable = EnemyFactoryMethod.Instance.target.GetComponent<IDamageable>();
            damageable.Damage(450,this.gameObject);
        }
        isStart = false;
           
    }
}
