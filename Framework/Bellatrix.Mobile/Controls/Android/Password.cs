﻿// <copyright file="Password.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Android;
using Bellatrix.Mobile.Events;

namespace Bellatrix.Mobile.Android
{
    public class Password : Element, IElementDisabled
    {
        public static Func<Password, bool> OverrideIsDisabledGlobally;
        public static Func<Password, string> OverrideGetPasswordGlobally;
        public static Action<Password, string> OverrideSetPasswordGlobally;

        public static Func<Password, bool> OverrideIsDisabledLocally;
        public static Func<Password, string> OverrideGetPasswordLocally;
        public static Action<Password, string> OverrideSetPasswordLocally;

        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> SettingPassword;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> PasswordSet;

        public static new void ClearLocalOverrides()
        {
            OverrideIsDisabledLocally = null;
            OverrideGetPasswordLocally = null;
            OverrideSetPasswordLocally = null;
        }

        public string GetPassword()
        {
            var action = InitializeAction(this, OverrideGetPasswordGlobally, OverrideGetPasswordLocally, DefaultGetPassword);
            return action();
        }

        public void SetPassword(string password)
        {
            var action = InitializeAction(this, OverrideSetPasswordGlobally, OverrideSetPasswordLocally, DefaultSetPassword);
            action(password);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled
        {
            get
            {
                var action = InitializeAction(this, OverrideIsDisabledGlobally, OverrideIsDisabledLocally, DefaultIsDisabled);
                return action();
            }
        }

        protected virtual bool DefaultIsDisabled(Password password) => base.DefaultIsDisabled(password);

        protected virtual string DefaultGetPassword(Password password) => DefaultGetText(password);

        protected virtual void DefaultSetPassword(Password password, string value) => DefaultSetText(password, SettingPassword, PasswordSet, value);
    }
}