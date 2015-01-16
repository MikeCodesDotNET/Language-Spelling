using System;
using UIKit;
using Foundation;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using Spelling.Core.Models;

namespace Spelling.Core
{
    public class SkillsTableDataSource : UITableViewDataSource
    {
        List<string> tableItems;
        string cellIdentifier = "TableCell";

        public SkillsTableDataSource (List<string> items)
        {
            tableItems = items;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return tableItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
            cell.TextLabel.Text = tableItems[indexPath.Row];
            return cell;
        }
    }
}

