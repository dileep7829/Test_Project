using System;
using TMPro;
using UnityEngine;
using Utils;

namespace Controllers.Game
{
    public class ScoreManager : MonoBehaviour
    {
    
        [SerializeField] private  TMP_Text txtMatchCount;
        [SerializeField] private  TMP_Text txtTurnCount;
        [SerializeField] private  TMP_Text txtScoreCount;
        private int _score;
        private int _turnCount;
        private int _matchCount;
        private int _matchSteak = 0;
        private int _unmatchSteak = 0;

        private void OnEnable()
        {
            EventsManager.Instance.OnItemMatched += OnItemMatchedEvent;
            EventsManager.Instance.OnItemUnMatched += OnItemUnmatchedEvent;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnItemMatched -= OnItemMatchedEvent;
            EventsManager.Instance.OnItemUnMatched -= OnItemUnmatchedEvent;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            ResetData();
        }

        private void ResetData()
        {
            _matchCount = 0;
            _turnCount = 0;
            _score = 0;
            _matchSteak = 0;
            _unmatchSteak = 0;
        }

        private void OnItemMatchedEvent(object sender, EventArgs e)
        {
            IncrementTurnCount();
            IncrementMatchCount();
            _matchSteak++;
            _unmatchSteak = 0;
            CalculateAndSetScore();
            CheckWinCondition();
        }

        private void OnItemUnmatchedEvent(object sender, EventArgs e)
        {
            IncrementTurnCount();
            _matchSteak = 0;
            _unmatchSteak++;
            CalculateAndSetScore();
        }

        private void CalculateAndSetScore()
        {
            _score += _matchSteak * GlobalData.MATCH_MULTIPLIER + _unmatchSteak * GlobalData.UNMATCH_MULTIPLIER;
            txtScoreCount.text = _score + "";
        }
    
        private void CheckWinCondition()
        {
            if (_matchCount >= GlobalData.targetCount)
            {
                EventsManager.Instance.OnGameFinished.Invoke(this,null);
            }
        }

        private void IncrementMatchCount()
        {
            txtMatchCount.text = ++_matchCount + "";
        }
    
        private void IncrementTurnCount()
        {
            txtTurnCount.text = ++_turnCount + "";
        }
    }
}
