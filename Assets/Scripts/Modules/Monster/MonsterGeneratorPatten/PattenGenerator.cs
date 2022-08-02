using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class PattenGenerator
{

    public PattenGenerator(sbyte ChapterNum, sbyte StageNum, sbyte WaveNum)
    {
        //float wave = WaveNum * 0.01f;
        //float stage = StageNum * 0.1f;
        //float DecConvert = (ChapterNum + stage + wave)*100f;
        //this.GeneratorKey = DecConvert + (DecConvert / 10) * 6;
        string C = ChapterNum.ToString("X");
        string S = StageNum.ToString("X");
        string W = WaveNum.ToString("X");
        string A = C + S + W;
        GeneratorKey = Convert.ToInt32(A, 16);
        string SN = (StageNum + 1).ToString("X");
        string NW = 1.ToString("X");
        string NA = C + SN + NW;
        NextGeneratorKey = Convert.ToInt32(NA, 16);
       
        GeneratorInit();
        CreateUnit();
        isNextStage = FindNextStage();
       

    }

    private int GeneratorKey;
    public sbyte currentMaxWave;

    private int NextGeneratorKey;

    public bool isNextStage;

    // 챕터.웨이브 / 스테이지넘버 / 클래스
    Dictionary<int,WavePatten> GeneratorDic = new Dictionary<int,  WavePatten>();
    private void CreateUnit()
    {
        foreach (var item in GeneratorDic)
        {
            if (item.Key == GeneratorKey)
            {
                item.Value.CreateUnit();
                 currentMaxWave = item.Value.maxWave;


            }
               
        }
       
    }

    private bool FindNextStage()
    {

        foreach (var item in GeneratorDic)
        {
        
            if (item.Key == NextGeneratorKey)
            {
               
                return true;

            }

        }
        return false;
    }

    private void GeneratorInit()
    {
        //GeneratorDic.Add(1.11f, new Tuple<int, WavePatten>(1, new Chapter1_Stage1(1)));
        GeneratorDic.Add(0x0111,  new Chapter1_Stage1(1));
        GeneratorDic.Add(0x0112,  new Chapter1_Stage1(2));
        GeneratorDic.Add(0x0113, new Chapter1_Stage1(3));
        GeneratorDic.Add(0x0121,  new Chapter01_02(1));
        GeneratorDic.Add(0x0122, new Chapter01_02(2));
        GeneratorDic.Add(0x0123, new Chapter01_02(3));
    }



}

