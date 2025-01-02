namespace HT.Framework.Demo.Hotfix
{
    /// <summary>
    /// 游戏界面
    /// </summary>
    [UIResource("ui", "Assets/Source_Hotfix/Prefabs/GamePanel.prefab", "GamePanel")]
    public class UIGame : UILogicResident
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();

            UIEntity.FindChildren("LogoutButton").rectTransform().AddEventListener(OnLogout);
            UIEntity.FindChildren("BackButton").rectTransform().AddEventListener(OnBack);
        }

        private void OnLogout()
        {
            Main.m_Procedure.SwitchProcedure<LoginProcedure>();
        }

        private void OnBack()
        {
            Main.m_Procedure.SwitchProcedure<ChooseProcedure>();
        }
    }
}