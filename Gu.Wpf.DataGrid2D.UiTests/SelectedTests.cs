namespace Gu.Wpf.DataGrid2D.UiTests
{
    using Gu.Wpf.UiAutomation;
    using NUnit.Framework;

    public class SelectedTests
    {
        [Test]
        public void SelectingInViewUpdatesIndexAndCellItem()
        {
            using (var app = Application.Launch(Info.ExeFileName, "SelectionWindow"))
            {
                var window = app.MainWindow;
                var tabItem = window.FindTabItem("SelectedCellItem");
                tabItem.Click();
                var dataGrid = tabItem.FindDataGrid("DataGrid");
                var indexBox = tabItem.FindTextBox("SelectedIndex");
                var itemBox = tabItem.FindTextBlock("SelectedItem");

                dataGrid[0, 0].Click();
                Assert.AreEqual("R0 C0", indexBox.Text);
                Assert.AreEqual("Item: 1", itemBox.Text);

                dataGrid[0, 1].Click();
                Assert.AreEqual("R0 C1", indexBox.Text);
                Assert.AreEqual("Item: 2", itemBox.Text);

                dataGrid[1, 1].Click();
                Assert.AreEqual("R1 C1", indexBox.Text);
                Assert.AreEqual("Item: 4", itemBox.Text);

                dataGrid[2, 0].Click();
                Assert.AreEqual("R2 C0", indexBox.Text);
                Assert.AreEqual("Item: 5", itemBox.Text);

                dataGrid[2, 1].Click();
                Assert.AreEqual("R2 C1", indexBox.Text);
                Assert.AreEqual("Item: 6", itemBox.Text);
            }
        }

        [Test]
        public void SettingIndexInViewModelUpdatesSelectionAndCellItem()
        {
            using (var app = Application.Launch(Info.ExeFileName, "SelectionWindow"))
            {
                var window = app.MainWindow;
                var tabItem = window.FindTabItem("SelectedCellItem");
                tabItem.Click();
                var indexBox = tabItem.FindTextBox("SelectedIndex");
                var itemBox = tabItem.FindTextBlock("SelectedItem");
                var loseFocusButton = window.FindButton("SelectionLoseFocusButton");

                indexBox.Text = "R1 C1";
                loseFocusButton.Click();
                Assert.AreEqual("Item: 4", itemBox.Text);

                indexBox.Text = "R0 C0";
                loseFocusButton.Click();
                Assert.AreEqual("Item: 1", itemBox.Text);

                // Not sure how we want to handle out of bounds
                indexBox.Text = "R10 C10";
                loseFocusButton.Click();
                Assert.AreEqual(string.Empty, itemBox.Text);
            }
        }

        [Test]
        public void SettingCellItemInViewModelUpdatesSelectionAndIndex()
        {
            using (var app = Application.Launch(Info.ExeFileName, "SelectionWindow"))
            {
                var window = app.MainWindow;
                var tabItem = window.FindTabItem("SelectedCellItem");
                tabItem.Click();
                var indexBox = tabItem.FindTextBox("SelectedIndex");
                var itemBox = tabItem.FindListBox("SelectionList");
                itemBox.Select(3);
                Assert.AreEqual("R1 C1", indexBox.Text);

                itemBox.Select(0);
                Assert.AreEqual("R0 C0", indexBox.Text);
            }
        }

        [Test]
        public void Reminder()
        {
            Assert.Inconclusive("Assert cell.IsSelected when supported");
        }
    }
}
