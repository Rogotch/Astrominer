using DG.Tweening;
using System;
using UnityEngine;

public interface IDigInstrument
{
    enum ToolType {DRILL}
    
    #region Signals
    event Action<Vector2Int, Vector2Int> DiggingStarted;
    event Action<Vector2Int, Vector2Int> DiggingEnded;
    event Action<Vector2Int>             CellDigged;
    #endregion

    abstract bool IsCanDig             (Vector2Int target);                     
    abstract bool IsCanDigFrom         (Vector2Int from, Vector2Int to);        
    abstract bool IsCanDigToDirection  (Vector2Int from, Vector2Int direction); 
    abstract void Dig                  (Vector2Int from, Vector2Int to);  
    abstract void DiggingStart         (Vector2Int from, Vector2Int to);  
    abstract void DiggingEnd           (Vector2Int from, Vector2Int to);  
    abstract void DigCell              (Vector2Int from, Vector2Int cell);
}
