using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace GovernmentPortal
{
    internal abstract class ExplorerContext<TModel> where TModel:class
    {
        internal abstract Icon ExplorerIcon { get; }
        internal abstract IExplorerControl<TModel> GetControlForModel(TModel model);
        internal abstract Task<List<DropDownItem<TModel>>> GetInitialListItems();
        internal virtual string ObjectDisplayName => typeof(TModel).Name.ToDisplayName();
        internal virtual bool DeleteButtonVisible => true;
        internal virtual IEnumerable<ToolStripMenuItem> GetExtraToolItems() => Enumerable.Empty<ToolStripMenuItem>();
        internal virtual DropDownItem<TModel> GetInitiallySelectedItem(IReadOnlyCollection<DropDownItem<TModel>> items) => null;
    }
}
