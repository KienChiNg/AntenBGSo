using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class LessionManager : MonoBehaviour
{
    public static LessionManager instance;
    public enum Lession : byte
    {
        Lession_1,
        Lession_2,
        Lession_3,
    }
    [SerializeField]
    private Lession _lessions;
    public List<LessionChildPart> lessionChildParts;
    [HideInInspector]
    public DrawWave drawWave;
    [ShowIf("ShowIfLession_2")]
    public UIControllerWave uIControllerWave;
    public bool ShowIfLession_1 => _lessions == Lession.Lession_1;
    public bool ShowIfLession_2 => _lessions == Lession.Lession_2;
    public bool ShowIfLession_3 => _lessions == Lession.Lession_3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        switch (_lessions)
        {
            case Lession.Lession_1:
                break;
            case Lession.Lession_2:
                drawWave = GetComponent<DrawWave>();

                drawWave.Init();
                uIControllerWave.Init();
                break;
        }
    }
    public void ResetAllUIPart()
    {
        foreach (LessionChildPart lessionChildPart in lessionChildParts)
        {
            lessionChildPart.Reset();
        }
    }
    
    
}
