using System;

namespace LanguageComponentConsoleApplication
{
    /// <summary>
    /// 语句组件跟踪标签
    /// </summary>
    /// <param name="parameter"></param>
    /// <param name="track"></param>
    public delegate void LanguageComponentTrackMark(object parameter, LanguageComponentManager track);

    [Serializable]
    public class LanguageComponentManager : IDisposable
    {
        public LanguageComponentTrackMark trackMark;

        public object Parameters;

        public int Index;

        public LanguageComponent FirstLanguage;

        public void Run()
        {
            FirstLanguage.Run(this);
        }

        public void Resume()
        {
            if(trackMark != null)
            {
                (trackMark.GetInvocationList()[Index] as LanguageComponentTrackMark)(Parameters,this);
            }
        }

        public void Dispose()
        {
            if(this.trackMark == null)
            {
                this.FirstLanguage = null;
                this.Parameters = null;
            }
            LanguageComponentManagerFactory.FreezLanguageComponentManagerObject(this);
        }
    }
}