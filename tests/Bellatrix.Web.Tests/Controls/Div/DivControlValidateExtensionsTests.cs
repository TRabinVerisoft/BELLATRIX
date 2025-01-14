﻿// <copyright file="DivControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Div Control")]
    [AllureFeature("ValidateExtensions")]
    public class DivControlValidateExtensionsTests : MSTest.WebTest
    {
        public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DivLocalPage);

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateStyleIs_DoesNotThrowException_When_Hover()
        {
            var divElement = App.Components.CreateById<Div>("myDiv");

            divElement.Hover();

            divElement.ValidateStyleIs("color: red;");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateInnerTextIs_DoesNotThrowException_When_InnerText()
        {
            var divElement = App.Components.CreateById<Div>("myDiv1");

            divElement.ValidateInnerTextIs("Automate The Planet");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ValidateInnerHtmlIs_DoesNotThrowException_When_InnerHtmlSet()
        {
            var divElement = App.Components.CreateById<Div>("myDiv2");

            divElement.ValidateInnerHtmlIs("<button name=\"button\">Click me</button>");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void validateDivSize_growsAfterExpansion()
        {
            App.Components.CreateByInnerTextContaining<Anchor>("Trainings").Click();
            Div traning2 = App.Components.CreateById<Div>("level-tow");
            int height = traning2.WrappedElement.Size.Height;
            App.Components.CreateByInnerTextContaining<Anchor>("C# Test Automation Advanced").Click();
            traning2.ValidatedHeightIsLargeThanMinHeight(height);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void validateDivSize_growsSmallAfterReduction()
        {
            App.Components.CreateByInnerTextContaining<Anchor>("Trainings").Click();
            Div traning2 = App.Components.CreateById<Div>("level-tow");
            int height = traning2.WrappedElement.Size.Height;
            App.Components.CreateByInnerTextContaining<Anchor>("C# Test Automation Advanced").Click();
            App.Components.CreateByInnerTextContaining<Anchor>("C# Test Automation Advanced").Click();
            traning2.ValidatedHeightIsSmallThanMaxHeight(height);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void validateDivSize_equalToBeforeAfterReload()
        {
            App.Components.CreateByInnerTextContaining<Anchor>("Trainings").Click();
            Div traning2 = App.Components.CreateById<Div>("level-tow");
            int height = traning2.WrappedElement.Size.Height;
            App.Components.CreateByInnerTextContaining<Anchor>("C# Test Automation Advanced").Click();
            App.Browser.Refresh();
            traning2 = App.Components.CreateById<Div>("level-tow");
            traning2.ValidatedHeightIsEqualToHeight(height);
            traning2.ValidatedHeightIsEqualToHeight(height, 2);
        }
    }
}