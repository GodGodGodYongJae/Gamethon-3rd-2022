using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public sbyte ChapterNum;
    public sbyte StageNum;
    public sbyte WaveNum;
    public sbyte MaxWave;

    public bool isLast;

    [SerializeField]
    bool testMode;
    public void Start()
    {
        ChapterNum = (testMode)?(sbyte)1:ScenesManager.Instance.StartChpater;
        StageNum = 1;
        WaveNum = 0;
        MaxWave = 99;
        NextWave();
        ChangeStageText();
    }

    public GameObject DeathObj;
    public void NextWave()
    {
        WaveNum++;
        if(WaveNum <= MaxWave)
        {
            EnemyFactoryMethod.Instance.EmptyDeathEnemy();
            EnemyGenerators.Instance.CreateUnit(ChapterNum, StageNum, WaveNum);
            MaxWave = EnemyGenerators.Instance.CurrentMaxWave;
        }
        else
        {
            WaveNum = 0;
            if(EnemyGenerators.Instance.FindNextStage(ChapterNum, StageNum))
            {
                StageNum++;
            }
            else
            {
                int saveChpater = ChapterNum + 1;
                PlayFabData.Instance.SetUserData("ClearChapter", saveChpater.ToString());
                isLast = true;
            }

            ChangeStageText();
            CutSceneManager.Instance.OnScene(true, CutSceneManager.Events.StageClear, true);
        }
        if(testMode.Equals(false))
        SaveLastStage();
    }

    void ChangeStageText()
    {
        string string_stageNum;
        if (StageNum < 10) string_stageNum = "0" + StageNum;
        else string_stageNum = StageNum.ToString();

        UIManager.Instance.TextChange(UIManager.UI.StageNum, ChapterNum + "-" + string_stageNum);
    }
    void SaveLastStage()
    {
        sbyte saveChpater = (isLast) ? (sbyte)((int)ChapterNum+1) : ChapterNum;
        int LastStageVal = (isLast) ? 
            (saveChpater * 10000) + 100 + 1 :
            (saveChpater * 10000) + (StageNum * 100) + WaveNum;
        PlayFabData.Instance.SetUserData("LastStage", LastStageVal.ToString());    
    }

    #region oldcode
    //public void Old_NextWave()
    //{

    //    WaveNum++;
    //    if (WaveNum <= MaxWave)
    //    {
    //        generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
    //        MaxWave = generator.currentMaxWave;
    //    }
    //    else
    //    {
    //        WaveNum = 0;
    //        if (generator.isNextStage)
    //        {
    //            StageNum++;
    //        }
               
    //        else
    //        {
    //            isLast = true;
    //        }
    //        //Wave가 끝났을 경우 다음 스테이지.
    //        // 1. 컷씬을 보여준다.
    //        string string_stageNum;
    //        if (StageNum < 10) string_stageNum = "0" + StageNum;
    //        else string_stageNum = StageNum.ToString();
    //        UIManager.Instance.TextChange(UIManager.UI.StageNum, ChapterNum + "-" + string_stageNum);
    //        CutSceneManager.Instance.OnScene(true,CutSceneManager.Events.StageClear,true);
    //        // 1-1. 컷씬에 필요한 Unit을 가져온다.
    //        // [ 해당 내용은 LastMonster 자체를 EnemyFactory에서 들고있어야 할 것 같다.]
    //        // 1-2. 컷씬이 끝나면 UI 화면을 띄어준다.
    //        // [ 해당 내용은 해당 시네머신 스크립트에서 실행 해주어야한다]
    //        // 1-3. 이때 다음 스테이지가 없다면 보여주는 UI 창이 다르다.
    //        // [ 아마 스테이지 컨트롤러를 싱글톤으로 뺄 순 없으니. EF에 함수 하나를 만들어서 확인해줘야한다]
    //        // 2. UI 에서 확인 버튼을 누르면 다음 스테이지로 넘어간다.

    //        // 2-1. 이때 다음 스테이지가 없다면 메인 타이틀로 돌아간다. 
    //        // 3. 스테이지로 넘어가면서 Player의 Pos를 0,0,0으로 맞춰준다.
    //        // 4. DeathObj를 비워준다.
    //        // 5. 다음 스테이지 유닛을 생성한다.

    //    }


       
    //}
    #endregion
}
