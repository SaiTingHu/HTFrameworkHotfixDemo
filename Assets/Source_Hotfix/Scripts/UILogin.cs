namespace HT.Framework.Demo.Hotfix
{
    /// <summary>
    /// 登录界面
    /// </summary>
    [UIResource("ui", "Assets/Source_Hotfix/Prefabs/LoginPanel.prefab", "LoginPanel")]
    public class UILogin : UILogicResident
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();

            UIEntity.FindChildren("LoginButton").rectTransform().AddEventListener(OnLogin);
        }

        private void OnLogin()
        {
            Main.m_Procedure.SwitchProcedure<ChooseProcedure>();
        }
    }
}