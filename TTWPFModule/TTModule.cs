using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows.Controls;
using TTWPFModule.Views;

namespace TTWPFModule
{
    public class TTModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public TTModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
            IRegion region = _regionManager.Regions["ContentRegion"];

            var view1 = containerProvider.Resolve<ViewA>();
            region.Add(view1);

            var view2 = containerProvider.Resolve<ViewA>();
            view2.Content = new TextBlock()
            {
                Text="Hello from view 2",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment=System.Windows.VerticalAlignment.Center
                
            };
            region.Add(view2);
            region.Activate(view2);
            region.Activate(view1);
            region.Deactivate(view1);
            region.Activate(view2);
            region.Remove(view2);
            region.Activate(view1);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
