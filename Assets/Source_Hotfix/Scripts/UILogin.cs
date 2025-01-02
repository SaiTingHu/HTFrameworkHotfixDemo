using HT.Framework;
using HT.Framework.Deployment;
using UnityEngine.UI;

namespace HotfixDemo
{
    /// <summary>
    /// 登录界面
    /// </summary>
    [UIResource("ui", "Assets/Source_Hotfix/Prefabs/LoginPanel.prefab", "LoginPanel")]
    public class UILogin : UILogicResident
    {
        protected override bool IsAutomate => false;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void OnInit()
        {
            base.OnInit();

            UIEntity.FindChildren("LoginButton").rectTransform().AddEventListener(OnLogin);
        }

        public override void OnOpen(params object[] args)
        {
            base.OnOpen(args);

            UIEntity.FindChildren("Txt_Version").GetComponent<Text>().text = "当前资源版本：" + DeploymentConfig.Current.LocalVersion.Version;
            UIEntity.FindChildren("LoginButton/Text").GetComponent<Text>().text = "登录 v2.0.0";
        }

        private void OnLogin()
        {
            Main.m_Procedure.SwitchProcedure<ChooseProcedure>();
        }
    }
}