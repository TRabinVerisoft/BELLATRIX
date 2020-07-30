﻿// <copyright file="Label.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.IOS;

namespace Bellatrix.Mobile.IOS
{
   public class Label : Element, IElementText
    {
        public static Func<Label, string> OverrideGetTextGlobally;

        public static Func<Label, string> OverrideGetTextLocally;

        public static new void ClearLocalOverrides()
        {
            OverrideGetTextLocally = null;
        }

        public string GetText()
        {
            var action = InitializeAction(this, OverrideGetTextGlobally, OverrideGetTextLocally, DefaultText);
            return action();
        }

        protected virtual string DefaultText(Label label) => DefaultGetText(label);
    }
}