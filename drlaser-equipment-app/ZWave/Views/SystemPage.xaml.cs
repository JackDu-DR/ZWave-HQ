using System.Diagnostics;
using XAct;
using XAct.UI.Views;
using ZWave.Controls;
using ZWave.Helpers;
using ZWave.ViewModels;

namespace ZWave.Views;

public partial class SystemPage : ContentView
{

    public SystemPage()
    {
        InitializeComponent();
        GenerateContent("Module1");
    }

    private void GenerateContent(string showingTab)
    {
        switch (showingTab)
        {
            case "Module1":
                Module1Content();
                ((SystemPageViewModel)BindingContext).IsShowingModule1 = true;
                Module1.IsSelected = true;
                break;
            case "CausewaySystem":
                CausewaySystemContent();
                ((SystemPageViewModel)BindingContext).IsShowingCausewaySystem = true;
                CausewaySystem.IsSelected = true;
                break;
            case "ProcessSystem":
                ProcessSystemContent();
                ((SystemPageViewModel)BindingContext).IsShowingProcessSystem = true;
                ProcessSystem.IsSelected = true;
                break;
        }
    }

    private void Module1Content()
    {
       
        CustomTabView module1Tab = new CustomTabView();

        CustomTabItem motionTabItem = new CustomTabItem();
        motionTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[Motion]"));
        motionTabItem.TabType = TabType.Motion;
        motionTabItem.CornerRadius = new CornerRadius(8, 0, 0, 0);
        motionTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");


        CustomTabItem laserTabItem = new CustomTabItem();
        laserTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[Laser]"));
        laserTabItem.TabType = TabType.Laser;
        laserTabItem.CornerRadius = new CornerRadius(0, 0, 0, 0);
        laserTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");


        CustomTabItem visionTabItem = new CustomTabItem();
        visionTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[Vision]"));
        visionTabItem.TabType = TabType.Vision;
        visionTabItem.CornerRadius = new CornerRadius(0, 8, 0, 0);
        motionTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");

        module1Tab.TabItems.Add(motionTabItem);
        module1Tab.TabItems.Add(laserTabItem);
        module1Tab.TabItems.Add(visionTabItem);

        module1Tab.CurrentItem = laserTabItem;
        module1Tab.ClassId = "Module1";

        module1Tab.SetBinding(CustomTabView.IsVisibleProperty, new Binding("IsShowingModule1"));
        SystemPageContent.Children.Add(module1Tab);
    }

    private void CausewaySystemContent()
    {
        CustomTabView causewaySystemTab = new CustomTabView();

        CustomTabItem donorLiftingModuleTabItem = new CustomTabItem();
        donorLiftingModuleTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[DonorLiftingModule]"));
        donorLiftingModuleTabItem.TabType = TabType.DonorLiftingModule;
        donorLiftingModuleTabItem.CornerRadius = new CornerRadius(8, 0, 0, 0);
        donorLiftingModuleTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");

        CustomTabItem substrateLiftingModuleTabItem = new CustomTabItem();
        substrateLiftingModuleTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[SubstrateLiftingModule]"));
        substrateLiftingModuleTabItem.TabType = TabType.SubstrateLiftingModule;
        substrateLiftingModuleTabItem.CornerRadius = new CornerRadius(0, 0, 0, 0);
        substrateLiftingModuleTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");

        causewaySystemTab.TabItems.Add(donorLiftingModuleTabItem);
        causewaySystemTab.TabItems.Add(substrateLiftingModuleTabItem);

        causewaySystemTab.CurrentItem = donorLiftingModuleTabItem;
        causewaySystemTab.ClassId = "CausewaySystem";

        causewaySystemTab.SetBinding(CustomTabView.IsVisibleProperty, new Binding("IsShowingCausewaySystem"));
        SystemPageContent.Children.Add(causewaySystemTab);
    }

    private void ProcessSystemContent()
    {
        CustomTabView processSystemTab = new CustomTabView();

        CustomTabItem processTableTabItem = new CustomTabItem();
        processTableTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[ProcessTable]"));
        processTableTabItem.TabType = TabType.ProcessTable;
        processTableTabItem.CornerRadius = new CornerRadius(8, 0, 0, 0);
        processTableTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");

        CustomTabItem inspectionVisionTabItem = new CustomTabItem();
        inspectionVisionTabItem.SetBinding(CustomTabItem.TitleProperty, new Binding("Localization[InspectionVision]"));
        inspectionVisionTabItem.TabType = TabType.InspectionVision;
        inspectionVisionTabItem.CornerRadius = new CornerRadius(0, 0, 0, 0);
        inspectionVisionTabItem.TabColor = ResourceHelper.GetColor("GreyNeutral3");

        processSystemTab.TabItems.Add(processTableTabItem);
        processSystemTab.TabItems.Add(inspectionVisionTabItem);

        processSystemTab.CurrentItem = processTableTabItem;
        processSystemTab.ClassId = "ProcessSystem";

        processSystemTab.SetBinding(CustomTabView.IsVisibleProperty, new Binding("IsShowingProcessSystem"));
        SystemPageContent.Children.Add(processSystemTab);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Module1.IsSelected = false;
        CausewaySystem.IsSelected = false;
        ProcessSystem.IsSelected = false;
        LaserEngine.IsSelected = false;

        ((SystemPageViewModel)BindingContext).IsShowingModule1 = false;
        ((SystemPageViewModel)BindingContext).IsShowingCausewaySystem = false;
        ((SystemPageViewModel)BindingContext).IsShowingProcessSystem = false;
        ((SystemPageViewModel)BindingContext).IsShowingLaserEngine = false;

        var btn = (CustomButton)sender;
        var btName = btn.ClassId;

        switch (btName)
        {
            case "Module1":
                if(!CheckContentExist("Module1"))
                    GenerateContent("Module1");
                ((SystemPageViewModel)BindingContext).IsShowingModule1 = true;
                Module1.IsSelected = true;
                break;
            case "CausewaySystem":
                if (!CheckContentExist("CausewaySystem"))
                    GenerateContent("CausewaySystem");
                ((SystemPageViewModel)BindingContext).IsShowingCausewaySystem = true;
                CausewaySystem.IsSelected = true;
                break;
            case "ProcessSystem":
                if (!CheckContentExist("ProcessSystem"))
                    GenerateContent("ProcessSystem");
                ((SystemPageViewModel)BindingContext).IsShowingProcessSystem = true;
                ProcessSystem.IsSelected = true;
                break;
            //case "LaserEngine":
            //    LaserEngine.IsSelected = true;
            //    break;
        }

        


    }

    private bool CheckContentExist(string showingTab)
    {
        bool result = false;
        foreach (View childView in SystemPageContent.Children)
        {
            if(childView.ClassId == showingTab)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}