using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyRepository
{
    // Repository: 데이터의 영속성 보장
    // 영속성: 프로그램을 종료해도 데이터가 보존되는 것
    
    private const string SAVE_KEY = nameof(CurrencyRepository);
    
    // Save
    public void Save(List<CurrencyDTO> dataList, string id)
    {
        CurrencySaveDatas datas = new CurrencySaveDatas();
        datas.DataList = dataList.ConvertAll(data => new CurrencySaveData
        {
            Type = data.Type,
            Value = data.Value
        });           

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + "_" + id, json);
    }
   
    // Load
    public List<CurrencyDTO> Load(string id)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + "_" + id))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + "_" + id);
        CurrencySaveDatas datas = JsonUtility.FromJson<CurrencySaveDatas>(json);

        return datas.DataList.ConvertAll<CurrencyDTO>(data => new CurrencyDTO(data.Type, data.Value));
    }
}

[Serializable]
public struct CurrencySaveData
{
    public ECurrencyType Type;
    public int Value;
}

[Serializable]
public class CurrencySaveDatas
{
    public List<CurrencySaveData> DataList;
}