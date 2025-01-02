namespace HT.Framework.Demo.Hotfix
{
    /// <summary>
    /// 选择界面
    /// </summary>
    [UIResource("ui", "Assets/Source_Hotfix/Prefabs/ChoosePanel.prefab", "ChoosePanel")]
    public class UIChoose : UILogicResident
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();

            UIEntity.FindChildren("Role1Button").rectTransform().AddEventListener(OnChooseRole);
            UIEntity.FindChildren("Role2Button").rectTransform().AddEventListener(OnChooseRole);
        }

        private void OnChooseRole()
        {
            Main.m_Procedure.SwitchProcedure<GameProcedure>();
        }
    }
}