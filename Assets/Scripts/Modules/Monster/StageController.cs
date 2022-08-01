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
    PattenGenerator generator;
    public void Start()
    {
        ChapterNum = 1;
        StageNum = 1;
        WaveNum = 0;
        //PattenGenerator generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
        //MaxWave = generator.currentMaxWave;
        MaxWave = 10;
        NextWave();
    }

    public void NextWave()
    {

        WaveNum++;
        if (WaveNum <= MaxWave)
        {
            generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
            MaxWave = generator.currentMaxWave;
        }
        else
        {
            WaveNum = 0;
            if (generator.isNextStage)
            {
                StageNum++;
            }
               
            else
            {
                isLast = true;
            }
            //Wave�� ������ ��� ���� ��������.
            // 1. �ƾ��� �����ش�.
            string string_stageNum;
            if (StageNum < 10) string_stageNum = "0" + StageNum;
            else string_stageNum = StageNum.ToString();
            UIManager.Instance.TextChange(UIManager.UI.StageNum, ChapterNum + "-" + string_stageNum);
            CutSceneManager.Instance.OnScene(true,CutSceneManager.Events.StageClear,true);
            // 1-1. �ƾ��� �ʿ��� Unit�� �����´�.
            // [ �ش� ������ LastMonster ��ü�� EnemyFactory���� ����־�� �� �� ����.]
            // 1-2. �ƾ��� ������ UI ȭ���� ����ش�.
            // [ �ش� ������ �ش� �ó׸ӽ� ��ũ��Ʈ���� ���� ���־���Ѵ�]
            // 1-3. �̶� ���� ���������� ���ٸ� �����ִ� UI â�� �ٸ���.
            // [ �Ƹ� �������� ��Ʈ�ѷ��� �̱������� �� �� ������. EF�� �Լ� �ϳ��� ���� Ȯ��������Ѵ�]
            // 2. UI ���� Ȯ�� ��ư�� ������ ���� ���������� �Ѿ��.

            // 2-1. �̶� ���� ���������� ���ٸ� ���� Ÿ��Ʋ�� ���ư���. 
            // 3. ���������� �Ѿ�鼭 Player�� Pos�� 0,0,0���� �����ش�.
            // 4. DeathObj�� ����ش�.
            // 5. ���� �������� ������ �����Ѵ�.

        }

        //if(generator.isNotWave)
        //{
        //    WaveNum = 1;
        //    StageNum++;
        //    generator = null;
        //    generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
        //    //Debug.Log(ChapterNum +""+ StageNum +""+ WaveNum);
        //    if(generator.isNotWave)
        //    {
        //        //Debug.Log("���̻� ���� Stage�� �����ϴ�.");
        //    }
        //}
       
    }
}
