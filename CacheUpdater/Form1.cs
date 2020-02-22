using SmartCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CacheUpdater
{
    public partial class Form1 : Form
    {
        SmartCacheProvider cache;
        public Form1()
        {
            InitializeComponent();
            cache = new SmartCacheProvider("10.0.0.7:6379");
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        { 
            if (!string.IsNullOrEmpty(txt_Key.Text))
            {
                try
                {
                    cache.Remove(txt_Key.Text.Trim());
                    MessageBox.Show("Sucessfully REmoved Key" + txt_Key.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
            }
            else
            {
                MessageBox.Show("PLease ENter Valid Key");
            }
            
        }
    }
}
