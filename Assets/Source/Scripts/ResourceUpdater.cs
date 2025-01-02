using HT.Framework;
using HT.Framework.Deployment;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 资源更新程序
/// </summary>
public class ResourceUpdater : SingletonBehaviourBase<ResourceUpdater>
{
    protected override void Awake()
    {
        base.Awake();

        AddModule<UpdateResourcePanel>();
        AddModule<SurePanel>();
    }
    private void Start()
    {
        StartCoroutine(CheckAll());
    }

    /// <summary>
    /// 检测及更新资源
    /// </summary>
    private IEnumerator CheckAll()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            GetModule<SurePanel>().Open("当前无网络连接，请查看网络设置！", Application.Quit, Application.Quit);
            yield break;
        }

        //检测资源
        GetModule<UpdateResourcePanel>().Open();
        GetModule<UpdateResourcePanel>().SetProgress(0, "检测资源版本......");
        yield return DeploymentConfig.Current.CheckResource();

        //更新资源
        void update()
        {
            GetModule<UpdateResourcePanel>().UpdateResource();
        }

        if (DeploymentConfig.Current.CheckInfo.IsVersionChanged && DeploymentConfig.Current.CheckInfo.TotalDownloadFileNumber > 0)
        {
            string version = DeploymentConfig.Current.CheckInfo.Version;
            string notes = DeploymentConfig.Current.CheckInfo.ReleaseNotes;
            int number = DeploymentConfig.Current.CheckInfo.TotalDownloadFileNumber;
            int size = DeploymentConfig.Current.CheckInfo.TotalDownloadFileSize / 1000;
            if (size <= 0) size = 1;

            StringToolkit.BeginConcat();
            StringToolkit.Concat($"检测到新版本 <color=red>{version}</color>，更新日志：\r\n");
            StringToolkit.Concat($"<color=cyan>{notes}</color>\r\n\r\n");
            StringToolkit.Concat($"需下载资源文件<color=red>{number}</color>个[<color=red>约{size}M</color>]，是否确认下载？");
            string content = StringToolkit.EndConcat();

            GetModule<SurePanel>().Open(content, update, Application.Quit);
        }
        else
        {
            update();
        }
    }

    #region UpdateResourcePanel
    /// <summary>
    /// 更新资源界面
    /// </summary>
    public class UpdateResourcePanel : ModuleBase
    {
        private GameObject _updateResourcePanel;
        private Text _txt_Title;
        private Image _img_UpdateProgress;
        private Text _txt_UpdateProgress;
        private float _progress;
        private float _timer;

        /// <summary>
        /// 资源更新提示
        /// </summary>
        private string Title
        {
            get
            {
                return _txt_Title.text;
            }
            set
            {
                _txt_Title.text = value;
            }
        }
        /// <summary>
        /// 资源更新进度
        /// </summary>
        private float Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
                _img_UpdateProgress.rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(-312.5f, -188f), new Vector2(0, -188f), _progress);
                _img_UpdateProgress.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(0, 35), new Vector2(625, 35), _progress);
                _txt_UpdateProgress.text = $"{(int)(value * 100)}%";
            }
        }

        public override void OnInit()
        {
            base.OnInit();

            _updateResourcePanel = Host.FindChildren("UpdateResourcePanel");
            _txt_Title = _updateResourcePanel.GetComponentByChild<Text>("Txt_Title");
            _img_UpdateProgress = _updateResourcePanel.GetComponentByChild<Image>("Img_UpdateProgress");
            _txt_UpdateProgress = _updateResourcePanel.GetComponentByChild<Text>("Txt_UpdateProgress");
        }

        /// <summary>
        /// 打开更新资源界面
        /// </summary>
        public void Open()
        {
            _updateResourcePanel.SetActive(true);
        }
        /// <summary>
        /// 设置资源更新进度和提示
        /// </summary>
        /// <param name="progress">进度</param>
        /// <param name="prompt">提示信息</param>
        public void SetProgress(float progress, string prompt)
        {
            Progress = progress;
            Title = prompt;
        }
        /// <summary>
        /// 更新资源并进入程序
        /// </summary>
        public void UpdateResource()
        {
            Host.StartCoroutine(UpdateResourceCoroutine());
        }

        /// <summary>
        /// 更新资源并进入程序
        /// </summary>
        private IEnumerator UpdateResourceCoroutine()
        {
            yield return DeploymentConfig.Current.UpdateResource((info) =>
            {
                if (info.TotalDownloadFileNumber > 0)
                {
                    if (_timer < 1)
                    {
                        _timer += Time.deltaTime;
                    }
                    else
                    {
                        _timer -= 1;
                        SetProgress((float)info.DownloadedFileSize / info.TotalDownloadFileSize, $"更新资源中({info.DownloadedFileNumber}/{info.TotalDownloadFileNumber})，{info.DownloadedSpeed} KB/s......");
                    }
                }
                else
                {
                    SetProgress(1, "更新资源中......");
                }
            });

            if (DeploymentConfig.Current.DownloadInfo.IsDone)
            {
                if (DeploymentConfig.Current.DownloadInfo.DownloadResult == UpdateResourceDownloadInfo.Result.Success)
                {
                    SetProgress(1, "载入资源中，请稍等......");

                    Host.gameObject.SetActive(false);
                }
                else
                {
                    SetProgress(1, $"更新资源出错：{DeploymentConfig.Current.DownloadInfo.Error}，请退出重新进入！");
                }
            }
        }
    }
    #endregion

    #region SurePanel
    /// <summary>
    /// 确认界面
    /// </summary>
    public class SurePanel : ModuleBase
    {
        private GameObject _surePanel;
        private HTFAction _sureEvent;
        private HTFAction _cancelEvent;

        public override void OnInit()
        {
            base.OnInit();

            _surePanel = Host.FindChildren("SurePanel");

            _surePanel.FindChildren("Btn_Sure").GetComponent<Button>().onClick.AddListener(() =>
            {
                _sureEvent?.Invoke();
                Close();
            });
            _surePanel.FindChildren("Btn_Cancel").GetComponent<Button>().onClick.AddListener(() =>
            {
                _cancelEvent?.Invoke();
                Close();
            });
        }

        /// <summary>
        /// 打开确认界面
        /// </summary>
        public void Open(string content, HTFAction sure, HTFAction cancel = null)
        {
            _surePanel.SetActive(true);
            _surePanel.GetComponentByChild<Text>("Txt_Content").text = content;
            _sureEvent = sure;
            _cancelEvent = cancel;
        }
        /// <summary>
        /// 关闭确认界面
        /// </summary>
        public void Close()
        {
            _surePanel.SetActive(false);
            _sureEvent = null;
            _cancelEvent = null;
        }
    }
    #endregion
}