using HT.Framework;

namespace HotfixDemo
{
    /// <summary>
    /// 登录流程
    /// </summary>
    public class LoginProcedure : ProcedureBase
    {
        /// <summary>
        /// 进入流程
        /// </summary>
        /// <param name="lastProcedure">上一个离开的流程</param>
        public override void OnEnter(ProcedureBase lastProcedure)
        {
            base.OnEnter(lastProcedure);

            Main.m_UI.OpenUI<UILogin>();
        }

        /// <summary>
        /// 离开流程
        /// </summary>
        /// <param name="nextProcedure">下一个进入的流程</param>
        public override void OnLeave(ProcedureBase nextProcedure)
        {
            base.OnLeave(nextProcedure);

            Main.m_UI.CloseUI<UILogin>();
        }
    }
}