﻿// <copyright file="GridRow.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Bellatrix.Assertions;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Controls.Advanced.Grid;
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public class GridRow : Element, IElementInnerHtml
    {
        private Grid _parentGrid;

        public static event EventHandler<ElementActionEventArgs> Clicking;
        public static event EventHandler<ElementActionEventArgs> Clicked;

        public int Index { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerHtml => GetInnerHtmlAttribute();

        public GridCell this[int i] => GetCells().ToList().ElementAt(i);

        public void Click()
        {
            Click(Clicking, Clicked);
        }

        public void SetParentGrid(Grid grid)
        {
            _parentGrid = grid;
        }

        public GridCell GetCell(int column)
        {
            return _parentGrid.GetCell(Index, column);
        }

        public GridCell GetCell(string headerName)
        {
            return _parentGrid.GetCell(headerName, Index);
        }

        public GridCell GetCell<TDto>(Expression<Func<TDto, object>> expression)
            where TDto : class
        {
            return _parentGrid.GetCell<TDto>(expression, Index);
        }

        public IEnumerable<GridCell> GetCells()
        {
            var listOfCells = new List<GridCell>();
            var rowCells = _parentGrid.TableService.GetRowCells(Index);
            for (int rowCellsIndex = 0; rowCellsIndex < rowCells.Count; rowCellsIndex++)
            {
                var rowCellXPath = rowCells[rowCellsIndex].GetXPath();
                var cell = ElementCreateService.CreateByXpath<GridCell>(rowCellXPath);
                _parentGrid.SetCellMetaData(cell, Index, rowCellsIndex);
                listOfCells.Add(cell);
            }

            return listOfCells;
        }

        public IEnumerable<TElement> GetCells<TElement>()
            where TElement : Element, new()
        {
            var listOfElements = new List<TElement>();
            var cells = GetCells().ToList();
            for (int columnIndex = 0; columnIndex < cells.Count; columnIndex++)
            {
                var cell = cells[columnIndex];
                TElement element = new TElement();
                if (cell.CellControlElementType == null)
                {
                    listOfElements.Add(cell.As<TElement>());
                }
                else
                {
                    var repo = new ElementRepository();
                    element = repo.CreateElementWithParent(cell.CellControlBy, cell.WrappedElement, typeof(TElement), false);
                    listOfElements.Add(element);
                }
            }

            return listOfElements;
        }

        public ElementsList<TElement> GetCells<TElement>(Func<TElement, bool> selector)
            where TElement : Element, new()
        {
            return GetCells<TElement>().Where(selector).ToElementList();
        }

        public TElement GetFirstOrDefaultCell<TElement>(Func<TElement, bool> selector)
            where TElement : Element, new()
        {
            return GetCells(selector).FirstOrDefault();
        }

        public T GetItem<T>()
            where T : new()
        {
            return _parentGrid.CastRow<T>(Index);
        }

        public void AssertRow<T>(T expectedItem, params string[] propertiesNotToCompare)
            where T : new()
        {
            var actualItem = GetItem<T>();

            EntitiesAsserter.AreEqual(expectedItem, actualItem, propertiesNotToCompare);
        }

        internal void DefaultClick<TElement>(
            TElement element,
            EventHandler<ElementActionEventArgs> clicking,
            EventHandler<ElementActionEventArgs> clicked)
            where TElement : Element
        {
            clicking?.Invoke(this, new ElementActionEventArgs(element));

            element.ToExists().ToBeClickable().WaitToBe();
            element.WrappedElement.Click();

            clicked?.Invoke(this, new ElementActionEventArgs(element));
        }
    }
}