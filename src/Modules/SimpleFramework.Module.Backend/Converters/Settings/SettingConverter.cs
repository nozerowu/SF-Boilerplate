﻿using Omu.ValueInjecter;
using webModel = SimpleFramework.Module.Backend.ViewModels.Setting;
using moduleModel = SimpleFramework.Core.Settings;

namespace SimpleFramework.Module.Backend.Converters.Settings
{
    public static class SettingConverter
    {
        public static webModel.SettingViewModel ToWebModel(this moduleModel.SettingEntry setting)
        {
			var retVal = new webModel.SettingViewModel();
			retVal.InjectFrom(setting);
            return retVal;
        }

		public static moduleModel.SettingEntry ToModuleModel(this webModel.SettingViewModel setting)
        {
			var retVal = new moduleModel.SettingEntry();
			retVal.InjectFrom(setting);
			return retVal;
        }
    }
}
