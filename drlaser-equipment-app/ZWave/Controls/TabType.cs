using CommonLib.ClassHierarchy.Implementation;
using ZWave.Views;
using ZWave.Views.Jobs;
using ZWave.Views.Systems;
using ZWave.Views.Systems.CausewaySystem;
using ZWave.Views.Systems.Motions;
using ZWave.Views.Systems.ProcessSystem;

namespace ZWave.Controls
{
    public enum TabType
    {
        JobMain1,
        JobMain2,

        Motion,
        MotionAxis,
        Laser,
        Vision,

        AxisControl,
        MotionSetting,
        MotionControl,
        MotionGain,
        MotionFilter,

        LaserBasic,
        LaserBurst,
        LaserExternal,

        DonorLiftingModule,
        SubstrateLiftingModule,

        ProcessTable,
        InspectionVision,
        InspectionVisionCameraPage
    }

    public class TabTypeResolver
    {
        public static View TabTypeToView(TabType tabType)
        {
            switch (tabType)
            {
                case TabType.JobMain1:
                    return Helpers.ServiceProvider.GetService<JobMainView>();
                case TabType.JobMain2:
                    return Helpers.ServiceProvider.GetService<JobMainView2>();

                case TabType.Motion:
                    return Helpers.ServiceProvider.GetService<SystemMotionPage>();
                case TabType.MotionGain:
                    return Helpers.ServiceProvider.GetService<TuneSettingsGainsControlPage>();
                case TabType.MotionFilter:
                    return Helpers.ServiceProvider.GetService<TuneSettingsFilterControlPage>();
                case TabType.MotionControl:
                    return Helpers.ServiceProvider.GetService<MotionControlPage>();
                case TabType.MotionSetting:
                    return Helpers.ServiceProvider.GetService<MotionSettingPage>();
                case TabType.MotionAxis:
                    return Helpers.ServiceProvider.GetService<MotionAxisControlPage>();
                case TabType.Laser:
                    return Helpers.ServiceProvider.GetService<SystemLaserPage>();
                case TabType.Vision:
                    return Helpers.ServiceProvider.GetService<SystemVisionPage>();

                case TabType.LaserBasic:
                    return Helpers.ServiceProvider.GetService<LaserBasicPage>();
                case TabType.LaserBurst:
                    return Helpers.ServiceProvider.GetService<LaserBurstControlPage>();
                case TabType.LaserExternal:
                    return Helpers.ServiceProvider.GetService<LaserExternalControlPage>();

                case TabType.DonorLiftingModule:
                    return Helpers.ServiceProvider.GetService<DonorLiftingModulePage>();
                case TabType.SubstrateLiftingModule:
                    return Helpers.ServiceProvider.GetService<SubstrateLiftingModulePage>();
                    
                case TabType.ProcessTable:
                    return Helpers.ServiceProvider.GetService<ProcessTablePage>();
                case TabType.InspectionVision:
                    return Helpers.ServiceProvider.GetService<InspectionVisionPage>();
            }

            throw new ArgumentOutOfRangeException(nameof(tabType));
        }
    }
}
