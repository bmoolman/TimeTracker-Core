using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Windows;

namespace TTWindowsWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }

        protected override Window CreateShell()
        {
            throw new NotImplementedException();
        }
    }
}
