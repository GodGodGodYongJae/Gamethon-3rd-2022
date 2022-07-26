using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PattenGenerator
{

    public PattenGenerator(float ChapterNum,int StageNum,short WaveNum)
    {
        float wave = WaveNum * 0.1f;
        this.ChaptperNum = ChapterNum + wave;
        this.StageNum = StageNum;
        GeneratorInit();
        CreateUnit();
    }

    private float ChaptperNum;
    private int StageNum;

    // é��.���̺� / ���������ѹ� / Ŭ����
    Dictionary<float,Tuple<int,WavePatten>> GeneratorDic = new Dictionary<float, Tuple<int, WavePatten>>();
    private void CreateUnit()
    {
        foreach (var item in GeneratorDic)
        {
            if (item.Key == ChaptperNum && item.Value.Item1 == StageNum)
                item.Value.Item2.CreateUnit();
        }
    }

    private void GeneratorInit()
    {
        GeneratorDic.Add(1.1f, new Tuple<int, WavePatten>(1 ,new Chapter1_Stage1(1)));
        GeneratorDic.Add(1.2f, new Tuple<int, WavePatten>(1, new Chapter1_Stage1(2)));
    }
}

