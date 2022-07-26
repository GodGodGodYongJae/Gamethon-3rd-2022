using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public int ChapterNum;
    public int StageNum;
    public short WaveNum;

    PattenGenerator generator;
    public void Start()
    {
        ChapterNum = 1;
        StageNum = 1;
        WaveNum = 1;
        PattenGenerator generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
    }

    public void NextWave()
    {
        WaveNum++;
        generator = null;
        generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
        if(generator.isNotWave)
        {
            WaveNum = 1;
            StageNum++;
            generator = null;
            generator = new PattenGenerator(ChapterNum, StageNum, WaveNum);
            if(generator.isNotWave)
            {
               // Debug.Log("더이상 다음 Stage가 없습니다.");
            }
        }
    }
}
