using UnityEngine;
using System.Collections;

public class WinAnswerCondition : WinCondition
{
    public float _properAnswer;

    public override bool IsConditionAchieved
    {
        get { return _isCondtionAchieved; }
    }

    private bool _isCondtionAchieved;

    public void Answer(float answer)
    {
        if (answer == _properAnswer)
        {
            GUIController.ShowMessage("Odpowiedź " + answer.ToString() + " jest poprawna!", MessageColor.Green);

            _isCondtionAchieved = true;

            RefreshWinConditions();
        }
        else
        {
            GUIController.ShowMessage("Odpowiedź " + answer.ToString() + " jest niepoprawna", MessageColor.Red);
        }
    }
}
