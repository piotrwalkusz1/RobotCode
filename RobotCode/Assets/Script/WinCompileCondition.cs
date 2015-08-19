using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    public class WinCompileCondition : WinCondition
    {
        public override bool IsConditionAchieved
        {
            get
            {
                return _isConditionAchieved;
            }
        }

        private bool _isConditionAchieved;

        void Awake()
        {
            Compiler.OnCompile += delegate()
            {
                _isConditionAchieved = true;

                RefreshWinConditions();
            };
        }
    }
}
