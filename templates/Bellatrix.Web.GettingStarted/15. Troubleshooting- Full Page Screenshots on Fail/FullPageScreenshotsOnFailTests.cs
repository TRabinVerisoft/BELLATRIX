﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this behaviour.
    // "screenshotsSettings": {
    //     "isEnabled": "true",
    //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
    // }
    //
    // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
    // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    [Browser(OS.OSX, BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class FullPageScreenshotsOnFailTests : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");
            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions");
            promotionsLink.Click();
        }
    }
}