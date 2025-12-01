using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgresMigrations
{
    public partial class FormPreview : Form
    {
        public FormPreview(string content)
        {
            InitializeComponent();
            txtPreview.Text = content;
        }
    }
}
