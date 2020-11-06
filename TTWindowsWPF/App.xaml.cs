using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Windows;
using TTWindowsWPF.Views;

namespace TTWindowsWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {        

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
