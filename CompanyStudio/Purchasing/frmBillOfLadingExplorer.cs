﻿using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing
{
    public partial class frmBillOfLadingExplorer : BaseCompanyStudioContent, ILocationScoped
    {
        private const string INBOUND = "in";
        private const string OUTBOUND = "out";
        private const string CARRIER = "carrier";
        private const string COMPLETE = "complete";

        public frmBillOfLadingExplorer()
        {
            InitializeComponent();

            imageList.Images.Add(INBOUND, Properties.Resources.cart_go);
            imageList.Images.Add(OUTBOUND, Properties.Resources.money);
            imageList.Images.Add(CARRIER, Properties.Resources.lorry);
            imageList.Images.Add(COMPLETE, Properties.Resources.accept);
        }

        public Location LocationModel { get; set; }

        private async void frmBillOfLadingExplorer_Load(object sender, EventArgs e)
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                treBOLs.Nodes.Clear();

                TreeNode inboundNode = new TreeNode("Consigned") { ImageKey = INBOUND, SelectedImageKey = INBOUND };
                TreeNode outboundNode = new TreeNode("Shipped") { ImageKey = OUTBOUND, SelectedImageKey = OUTBOUND };
                TreeNode carrierNode = new TreeNode("Carrier") { ImageKey = CARRIER, SelectedImageKey = CARRIER };
                TreeNode historicalNode = new TreeNode("Historical") { ImageKey = COMPLETE, SelectedImageKey = COMPLETE };

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "BillOfLading/GetForCompany");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<BillOfLading> billsOfLading = await get.GetObject<List<BillOfLading>>() ?? new List<BillOfLading>();

                foreach(BillOfLading billOfLading in billsOfLading.OrderBy(bol => bol.IssuedDate))
                {
                    string nodeText = string.Format("BOL {0} - Container {1}", billOfLading.BillOfLadingID, billOfLading.Railcar?.ReportingID ?? "[None]");
                    string toolTipText = string.Format("BOL {0} - {1} - From {2} to {3} via {4}", billOfLading.BillOfLadingID, billOfLading.Railcar?.ReportingID, billOfLading.From, billOfLading.To, billOfLading.Via);

                    TreeNode bolNode = new TreeNode(nodeText);
                    bolNode.ToolTipText = toolTipText;

                    if (billOfLading.CompanyIDShipper == Company.CompanyID)
                    {
                        bolNode.ImageKey = OUTBOUND;
                        bolNode.SelectedImageKey = OUTBOUND;
                        if (billOfLading.DeliveredDate == null) { outboundNode.Nodes.Add(bolNode); } else { historicalNode.Nodes.Add(bolNode); }
                    }
                    else if (billOfLading.CompanyIDConsignee == Company.CompanyID)
                    {
                        bolNode.ImageKey = INBOUND;
                        bolNode.SelectedImageKey = INBOUND;
                        if (billOfLading.DeliveredDate == null) { inboundNode.Nodes.Add(bolNode); } else { historicalNode.Nodes.Add(bolNode); }
                    }
                    else if (billOfLading.CompanyIDCarrier != Company.CompanyID)
                    {
                        bolNode.ImageKey = CARRIER;
                        bolNode.SelectedImageKey = CARRIER;
                        if (billOfLading.DeliveredDate == null) { carrierNode.Nodes.Add(bolNode); } else { historicalNode.Nodes.Add(bolNode); }
                    }
                }

                inboundNode.Text += " (" + inboundNode.Nodes.Count + ")";
                outboundNode.Text += " (" + outboundNode.Nodes.Count + ")";
                carrierNode.Text += " (" + carrierNode.Nodes.Count + ")";

                treBOLs.Nodes.Add(inboundNode);
                treBOLs.Nodes.Add(outboundNode);
                treBOLs.Nodes.Add(carrierNode);
                treBOLs.Nodes.Add(historicalNode);

                treBOLs.ExpandAll();
                historicalNode.Collapse();
            }
            finally
            {
                loader.Visible = false;
            }
        }
    }
}
