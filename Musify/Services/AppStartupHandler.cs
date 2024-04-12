﻿using Microsoft.Extensions.Logging;
using Musify.Views;

namespace Musify.Services;

public class AppStartupHandler
{
    public AppStartupHandler(
        ILogger<AppStartupHandler> logger,
        MainView mainView,
        Navigation navigation)
    {
        navigation.SetCurrentIndex(0);
        navigation.Navigate("Home");

        mainView.SetSize(1100, 550);
        mainView.SetMinSize(670, 550);
        mainView.SetIcon("icon.ico");
        mainView.Activate();

        logger.LogInformation("[AppStartupHandler-.ctor] App fully started");
    }
}