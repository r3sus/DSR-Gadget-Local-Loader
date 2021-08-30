using PropertyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSR_Gadget.List_Items.DSRParam
{
    public abstract class DSRParam
    {
        public List<Control> FlpContent { get; }
        protected PHPointer ParamPtr;
        protected PHPointer ItemParamPtr;
        protected PHook Hook;
        protected IDSRParam Item;

        public void SetActiveItem(IDSRParam item)
        {
            Item = item;
            ItemParamPtr = Hook.CreateChildPointer(ParamPtr, item.Offset);
        }

        public DSRParam(PHPointer paramPtr, PHook hook)
        {
            Hook = hook;
            ParamPtr = paramPtr;
            FlpContent = new List<Control>();
        }

        protected class FlpEntry<TControl> where TControl : Control
        {
            private Label Label;
            private TControl Cnt;
            private Action<TControl> TxtChangedAction;
            private Action<TControl> TxtUpdateAction;
            public FlowLayoutPanel Flp { get; }

            public FlpEntry(TControl control, string labelText, Action<TControl> changedAction, Action<TControl> updateAction)
            {
                Flp = new FlowLayoutPanel() { AutoSize = true };
                Cnt = control;
                Cnt.Width = 80;
                Cnt.Margin = new Padding(0, 0, 20, 5);
                Label = new Label() { Width = 200, Text = labelText };
                TxtChangedAction = changedAction;
                TxtUpdateAction = updateAction;

                Flp.Controls.Add(Cnt);
                Flp.Controls.Add(Label);
            }

            private void Entry_TextChanged(object sender, EventArgs e)
            {
                TxtChangedAction(Cnt);
            }

            public void Update()
            {
                TxtUpdateAction(Cnt);
            }
        }

        /*
        private class FlpTxtEntry
        {
            private Label Label;
            private TextBox Txt;
            private Action<TextBox> TxtChangedAction;
            private Action<TextBox> TxtUpdateAction;
            public FlowLayoutPanel Flp { get; }

            public FlpTxtEntry(string labelText, Action<TextBox> txtChangedAction, Action<TextBox> txtUpdateAction)
            {
                Flp = new FlowLayoutPanel() { AutoSize = true };
                Txt = new TextBox() { Width = 80, Margin = new Padding(0, 0, 20, 5) };
                Label = new Label() { Width = 200, Text = labelText };
                TxtChangedAction = txtChangedAction;
                TxtUpdateAction = txtUpdateAction;

                Flp.Controls.Add(Txt);
                Flp.Controls.Add(Label);
            }

            private void Entry_TextChanged(object sender, EventArgs e)
            {
                TxtChangedAction(Txt);
            }

            public void Update()
            {
                TxtUpdateAction(Txt);
            }
        }

        private class FlpNupEntry
        {
            private Label Label;
            private NumericUpDown Txt;
            private Action<NumericUpDown> TxtChangedAction;
            private Action<NumericUpDown> TxtUpdateAction;
            public FlowLayoutPanel Flp { get; }

            public FlpNupEntry(string labelText, Action<NumericUpDown> txtChangedAction, Action<NumericUpDown> txtUpdateAction)
            {
                Flp = new FlowLayoutPanel() { AutoSize = true };
                Txt = new NumericUpDown() { Width = 80, Margin = new Padding(0, 0, 20, 5) };
                Label = new Label() { Width = 200, Text = labelText };
                TxtChangedAction = txtChangedAction;
                TxtUpdateAction = txtUpdateAction;

                Flp.Controls.Add(Txt);
                Flp.Controls.Add(Label);
            }

            private void Entry_TextChanged(object sender, EventArgs e)
            {
                TxtChangedAction(Txt);
            }

            public void Update()
            {
                TxtUpdateAction(Txt);
            }
        }
        */
    }
}
